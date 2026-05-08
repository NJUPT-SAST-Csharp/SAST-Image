using Domain.Event;

namespace Domain.Category.Events;

public sealed record class CategoryCreatedEvent(
    CategoryId Id,
    CategoryName Name,
    CategoryDescription Description
) : DomainEventBase;
