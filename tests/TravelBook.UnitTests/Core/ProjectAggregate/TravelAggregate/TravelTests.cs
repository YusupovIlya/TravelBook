namespace TravelBook.UnitTests;

public class TravelTests
{
    private string _testUserId = Guid.NewGuid().ToString();
    private Place _testPlace = new Place("Paris", "France");
    private DateTime? _testDateStartTravel = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    private DateTime? _testDateFinishTravel = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10);
    private Travel _testTravel;

    private Travel CreateTestTravel()
    {
        return new Travel(_testUserId, _testPlace,
        _testDateStartTravel.Value, _testDateFinishTravel.Value);
    }

    [Fact]
    public void InitializesUserId()
    {
        // act
        _testTravel = CreateTestTravel();

        // assert
        Assert.Equal(_testUserId, _testTravel.UserId);
    }

    [Fact]
    public void InitializesPlace()
    {
        // act
        _testTravel = CreateTestTravel();

        // assert
        Assert.Equal(_testPlace, _testTravel.Place);
    }

    [Fact]
    public void InitializesPeriod()
    {
        // act
        _testTravel = CreateTestTravel();

        // assert
        Assert.Equal(_testDateStartTravel, _testTravel.DateStartTravel);
        Assert.Equal(_testDateFinishTravel, _testTravel.DateFinishTravel);
    }

    [Fact]
    public void PhotoAlbums_IsEmptyCollection_ReturnsTrue()
    {
        // arrange
        _testTravel = CreateTestTravel();

        // assert
        Assert.Empty(_testTravel.PhotoAlbums);
    }

    [Fact]
    public void Articles_IsEmptyCollection_ReturnsTrue()
    {
        // arrange
        _testTravel = CreateTestTravel();

        // assert
        Assert.Empty(_testTravel.Articles);
    }
}

