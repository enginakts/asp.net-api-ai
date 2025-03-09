namespace Project02_ApiConsumeUI.DTOS;

public class CreateCustomerDTO
{
     public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurName { get; set; }
        public decimal CustomerBalance { get; set; }
}
