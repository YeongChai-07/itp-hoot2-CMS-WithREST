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