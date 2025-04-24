using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FinanceApp.Data
{
    public class FinanceApplicationContext:DbContext
    {
        public FinanceApplicationContext(DbContextOptions<FinanceApplicationContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }

    }
}
