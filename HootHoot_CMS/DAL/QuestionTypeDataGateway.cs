using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class QuestionTypeDataGateway:DataGateway<QuestionType>
    {
        public IEnumerable<QuestionType> SelectAll_Joint()
        {
            return dbData.Include(qt => qt.question).AsEnumerable();
        }
        public IEnumerable<string> GetAllQuestionTypes()
        {
            var optionTypes = (from qnsType in dbData
                               select qnsType.questiontype);

            return optionTypes.AsEnumerable();
        }
    }
}