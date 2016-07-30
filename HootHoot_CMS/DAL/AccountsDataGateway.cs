using System.Linq;

using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class AccountsDataGateway:DataGateway<Accounts>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
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