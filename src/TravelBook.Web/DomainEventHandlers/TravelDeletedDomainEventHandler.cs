using TravelBook.Core.Events;

namespace TravelBook.Web.DomainEventHandlers;
public class TravelDeletedDomainEventHandler : INotificationHandler<TravelDeletedDomainEvent>
{
    private readonly ILogger<TravelDeletedDomainEvent> _logger;
    private readonly IFilesService _filesService;
    public TravelDeletedDomainEventHandler(ILogger<TravelDeletedDomainEvent> logger,
                                             IFilesService filesService)
    {
        _logger = logger;
        _filesService = filesService;
    }

    public Task Handle(TravelDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Travel with id {notification.TravelId}" +
                         $" was deleted by user with id:{notification.UserId}");

        foreach (int id in notification.PhotoAlbumsId)
            _filesService.DeleteAlbumFolder(notification.UserId, id);

        return Task.CompletedTask;
    }
}