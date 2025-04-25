using FinanceApp.Data;
using FinanceApp.DTO;
using FinanceApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        public async Task<bool> DeleteExpense(int id)
        { 
            var expense = financeApplicationContext.Expenses.Find(id);
            if (expense == null)
            {
                return false;
            }
            financeApplicationContext.Expenses.Remove(expense);
            await financeApplicationContext.SaveChangesAsync();
            return true;
        }
        public async Task<Expense?> FindExpense(int id)
        {
            var expense = await financeApplicationContext.Expenses.FindAsync(id);
            return expense;
        }
        public async Task<bool> UpdateExpense(ExpenseDTO expense)
        {
            var expenseModel = await financeApplicationContext.Expenses.FindAsync(expense.Id);
            if(expenseModel == null)
                return false;
            expenseModel.Description = expense.Description;
            expenseModel.Category = expense.Category;
            expenseModel.Amount = expense.Amount;
            await financeApplicationContext.SaveChangesAsync();
            return true;
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
