namespace TravelBook.Core.Events;

public class TravelDeletedDomainEvent : INotification
{
    public string UserId { get; private set; }
    public int TravelId { get; private set; }
    public int[] PhotoAlbumsId { get; private set; }
    public TravelDeletedDomainEvent(string userId, int travelId, int[] photoAlbumsId)
    {
        UserId = userId;
        TravelId = travelId;
        PhotoAlbumsId = photoAlbumsId;
    }
}