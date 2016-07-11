﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HootHoot_CMS.DAL
{
    public class OptionTypeDataGateway:DataGateway<Models.OptionType>
    {
        public IEnumerable<string> GetAllOptionTypes()
        {
            var optionTypes = (from optionType in dbData
                               select optionType.optiontype);

            return optionTypes.AsEnumerable();
        }
    }
}