using DALBank.Data;
using DALBank.Model;

namespace DALBank.DAL
{
    public class AccountRepository
    {
        private DALBankContext db { get; set; }
        public AccountRepository(DALBankContext context)
        {
            db = context;
        }

        // read
        public ICollection<Account> GetAllAccounts()
        {
            return db.Account.ToList();
        }

        public Account GetAccountById(int id)
        {
            return db.Account.Find(id);
        }

        // create
        public void CreateAccount(Account account)
        {
            db.Account.Add(account);
        }

        // update
        public void UpdateAccount(Account account)
        {
            db.Account.Update(account);
        }

        // delete
        public void DeleteAccount(Account account)
        {
            db.Account.Remove(account);
        }

        //save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
