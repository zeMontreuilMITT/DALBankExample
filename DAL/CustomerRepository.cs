using DALBank.Data;
using DALBank.Model;

namespace DALBank.DAL
{
    public class CustomerRepository
    {
        private DALBankContext db { get; set; } 
        public CustomerRepository(DALBankContext context)
        {
            db = context;
        }

        public ICollection<Customer> GetCustomers()
        {
            return db.Customer.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return db.Customer.Find(id);
        }

        public void AddCustomer(Customer customer)
        {
            db.Customer.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            db.Customer.Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            db.Customer.Remove(customer);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
