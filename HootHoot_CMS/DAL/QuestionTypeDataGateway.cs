﻿using System.Collections.Generic;
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
            // Retrieving the Dataset returned from the data source using the "Eager-Loading" strategy
            // More details are availabel from:
            // http://www.codeproject.com/Articles/732426/Deferred-Execution-Vs-Lazy-Loading-Vs-Eager-Loadin
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