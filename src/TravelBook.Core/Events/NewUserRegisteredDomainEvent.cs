namespace TravelBook.Core.Events;

public class NewUserRegisteredDomainEvent: INotification
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public NewUserRegisteredDomainEvent(string userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }
}
