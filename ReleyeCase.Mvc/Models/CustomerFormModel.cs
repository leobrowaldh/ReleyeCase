using ReleyeCase.Data.DbModels;

namespace ReleyeCase.Mvc.Models;

public class CustomerFormModel
{
    public Customer NewCustomer { get; set; } = new Customer();
    public Address NewAddress { get; set; } = new Address();
}
