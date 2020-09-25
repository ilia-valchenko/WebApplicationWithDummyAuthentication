using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationWithDummyAuthentication.Domain.Entities;
using WebApplicationWithDummyAuthentication.Services.Interfaces;

namespace WebApplicationWithDummyAuthentication.Services
{
    public class PersonService : IPersonService
    {
        public async Task<IEnumerable<Person>> GetAsync()
        {
            return await Task.Run(() => (IEnumerable<Person>)new[]
            {
                new Person("John", "Doe", 34)
            });
        }

        public async Task<Person> GetAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                return new Person("John", "Doe", 34);
            });
        }
    }
}