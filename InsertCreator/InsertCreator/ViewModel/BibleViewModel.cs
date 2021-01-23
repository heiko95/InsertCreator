using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class BibleViewModel : ObservableObject, INotifyDataErrorInfo
    {
        #region Public Constructors

        public BibleViewModel(ObservableCollection<BibleBook> biblebooks, FadeInWriter fadeInWriter)
        {
            this.Errors = new Dictionary<string, List<string>>();
            Biblebooks = biblebooks;

            foreach (var item in Biblebooks)
            {
                BiblebookNames.Add(item.Name);
            }

            BibleBookView = new CollectionView(Biblebooks);
            BibleBookView.MoveCurrentTo(Biblebooks[0]);

            BibleBookView.CurrentChanged += new EventHandler(queries_CurrentChanged);
        }

        #endregion Public Constructors

        #region Public Properties

        public List<string> BiblebookNames { get; set; } = new List<string>();
        public ObservableCollection<BibleBook> Biblebooks { get; set; } = new ObservableCollection<BibleBook>();
        public CollectionView BibleBookView { get; private set; }

        public string Selectedbook
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (!string.IsNullOrEmpty(value))
                {
                    _maxChapter = Biblebooks.First(x => x.Name == value).Chapters.Count();
                    BookSelectedFlag = true;
                    OnPropertyChanged("BookSelectedFlag");
                    return;
                }

                BookSelectedFlag = false;
                OnPropertyChanged("BookSelectedFlag");
            }
        }

        public bool BookSelectedFlag
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
            }
        }

        private int _maxChapter = 0;

        public string SelectedChapter
        {
            get { return GetValue<string>(); }
            set
            {
                if (ValidateProperty(value, ChapterValidation))
                {
                    SetValue(value);
                    return;
                }
                SetValue("");
            }
        }

        public string SelectedValue
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                BibleBookView.Refresh();
            }
        }

        public string SelectedVerses
        {
            get { return GetValue<string>(); }
            set
            {
                if (ValidateProperty(value,
               newValue => newValue.StartsWith("@")
                 ? (true, new List<string>())
                 : (false, new List<string> { "Value must start with '@'." })))
                {
                    SetValue(value);
                }
            }
        }

        #endregion Public Properties

        #region Private Properties

        // Maps a property name to a list of errors that belong to this property
        private Dictionary<String, List<String>> Errors { get; }

        #endregion Private Properties

        #region Public Methods

        public void AddError(string propertyName, string errorMessage, bool isWarning = false)
        {
            if (!this.Errors.TryGetValue(propertyName, out List<string> propertyErrors))
            {
                propertyErrors = new List<string>();
                this.Errors[propertyName] = propertyErrors;
            }

            if (!propertyErrors.Contains(errorMessage))
            {
                if (isWarning)
                {
                    // Move warnings to the end
                    propertyErrors.Add(errorMessage);
                }
                else
                {
                    propertyErrors.Insert(0, errorMessage);
                }
                OnErrorsChanged(propertyName);
            }
        }

        public bool PropertyHasErrors(string propertyName) => this.Errors.TryGetValue(propertyName, out List<string> propertyErrors) && propertyErrors.Any();

        #endregion Public Methods

        #region Protected Methods

        protected virtual void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion Protected Methods

        #region Private Methods

        private (bool IsValid, IEnumerable<string> ErrorMessages) ChapterValidation(string value)
        {
            if (string.IsNullOrEmpty(value))
                return (true, new List<string>());

            int number;
            if (int.TryParse(value, out number))
            {
                if (number == 0 || number > _maxChapter)
                {
                    return (false, new List<string>() { "Ungültiger Wert" });
                }
                return (true, new List<string>());
            }

            return (false, new List<string>() { "Wert muss eine Zahl sein" });
        }

        #endregion Private Methods

        #region INotifyDataErrorInfo implementation

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // Returns if the view model has any invalid property
        public bool HasErrors => this.Errors.Any();

        // Returns all errors of a property. If the argument is 'null' instead of the property's name,
        // then the method will return all errors of all properties.
        public System.Collections.IEnumerable GetErrors(string propertyName)
          => string.IsNullOrWhiteSpace(propertyName)
            ? this.Errors.SelectMany(entry => entry.Value)
            : this.Errors.TryGetValue(propertyName, out List<string> errors)
              ? errors
              : new List<string>();

        public bool ValidateProperty<TValue>(
      TValue value,
      Func<TValue, (bool IsValid, IEnumerable<string> ErrorMessages)> validationDelegate,
      [CallerMemberName] string propertyName = null)
        {
            // Clear previous errors of the current property to be validated
            this.Errors.Remove(propertyName);
            OnErrorsChanged(propertyName);

            // Validate using the delegate
            (bool IsValid, IEnumerable<string> ErrorMessages) validationResult = ((bool IsValid, IEnumerable<string> ErrorMessages))(validationDelegate?.Invoke(value));

            if (!validationResult.IsValid)
            {
                // Store the error messages of the failed validation
                foreach (string errorMessage in validationResult.ErrorMessages)
                {
                    // See previous example for implementation of AddError(string,string):void
                    AddError(propertyName, errorMessage);
                }
            }

            return validationResult.IsValid;
        }

        #endregion INotifyDataErrorInfo implementation

        private void BibleViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
        }

        private void queries_CurrentChanged(object sender, EventArgs e)
        {
            BibleBook currentQuery = (BibleBook)BibleBookView.CurrentItem;
        }
    }
}