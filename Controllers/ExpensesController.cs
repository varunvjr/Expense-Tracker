using FinanceApp.Data.Services;
using FinanceApp.DTO;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService expensesService;
        public ExpensesController(IExpensesService expensesService)
        {
            this.expensesService = expensesService;
        }
        public async Task<IActionResult> Index()
        {
            var expenses = await expensesService.GetAllExpenses();
            return View(expenses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await expensesService.AddExpense(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }
        public IActionResult GetChart()
        {
            var chartData = expensesService.GetChartData();
            return Json(chartData);
        }

        public async Task<IActionResult>Delete(int id)
        {
            bool found = await expensesService.DeleteExpense(id);
            
            return found?RedirectToAction("Index"): NotFound("Expense not found");
        }

        public async Task<IActionResult>Update(int id)
        {
            Expense? expense = await expensesService.FindExpense(id);
            if(expense == null)
            {
                return NotFound("Expense Not Found");
            }
            var expenseDTO = new ExpenseDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Category = expense.Category
            };
            return View(expenseDTO);
        }
        [HttpPost]
        public async Task<IActionResult>Update(ExpenseDTO expenseDTO)
        {
     
            if (ModelState.IsValid)
            {
                await expensesService.UpdateExpense(expenseDTO);
                return RedirectToAction("Index");
            }
            return NotFound("Inproper update");

        }
       
    }
}
