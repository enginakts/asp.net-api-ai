namespace Project02_ApiConsumeUI.DTOS;

public class UpdateCustomerDTO
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerSurName { get; set; }
    public decimal CustomerBalance { get; set; }
}
