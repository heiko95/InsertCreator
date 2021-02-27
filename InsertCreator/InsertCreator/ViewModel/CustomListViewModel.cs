namespace HgSoftware.InsertCreator.ViewModel
{
    public class CustomListViewModel : ObservableObject
    {
        #region Public Constructors

        public CustomListViewModel()
        {
            TextLaneOne = "";
            TextLaneTwo = "";
        }

        #endregion Public Constructors

        #region Public Properties

        public string TextLaneOne
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public string TextLaneTwo
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        #endregion Public Properties
    }
}