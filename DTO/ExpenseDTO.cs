using System.ComponentModel.DataAnnotations;

namespace FinanceApp.DTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = null;
        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Amount needs to be greated than zero")]
        public double Amount { get; set; }
        [Required]
        public string Category { get; set; } = null;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
