using HgSoftware.InsertCreator.Extensions;
using HgSoftware.InsertCreator.Model;
using System;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class MinistryGridViewModel : ObservableObject, IInsertData
    {
        #region Public Constructors

        public MinistryGridViewModel()
        {
            ForeName = "";
            SureName = "";
            Function = "";
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<string> OnUpdateFunction;

        #endregion Public Events

        #region Public Properties

        public string FirstLine => FullName.LimitString(20);

        public string ForeName
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

        public string Function
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnUpdateFunction?.Invoke(this, value);
            }
        }

        public string SecondLine => Function.LimitString(20);

        public string SureName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnUpdateFunction?.Invoke(this, Function);
            }
        }

        #endregion Public Properties
    }
}