namespace WebApplicationWithDummyAuthentication.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Person(string firstName, string LastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = LastName;
            this.Age = age;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public int Age { get; }
    }
}