namespace DALBank.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public Boolean IsActive { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
