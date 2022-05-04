#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DALBank.Model;

namespace DALBank.Data
{
    public class DALBankContext : DbContext
    {
        public DALBankContext (DbContextOptions<DALBankContext> options)
            : base(options)
        {
        }

        public DbSet<DALBank.Model.Account> Account { get; set; }
        public DbSet<DALBank.Model.Customer> Customer { get; set; }
    }
}
