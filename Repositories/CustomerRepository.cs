using Microsoft.EntityFrameworkCore;
using ReleyeCase.Data;
using ReleyeCase.Data.DbModels;
using System.Collections.Concurrent;

namespace Repositories;

public class CustomerRepository: ICustomerRepository
{
    private static ConcurrentDictionary<Guid, Customer>? customersCache;
    private readonly ReleyeDbContext _context;

    public CustomerRepository(ReleyeDbContext context)
    {
        _context = context;

        // Pre-load customers from database and convert to a thread-safe ConcurrentDictionary.
        if (customersCache is null)
        {
            customersCache = new ConcurrentDictionary<Guid, Customer>(
            _context.Customers.Include(c => c.Address).ToDictionary(c => c.CustomerId));
        }
    }
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

        if (customersCache is null) return customer;
        // If the customer is new, add it to cache, else
        // call UpdateCache method.
        return customersCache.AddOrUpdate(customer.CustomerId, customer, UpdateCache);
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

    private Customer UpdateCache(Guid id, Customer c)
    {
        Customer? old;
        if (customersCache is not null)
        {
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
        }
        return null!;
    }

    public Task<Customer> DeleteCustomer(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetCustomer(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Customer>> GetCustomers()
    {
        return Task.FromResult(customersCache is null
                    ? new List<Customer>() : customersCache.Values.ToList());
    }

    public Task<Customer> UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }
}
