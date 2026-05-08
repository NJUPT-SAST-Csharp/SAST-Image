using Domain.Event;

namespace Domain.Category.Events;

public sealed record class CategoryUpdatedEvent(
    CategoryId Id,
    CategoryName? Name,
    CategoryDescription? Description
) : DomainEventBase;
