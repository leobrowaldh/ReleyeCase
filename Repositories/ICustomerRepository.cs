using ReleyeCase.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface ICustomerRepository
{
    public Task<List<Customer>> GetCustomers();
    public Task<Customer> GetCustomer(Guid id);
    public Task<Customer> CreateCustomer(Customer customer);
    public Task<Customer> UpdateCustomer(Customer customer);
    public Task<Customer> DeleteCustomer(Guid id);
}
