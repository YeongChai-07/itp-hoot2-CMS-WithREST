/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;*/
using System.Linq;

using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class AccountsDataGateway:DataGateway<Accounts>
    {
        public Accounts findByUserName(string userName)
        {
            var userInfo = (from account in dbData
                            where account.user_name.ToLower().Equals(userName)
                           select account);

            if(userInfo.Count() == 0)
            {
                return null;
            }

            return userInfo.First();
        }
    }
}