using MediatR;
using TravelBook.Core.Events;
using TravelBook.Web.Service;

namespace TravelBook.Web.DomainEventHandlers;

public class NewUserRegisteredDomainEventHandler : INotificationHandler<NewUserRegisteredDomainEvent>
{
    private readonly ILogger<NewUserRegisteredDomainEvent> _logger;
    private readonly IFilesService _filesService;
    public NewUserRegisteredDomainEventHandler(ILogger<NewUserRegisteredDomainEvent> logger,
                                               IFilesService filesService)
    {
        _logger = logger;
        _filesService = filesService;
    }
    public Task Handle(NewUserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"New user ({notification.UserName}, {notification.UserId})" +
                         $" was successfully registered ");

        _filesService.CreateUserFolder(notification.UserId);

        return Task.CompletedTask;
    }
}
