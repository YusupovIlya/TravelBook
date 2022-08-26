namespace TravelBook.UnitTests.Core;

public class ArticleTests
{
    private string _testTitle = "First note";
    private string _testText = "Abcd";
    private int _testTravelId = 12;
    private Article _testArticle;

    private Article CreateTestArticle()
    {
        return new Article(_testTitle, _testText, _testTravelId);
    }
    [Fact]
    public void InitializesTitle()
    {
        // act
        _testArticle = CreateTestArticle();

        // assert
        Assert.Equal(_testTitle, _testArticle.Title);
    }

    [Fact]
    public void InitializesText()
    {
        // act
        _testArticle = CreateTestArticle();

        // assert
        Assert.Equal(_testText, _testArticle.Text);
    }
    [Fact]
    public void InitializesTravelId()
    {
        // act
        _testArticle = CreateTestArticle();

        // assert
        Assert.Equal(_testTravelId, _testArticle.TravelId);
    }
}
