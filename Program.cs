using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DALBank.Data;
using DALBank.Model;
using DALBank.DAL;
using DALBank.BLL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DALBankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DALBankContext") ?? throw new InvalidOperationException("Connection string 'DALBankContext' not found.")));

var app = builder.Build();


// BASIC CRUD
#region Accounts
app.MapGet("/accounts", (DALBankContext db) =>
{
    AccountRepository repo = new AccountRepository(db);
    AccountBusinessLogic accountBL = new AccountBusinessLogic(repo);
    return accountBL.GetAllAccounts();
});

app.MapGet("/accounts/{id}", (DALBankContext db, int id) =>
{
    AccountRepository repo = new AccountRepository(db);
    AccountBusinessLogic accountBL = new AccountBusinessLogic(repo);
    return accountBL.GetAccountById(id);
    // create a means of handling a bad request
});

app.MapPost("/accounts", (DALBankContext db, Account newAccount) =>
{
    AccountRepository repo = new AccountRepository(db);
    repo.Add(newAccount);
    repo.Save();
});

app.MapPost("/accounts/{id}", (DALBankContext db, Account updatedAccount) =>
{
    AccountRepository repo = new AccountRepository(db);
    repo.Update(updatedAccount);
    repo.Save();
});

app.MapPost("/accounts/{id}/withdraw", (int id, double amount, DALBankContext db) =>
{
    AccountBusinessLogic accountBL = new AccountBusinessLogic(new AccountRepository(db));

    accountBL.Withdraw(id, amount);
});

app.MapPost("/accounts/{id}/deposit", (int id, double amount, DALBankContext db) =>
{

});

/*
app.MapDelete("/accounts/{id}", (DALBankContext db, Account accountToDelete) =>
{
    AccountRepository repo = new AccountRepository(db);
    repo.DeleteAccount(accountToDelete);
    repo.Save();
});*/
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

app.MapGet("/customers/totalbalance/{id}", (DALBankContext db, int id) =>
{
    AccountBusinessLogic accountBL = new AccountBusinessLogic(new AccountRepository(db));
    CustomerBusinessLogic customerBL = new CustomerBusinessLogic(new CustomerRepository(db));

    return customerBL.GetTotalAccountBalance(id, accountBL);
});

//POST
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

app.MapPost("/customers/closeall/{id}", (DALBankContext db, int id) =>
{
    CustomerBusinessLogic customerBL = new CustomerBusinessLogic(new CustomerRepository(db));
    AccountBusinessLogic accountBL = new AccountBusinessLogic(new AccountRepository(db));

    customerBL.CloseAllCustomerAccounts(id, accountBL);
});

// DELETE
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