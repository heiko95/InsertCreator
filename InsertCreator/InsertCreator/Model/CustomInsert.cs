using HgSoftware.InsertCreator.Extensions;

namespace HgSoftware.InsertCreator.Model
{
    public class CustomInsert : IInsertData
    {
        #region Public Constructors

        public CustomInsert(string firstLine, string secondLine)
        {
            LineOne = firstLine;
            LineTwo = secondLine;
        }

        #endregion Public Constructors

        #region Public Properties

        public string FirstLine => LineOne.LimitString(20);
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string SecondLine => LineTwo.LimitString(20);

        #endregion Public Properties
    }
}