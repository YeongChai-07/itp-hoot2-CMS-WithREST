using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class OptionTypeDataGateway:DataGateway<OptionType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OptionType> SelectAll_Joint()
        {
            // Retrieving the Dataset returned from the data source using the "Eager-Loading" strategy
            // More details are availabel from:
            // http://www.codeproject.com/Articles/732426/Deferred-Execution-Vs-Lazy-Loading-Vs-Eager-Loadin
            return dbData.Include(ot => ot.question).AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllOptionTypes()
        {
            var optionTypes = (from optionType in dbData
                               select optionType.optiontype);

            return optionTypes.AsEnumerable();
        }
    }
}