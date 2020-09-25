using System;

namespace WebApplicationWithDummyAuthentication.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}