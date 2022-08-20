using MediatR;
using TravelBook.Core.Events;
using TravelBook.Web.Service;

namespace TravelBook.Web.DomainEventHandlers;

public class PhotoAlbumAddedDomainEventHandler : INotificationHandler<PhotoAlbumAddedDomainEvent>
{
    private readonly ILogger<PhotoAlbumAddedDomainEvent> _logger;
    private readonly IFilesService _filesService;
    public PhotoAlbumAddedDomainEventHandler(ILogger<PhotoAlbumAddedDomainEvent> logger,
                                             IFilesService filesService)
    {
        _logger = logger;
        _filesService = filesService;
    }
    public Task Handle(PhotoAlbumAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"New album - Title: {notification.PhotoAlbum.Title})" +
                         $" was added by user with id:{notification.UserId}");

        _filesService.CreateAlbumFolder(notification.UserId, notification.PhotoAlbum.Id);

        return Task.CompletedTask;
    }
}
