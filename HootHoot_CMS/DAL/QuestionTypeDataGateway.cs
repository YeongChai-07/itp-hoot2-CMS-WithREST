using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class QuestionTypeDataGateway:DataGateway<QuestionType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<QuestionType> SelectAll_Joint()
        {
            return dbData.Include(qt => qt.question).AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllQuestionTypes()
        {
            var optionTypes = (from qnsType in dbData
                               select qnsType.questiontype);

            return optionTypes.AsEnumerable();
        }
    }
}