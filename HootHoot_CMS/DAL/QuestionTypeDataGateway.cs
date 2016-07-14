using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HootHoot_CMS.DAL
{
    public class QuestionTypeDataGateway:DataGateway<Models.QuestionType>
    {
        public IEnumerable<string> GetAllQuestionTypes()
        {
            var optionTypes = (from qnsType in dbData
                               select qnsType.questiontype);

            return optionTypes.AsEnumerable();
        }
    }
}