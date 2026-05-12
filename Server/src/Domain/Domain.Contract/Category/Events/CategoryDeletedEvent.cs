using Domain.Event;

namespace Domain.Category.Events;

public sealed record class CategoryDeletedEvent(CategoryId Id) : DomainEventBase;
