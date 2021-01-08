using Liedeinblendung.Extensions;
using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Liedeinblendung.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel(List<Song> hymnalList, string bookname)
        {
            VerseList = new ObservableCollection<SelectedVerse>();

            _currentHymnal = hymnalList;
            _bookname = bookname;
        }

        public ICommand AcceptCommand => new RelayCommand(OnAcceptPressed);

        private readonly string _bookname;

        public string InputNumber 
        {
            get { return GetValue<string>(); }
            set 
            { 
                SetValue(value);
                CheckValid(value);            
            }
        }

        public string InputText
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string InputVers
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool NumberFalidFlag
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<SelectedVerse> VerseList
        {
            get { return GetValue<ObservableCollection<SelectedVerse>>(); }
            set { SetValue(value); }
        }

        public string MelodieAutor
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string TextAutor
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public List<int> SelectedVerses
        {
            get { return GetValue<List<int>>(); }
            set { SetValue(value); }
        }

        


        private readonly FadeInWriter _fadeInWriter = new FadeInWriter();

        private List<Song> _currentHymnal = new List<Song>();




        private void CheckValid(string number)
        {
            if (_currentHymnal.Exists(x=>x.Number == number))
            {
                NumberFalidFlag = true;
                CreateFade(number);
                return;
            }

            if (_currentHymnal.Exists(x => x.Number == $"{number}a"))
            {

                NumberFalidFlag = true;
                InputNumber = $"{number}a";
                CreateFade(InputNumber);
                return;
            }

            NumberFalidFlag = false;
            InputText = "";
            VerseList.Clear();
            MelodieAutor = "";
            TextAutor = "";

        }

        private void OnAcceptPressed(object obj)
        {
            //WriteInputVers();
            var hymnal = new HymnalData(_bookname, InputNumber, InputText, MelodieAutor, TextAutor, VerseList);
            _fadeInWriter.WriteFade(hymnal);
            ClearView();
        }

        private void WriteInputVers()
        {
            string state = "";
            
            foreach (var verse in VerseList)
            {
                if (verse.IsSelected)
                    state += "T";
                else
                    state += "F";
            }

            List<Match> matches = new List<Match>();

            foreach(Match match in Regex.Matches(state, "[T]{1,}"))
            {
                matches.Add(match);
            }

            if ( matches.Count > 0)
            {
                InputVers = " / ";

                foreach (var match in matches)
                {

                    switch (match.Length)
                    {
                        case 1:
                            InputVers = $"{InputVers}{match.Index + 1}";
                            break;

                        case 2:
                            InputVers = $"{InputVers}{match.Index + 1}-{match.Index + match.Length}";
                            break;

                        default:
                            InputVers = $"{InputVers}{match.Index + 1}-{match.Index + match.Length}";
                            break;
                    }

                    InputVers = $"{InputVers} + ";


                }

                char[] charsToTrim = { ' ', '+'};
                InputVers = InputVers.TrimEnd(charsToTrim);
                return;

            }
            InputVers = "";



        }

        private void ClearView()
        {
            InputNumber = "";
            InputVers = "";

        }

        private void CreateFade(string number)
        {
            Song current = _currentHymnal.Find(x => x.Number == number);

            VerseList.Clear();
            InputText = current.Title;

            foreach(var verse in current.Verses)
            {
                VerseList.Add(new SelectedVerse(verse));
            }

            if (current.Metadata.Exists(x => x.Key == "Text"))
                TextAutor = $" Text: {current.Metadata.Find(x => x.Key == "Text").Value}";
            else
                TextAutor = "";

            if (current.Metadata.Exists(x => x.Key == "Melodie"))
                MelodieAutor = $" Melodie: {current.Metadata.Find(x => x.Key == "Melodie").Value}";
            else if
                 (current.Metadata.Exists(x => x.Key == "Musik"))
                MelodieAutor = $" Musik: {current.Metadata.Find(x => x.Key == "Musik").Value}";
            else
            MelodieAutor = "";




            //hymnal.SongVerses = InputVers;
        }


        

    }
}
