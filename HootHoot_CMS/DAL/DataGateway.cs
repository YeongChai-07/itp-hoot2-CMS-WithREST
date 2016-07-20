using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace HootHoot_CMS.DAL
{
    public class DataGateway<T>:IDataGateway<T>where T:class
    {
        internal HootHootDbContext dbContext = new HootHootDbContext();
        internal DbSet<T> dbData = null;

        public DataGateway()
        {
            this.dbData = dbContext.Set<T>();
        }

        public virtual IEnumerable<T> SelectAll()
        {
            return dbData.AsEnumerable<T>();
        }
    
        public virtual T SelectById(int? id)
        {
            return dbData.Find(id);
        }

        public void Insert(T obj)
        {
            dbData.Add(obj);
            dbContext.SaveChanges();
        }

        public void Update(T obj)
        {
            dbContext.Entry(obj).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Delete(T obj)
        {
            dbData.Remove(obj);
            dbContext.Entry(obj).State = EntityState.Deleted;

            dbContext.SaveChanges();
            
        }

    }
}