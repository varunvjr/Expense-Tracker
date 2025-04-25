using FinanceApp.DTO;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.Data.Services
{
    public interface IExpensesService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task AddExpense(Expense expense);
        IQueryable GetChartData();

        Task<bool> DeleteExpense(int id);

        Task<Expense?> FindExpense(int id);
        Task<bool> UpdateExpense(ExpenseDTO expense);
    }
}