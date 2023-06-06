using MediatR;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Events
{
    public class OnUserPost : INotification
    {
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}
