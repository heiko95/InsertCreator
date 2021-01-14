﻿using System;

namespace Liedeinblendung.ViewModel
{
    public class MinistryGridViewModel : ObservableObject
    {
        public event EventHandler<string> OnUpdateFunction;

        public string Function
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnUpdateFunction?.Invoke(this, value);
            }
        }

        public string ForeName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string SureName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

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
    }
}