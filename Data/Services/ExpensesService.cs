using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data.Services
{
    public class ExpensesService:IExpensesService
    {
        private readonly FinanceApplicationContext financeApplicationContext;
        public ExpensesService(FinanceApplicationContext financeApplicationContext)
        {
            this.financeApplicationContext = financeApplicationContext;
        }
        
        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            var expenses = await financeApplicationContext.Expenses.ToListAsync();
            return expenses;
        }
        public async Task AddExpense(Expense expense)
        {
            financeApplicationContext.Expenses.Add(expense);
            await financeApplicationContext.SaveChangesAsync();

        }
        public IQueryable GetChartData()
        {
            var data = financeApplicationContext.Expenses
                            .GroupBy(e => e.Category)
                            .Select(g => new
                            {
                                Category = g.Key,
                                Total = g.Sum(e => e.Amount)
                            });
            return data;

        }

    }
}
