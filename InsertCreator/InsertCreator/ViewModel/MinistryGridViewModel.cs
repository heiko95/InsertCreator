using System;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class MinistryGridViewModel : ObservableObject
    {
        #region Public Events

        public event EventHandler<string> OnUpdateFunction;

        #endregion Public Events

        #region Public Properties

        public MinistryGridViewModel()
        {
            ForeName = "";
            SureName = "";
            Function = "";
        }

        public string ForeName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnUpdateFunction?.Invoke(this, Function);
            }
        }

        public string Function
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnUpdateFunction?.Invoke(this, value);
            }
        }

        public string SureName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnUpdateFunction?.Invoke(this, Function);
            }
        }

        public string FullName
        {
            get { return $"{ForeName} {SureName}"; }
        }

        public string FullName2
        {
            get { return $"{SureName}, {ForeName}"; }
        }

        #endregion Public Properties

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (obj is MinistryGridViewModel other)
            {
                return this.ForeName == other.ForeName &&
                    this.SureName == other.SureName &&
                    this.Function == other.Function;
            }
            return false;
        }

        #endregion Public Methods
    }
}