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
        var existingAddress = CheckIfAddressExists(customer.Address);

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

    private static Address? CheckIfAddressExists(Address? address)
    {
        if (address == null)
        {
            return null;
        }

        // Check for the address in the cache:
        var cachedCustomer = customersCache?.Values.FirstOrDefault(c =>
            c.Address != null &&
            c.Address.Country.ToLower() == address.Country.ToLower() &&
            c.Address.City.ToLower() == address.City.ToLower() &&
            (c.Address.Street != null && address.Street != null && c.Address.Street.ToLower() == address.Street.ToLower()) &&
            (c.Address.ZipCode != null && address.ZipCode != null && c.Address.ZipCode.ToLower() == address.ZipCode.ToLower()));

        if (cachedCustomer != null)
        {
            return cachedCustomer.Address;
        }
        return null;
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
        //We wrap the result in a task to comply with the interface, that expects a task.
        return Task.FromResult(customersCache?.Values.ToList() ?? []);
    }

    public Task<Customer> UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }
}
