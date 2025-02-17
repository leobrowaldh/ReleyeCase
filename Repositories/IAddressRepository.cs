using ReleyeCase.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface IAddressRepository
{
    public Task<List<Address>> GetAddresses();
    public Task<Address> GetAddress(Guid id);
    public Task<Address> CreateAddress(Address address);
    public Task<Address> UpdateAddress(Address address);
    public Task<Address> DeleteAddress(Guid id);
}
