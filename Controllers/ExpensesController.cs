using FinanceApp.Data.Services;
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
    }
}
