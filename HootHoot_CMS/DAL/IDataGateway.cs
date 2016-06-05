using System;
using System.Collections.Generic;
/*using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace HootHoot_CMS.DAL
{
    interface IDataGateway<T> where T:class
    {
        IEnumerable<T> SelectAll();
        //NOTE: Nullable input parameter
        T SelectById(int? id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);


    }
}
