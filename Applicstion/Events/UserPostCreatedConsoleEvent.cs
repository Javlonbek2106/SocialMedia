using MediatR;

namespace Application.Events
{
   

    public class UserPostCreatedConsoleEventHandler : INotificationHandler<OnUserPost>
    {
        public async Task Handle(OnUserPost notification, CancellationToken cancellationToken)
        {
            await SendNotification(notification.UserName, notification.Text);
        }

        private async Task SendNotification(string userName, string text)
        {
            await Console.Out.WriteLineAsync($"Xabar yuborildi: Foydalanuvchi: {userName}, Matn: {text}");
        }
    }
}
