using Microsoft.EntityFrameworkCore;
using ReleyeCase.Data;
using ReleyeCase.Data.DbModels;

namespace Repositories;

public class CustomerRepository(ReleyeDbContext _context) : ICustomerRepository
{
    public async Task<Customer> CreateCustomer(Customer customer)
    {
        // Check if the address already exists
        var existingAddress = await CheckIfAddressExists(customer.Address);

        if (existingAddress != null)
        {
            customer.Address = existingAddress;
        }
        customer.CreatedAt = DateTime.Now;
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    private async Task<Address?> CheckIfAddressExists(Address? address)
    {
        if (address == null)
        {
            return null;
        }

        return await _context.Addresses
            .FirstOrDefaultAsync(a =>
                a.Country.ToLower() == address.Country.ToLower() &&
                a.City.ToLower() == address.City.ToLower() &&
                (a.Street != null && address.Street != null && a.Street.ToLower() == address.Street.ToLower()) &&
                (a.ZipCode != null && address.ZipCode != null && a.ZipCode.ToLower() == address.ZipCode.ToLower()));
    }

    public Task<Customer> DeleteCustomer(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetCustomer(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Customer>> GetCustomers()
    {
        var customers = await _context.Customers.Include(c => c.Address).ToListAsync();
        return customers ?? [];
    }

    public Task<Customer> UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }
}
