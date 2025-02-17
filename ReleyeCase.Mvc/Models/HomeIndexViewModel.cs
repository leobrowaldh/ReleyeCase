using ReleyeCase.Data.DbModels;

namespace ReleyeCase.Mvc.Models;

public class HomeIndexViewModel
{
    public List<Customer> Customers { get; set; } = new List<Customer>();
    public Customer NewCustomer { get; set; } = new Customer();
}
