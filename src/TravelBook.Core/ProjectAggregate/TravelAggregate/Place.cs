
namespace TravelBook.Core.ProjectAggregate;

public class Place: ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }

    public Place() { }

    public Place(string street, string city, string country)
    {
        Street = street;
        City = city;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return City;
        yield return Street;
    }
}
