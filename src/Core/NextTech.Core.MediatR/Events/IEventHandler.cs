using MediatR;

namespace NextTech.Core.MediatR.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}
