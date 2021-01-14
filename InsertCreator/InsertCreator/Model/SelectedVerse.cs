using Liedeinblendung.ViewModel;

namespace Liedeinblendung.Model
{
    public class SelectedVerse : ObservableObject
    {
        #region Public Constructors

        public SelectedVerse(Verse verse)
        {
            Verse = verse;
            IsSelected = false;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsSelected
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public Verse Verse
        {
            get { return GetValue<Verse>(); }
            set { SetValue(value); }
        }

        #endregion Public Properties
    }
}