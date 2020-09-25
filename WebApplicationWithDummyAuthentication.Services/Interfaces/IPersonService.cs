using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationWithDummyAuthentication.Domain.Entities;

namespace WebApplicationWithDummyAuthentication.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAsync();

        Task<Person> GetAsync(Guid id);
    }
}