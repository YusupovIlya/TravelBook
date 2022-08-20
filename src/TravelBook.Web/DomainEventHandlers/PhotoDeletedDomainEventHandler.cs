using MediatR;
using TravelBook.Core.Events;
using TravelBook.Web.Service;

namespace TravelBook.Web.DomainEventHandlers;

public class PhotoDeletedDomainEventHandler : INotificationHandler<PhotoDeletedDomainEvent>
{
    private readonly ILogger<PhotoDeletedDomainEvent> _logger;
    private readonly IFilesService _filesService;
    public PhotoDeletedDomainEventHandler(ILogger<PhotoDeletedDomainEvent> logger,
                                             IFilesService filesService)
    {
        _logger = logger;
        _filesService = filesService;
    }
    public Task Handle(PhotoDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Photo - {notification.Photo.Title})" +
                         $" was deleted by user with id:{notification.UserId}");

        _filesService.DeletePhoto(notification.Photo.ImagePath);

        return Task.CompletedTask;
    }
}
