namespace TravelBook.UnitTests;

public class PlaceTests
{
    private string _testCountry = "Japan";
    private string _testCity = "Tokio";
    private Place _testPlace;

    private Place CreateTestPlace()
    {
        return new Place(_testCity, _testCountry);
    }

    [Fact]
    public void InitializesCountry()
    {
        // act
        _testPlace = CreateTestPlace();

        // assert
        Assert.Equal(_testCountry, _testPlace.Country);
    }

    [Fact]
    public void InitializesCity()
    {
        // act
        _testPlace = CreateTestPlace();

        // assert
        Assert.Equal(_testCity, _testPlace.City);
    }

    [Fact]
    public void ToString_ReturnValidString()
    {
        // act
        _testPlace = CreateTestPlace();

        // assert
        Assert.Equal("Japan, Tokio", _testPlace.ToString());
    }
}
