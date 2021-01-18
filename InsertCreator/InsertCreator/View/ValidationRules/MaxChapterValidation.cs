using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HgSoftware.InsertCreator.View.ValidationRules
{
    class MaxChapterValidation : ValidationRule
    {

        public string MustEndWith { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (str == null)
            {
                return new ValidationResult(false, "Please enter some text");
            }
            if (!str.EndsWith(MustEndWith))
            {
                return new ValidationResult(false, String.Format("Text must end with '{0}'", MustEndWith));
            }
            return new ValidationResult(true, null);

        }


    }
}
