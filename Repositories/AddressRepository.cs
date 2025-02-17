using ReleyeCase.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class AddressRepository : IAddressRepository
{
    public Task<Address> CreateAddress(Address address)
    {
        throw new NotImplementedException();
    }

    public Task<Address> DeleteAddress(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Address> GetAddress(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Address>> GetAddresses()
    {
        throw new NotImplementedException();
    }

    public Task<Address> UpdateAddress(Address address)
    {
        throw new NotImplementedException();
    }
}
