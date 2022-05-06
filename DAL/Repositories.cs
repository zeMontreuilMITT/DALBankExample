using DALBank.Data;
using DALBank.Model;

namespace DALBank.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(Func<T, bool> firstFunction);
        ICollection<T> GetList(Func<T, bool> whereFunction);
        void Save();
    }

    public class AccountRepository: IGenericRepository<Account>
    {
        private DALBankContext context { get; set; }

        public AccountRepository(DALBankContext db)
        {
            context = db;
        }

        public void Add(Account entity)
        {
            context.Account.Add(entity);
        }

        public void Update(Account entity)
        {
            context.Account.Update(entity);
        }

        public void Delete(Account entity)
        {
            context.Account.Remove(entity);
        }

        public Account Get(Func<Account, bool> firstFunction)
        {
            return context.Account.First(firstFunction);
            // context.Account.First(a => return a.Id == id);
        }

        public ICollection<Account> GetList(Func<Account, bool> whereFunction)
        {
            return context.Account.Where(whereFunction).ToList()
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
