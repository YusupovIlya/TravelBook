namespace TravelBook.UnitTests;

public class PhotoAlbumTests
{
    private string _testName = "With family";
    private int _testTravelId = 1;
    private Photo[]? _testPhotos;
    private PhotoAlbum? _testPhotoalbum;

    private PhotoAlbum CreateTestAlbum()
    {
        return new PhotoAlbum(_testName, _testTravelId);
    }

    private Photo[] GeneratePhotosArray()
    {
        return new Photo[]{ 
            new Photo("/img1.jpg"),
            new Photo("/img2.jpg", "photo1", 1),
            new Photo("/img3.jpg")
        };
    }

    [Fact]
    public void InitializesTitle()
    {
        // act
        _testPhotoalbum = CreateTestAlbum();

        // assert
        Assert.Equal(_testName, _testPhotoalbum.Title);
    }

    [Fact]
    public void InitializesTravel()
    {
        // act
        _testPhotoalbum = CreateTestAlbum();

        // assert
        Assert.Equal(_testTravelId, _testPhotoalbum.TravelId);
    }

    [Fact]
    public void AddPhotos_NotEmptyCollection_ReturnsTrue()
    {
        // arrange
        _testPhotoalbum = CreateTestAlbum();
        _testPhotos = GeneratePhotosArray();

        // act
        _testPhotoalbum.AddPhotos(_testPhotos);

        // assert
        Assert.NotEmpty(_testPhotoalbum.Photos);
    }

    [Fact]
    public void AddPhotos_ContainsGivenPhoto_ReturnsTrue()
    {
        // arrange
        _testPhotoalbum = CreateTestAlbum();
        _testPhotos = GeneratePhotosArray();
        Photo testPhoto = new Photo("/img3.jpg");

        // act
        _testPhotoalbum.AddPhotos(_testPhotos);

        // assert
        Assert.Contains(testPhoto, _testPhotoalbum.Photos);
    }

    [Fact]
    public void RemovePhotos_IsEmptyCollection_ReturnsTrue()
    {
        // arrange
        _testPhotoalbum = CreateTestAlbum();
        _testPhotos = GeneratePhotosArray();

        // act
        _testPhotoalbum.AddPhotos(_testPhotos);
        _testPhotoalbum.RemovePhotos(
            new Photo("/img1.jpg"),
            new Photo("/img2.jpg", "photo1", 1),
            new Photo("/img3.jpg"));

        // assert
        Assert.Empty(_testPhotoalbum.Photos);
    }
}
