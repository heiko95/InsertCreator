using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Documents;

namespace HgSoftware.InsertCreator.Model
{
    internal class CalendarService
    {
        #region Fields

        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private readonly BibleTextService _bibleTextService = new BibleTextService($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Luther2017.csv");
        private readonly string _bookname;
        private readonly string _calendarUrl;
        private readonly List<Song> _gbData;
        private readonly HistoryViewModel _historyViewModel;
        private readonly MinistryJsonReaderWriter _readerWriter = new MinistryJsonReaderWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Ministry.json");
        private string _currentEventData = null;
        private DateTime _currentEventDate = DateTime.MaxValue;
        private List<MinistryGridViewModel> _ministries;

        #endregion Fields

        #region Constructors

        public CalendarService(string calendarUrl, HistoryViewModel historyViewModel)
        {
            _calendarUrl = calendarUrl;
            _historyViewModel = historyViewModel;
            _gbData = HymnalJsonReader.LoadHymnalData($"{Directory.GetCurrentDirectory()}/DataSource/GB_Data.json");
            _bookname = "Gesangbuch";

            if (_readerWriter.LoadMinistryData() != null)
                _ministries = _readerWriter.LoadMinistryData().ToList();

            var events = LoadEventData();
            GetCurrentEvent(events);
        }

        #endregion Constructors

        #region Methods

        internal void CreateHymnal(string number)
        {
            try
            {
                Song current = _gbData.Find(x => (x.Number == number || x.Number == $"{number}a"));
                var hymnal = new HymnalData(_bookname,
                                            current.Number,
                                            current.Title,
                                            $" Melodie: {current.Metadata.Find(x => x.Key == "Melodie").Value}",
                                            current.Metadata.Exists(x => x.Key == "Text") ? $" Text: {current.Metadata.Find(x => x.Key == "Text").Value}" : "",
                                            new ObservableCollection<SelectedVerse>());
                _historyViewModel.AddToHistory(hymnal);
            }
            catch (Exception ex)
            {
                _log.Error($"Error while creating Hymnal {number} for event {_currentEventDate}", ex);
            }
        }

        internal void CreateTodayInserts()
        {
            if (_currentEventData != null)
            {
                var values = _currentEventData.Split(';');
                if (!string.IsNullOrEmpty(values[0]))
                    CreateHymnal(values[0]);

                if (!string.IsNullOrEmpty(values[1]))
                    CreateHymnal(values[1]);

                if (!string.IsNullOrEmpty(values[2]))
                    CreateHymnal(values[2]);

                if (!string.IsNullOrEmpty(values[3]) && !string.IsNullOrEmpty(values[4]) && !string.IsNullOrEmpty(values[5]))
                    CreateBibleText(values[3], values[4], values[5]);

                if (!string.IsNullOrEmpty(values[6]) && !string.IsNullOrEmpty(values[7]) && !string.IsNullOrEmpty(values[8]))
                    CreateMynistry(values[6], values[7], values[8]);
            }
        }

        private void CreateBibleText(string book, string chapter, string verses)
        {
            try
            {
                var verseList = _bibleTextService.GetVerseList(verses);
                var text = _bibleTextService.GetBibleText(book, Convert.ToInt32(chapter), verseList);
                var bibleData = new BibleData(book, chapter, verses, text);
                _historyViewModel.AddToHistory(bibleData);
            }
            catch (Exception ex)
            {
                _log.Error($"Error while creating BibleText for event {_currentEventDate}", ex);
            }
        }

        private void CreateMynistry(string firstname, string surename, string funktion)
        {
            try
            {
                var ministry = _ministries.Find(x => x.ForeName == firstname && x.SureName == surename && x.Function == funktion);
                if (ministry != null)
                    _historyViewModel.AddToHistory(ministry);
            }
            catch (Exception ex)
            {
                _log.Error($"Error while creating Ministry for event {_currentEventDate}", ex);
            }
        }

        private void GetCurrentEvent(Dictionary<DateTime, string> events)
        {
            DateTime today = DateTime.Today;
            DateTime currentTime = DateTime.Now;

            foreach (var eventData in events)
            {
                if (eventData.Key.Date == today && eventData.Key.TimeOfDay >= currentTime.TimeOfDay)
                {
                    if (eventData.Key < _currentEventDate)
                    {
                        _currentEventDate = eventData.Key;
                        _currentEventData = eventData.Value;
                    }
                }
            }
        }

        private Dictionary<DateTime, string> LoadEventData()
        {
            var events = new Dictionary<DateTime, string>();
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.GetEncoding("UTF-8");
                string icsData = client.DownloadString(_calendarUrl);

                // Split the ics data by lines
                string[] lines = icsData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // Find the lines with X-INSERTCREATORDATA and extract the values
                foreach (string line in lines)
                {
                    try
                    {
                        if (line.StartsWith("X-INSERTCREATORDATA:"))
                        {
                            string value = line.Substring("X-INSERTCREATORDATA:".Length);
                            var datestring = value.Split(';').First();
                            var eventData = value.Substring(datestring.Length + 1);
                            var date = DateTime.Parse(datestring);
                            events.Add(date, eventData);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Invalid data in calendar file", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error while loading calendar data", ex);
            }

            return events;
        }

        #endregion Methods
    }
}