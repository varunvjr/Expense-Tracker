using FinanceApp.Models;
namespace FinanceApp.Data.Services
{
    public interface IExpensesService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task AddExpense(Expense expense);
        IQueryable GetChartData();
    }
}