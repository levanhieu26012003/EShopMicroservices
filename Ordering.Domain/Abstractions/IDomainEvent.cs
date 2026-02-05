using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification // đánh dấu là notify, nhằm thông báo cho các
                                                    //listener
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
