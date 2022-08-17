using MediatR;
using TravelBook.Core.Events;
using TravelBook.Web.Service;

namespace TravelBook.Web.DomainEventHandlers;

public class PhotoAlbumDeletedDomainEventHandler : INotificationHandler<PhotoAlbumDeletedDomainEvent>
{
    private readonly ILogger<PhotoAlbumDeletedDomainEvent> _logger;
    private readonly IFilesService _filesService;
    public PhotoAlbumDeletedDomainEventHandler(ILogger<PhotoAlbumDeletedDomainEvent> logger,
                                             IFilesService filesService)
    {
        _logger = logger;
        _filesService = filesService;
    }
    public Task Handle(PhotoAlbumDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"Album - (Travel: {notification.Travel.Place.ToString()}," +
                         $" Title: {notification.PhotoAlbum.Title})" +
                         $" was deleted by user with id:{notification.UserId}");

        _filesService.DeleteAlbumFolder(notification.UserId, notification.PhotoAlbum.Id);

        return Task.CompletedTask;
    }
}
