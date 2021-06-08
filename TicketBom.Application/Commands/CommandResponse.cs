using System.Collections.Generic;
using Flunt.Notifications;

namespace TicketBom.Application.Commands
{
    public class CommandResponse
    {
        public IReadOnlyCollection<Notification> Notifications { get; protected set; }

        public bool Valid => Notifications.Count == 0;
        public bool Invalid => Notifications.Count != 0;

        public string Message { get; private set; }

        public CommandResponse(IReadOnlyCollection<Notification> notifications, string message)
            : this(notifications)
        {
            Message = message;
        }

        public CommandResponse(IReadOnlyCollection<Notification> notifications)
        {
            Notifications = notifications;
        }
    }

    public class CommandResponse<TData> : CommandResponse
    {
        public TData Data { get; }

        public CommandResponse(TData data, IReadOnlyCollection<Notification> notifications, string message)
            : base(notifications, message)
        {
            Data = data;
        }

        public CommandResponse(TData data, IReadOnlyCollection<Notification> notifications)
            : this(data, notifications, null)
        {
        }

        public CommandResponse(TData data)
            : this(data, new List<Notification>(), null)
        {
        }
    }
}
