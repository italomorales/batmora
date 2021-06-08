using System;
using System.Collections.Generic;
using System.Text;
using Flunt.Notifications;

namespace TicketBom.Application.Commands
{
    public abstract class Command : Notifiable
    {
        public abstract void Validate();
    }
}
