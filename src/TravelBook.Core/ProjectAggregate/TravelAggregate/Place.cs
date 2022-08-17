
namespace TravelBook.Core.ProjectAggregate;

public class Place: ValueObject
{
    public string City { get; private set; }
    public string Country { get; private set; }

    public Place() { }

    public Place(string city, string country)
    {
        City = city;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return City;
    }
    public override string ToString()
    {
        return $"{this.Country}, {this.City}";
    }
}
