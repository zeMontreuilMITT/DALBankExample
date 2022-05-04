using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DALBank.Data;
using DALBank.Model;
using DALBank.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DALBankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DALBankContext") ?? throw new InvalidOperationException("Connection string 'DALBankContext' not found.")));

var app = builder.Build();

// BASIC CRUD
#region Accounts



#endregion

#region Customers
app.MapGet("/customers", (DALBankContext db) =>
{
    CustomerRepository dal = new CustomerRepository(db);

    List<Customer> customers = dal.GetCustomers().ToList();
    customers = db.Customer.ToList();

    return dal.GetCustomers();
});

app.MapGet("/customers/{id}", (DALBankContext db, int id) =>
{
    CustomerRepository dal = new CustomerRepository(db);
    return dal.GetCustomerById(id);
});

app.MapPost("/customers", (DALBankContext db, Customer customer) =>
{
    CustomerRepository dal = new CustomerRepository(db);

    dal.AddCustomer(customer);
    dal.Save();
});

app.MapPost("/customers/{id}", (DALBankContext db, int id) =>
{
    Customer customer = db.Customer.Find(id);

    if(customer != null)
    {
        db.Customer.Update(customer);
        db.SaveChanges();
    } else
    {
        Results.BadRequest();
    }
});

app.MapDelete("/customers/{id}", (DALBankContext db, int id) =>
{
    Customer customer = db.Customer.Find(id);

    if (customer != null)
    {
        db.Customer.Remove(customer);
        db.SaveChanges();
    }
    else
    {
        Results.BadRequest();
    }
});
#endregion

app.Run();