namespace HootHoot_CMS.Models
{
    public class QuestionsViewModel
    {
        public string option_1 { get; set; }
        public string option_2 { get; set; }
        public string option_3 { get; set; }
        public string option_4 { get; set; }

        public void assignsOption_Value(byte optionNo, string value)
        {
            setValue_ForOption(optionNo, value);
        }

        private void setValue_ForOption(byte optionNo, string value)
        {
            if(optionNo == 1)
            {
                option_1 = value;
            }
            else if(optionNo == 2)
            {
                option_2 = value;
            }
            else if(optionNo == 3)
            {
                option_3 = value;
            }
            else if (optionNo == 4)
            {
                option_4 = value;
            }
        }
    }
}