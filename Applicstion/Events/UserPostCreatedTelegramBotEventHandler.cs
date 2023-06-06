using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace Application.Events
{
    public class UserPostCreatedTelegramBotEventHandler : INotificationHandler<OnUserPost>
    {
        private readonly ITelegramBotClient _botClient;
        private readonly string _chatId;

        public UserPostCreatedTelegramBotEventHandler(ITelegramBotClient botClient, IConfiguration configuration)
        {
            _botClient = botClient;
            _chatId = configuration["Telegram:ChatId"];
        }

        public async Task Handle(OnUserPost notification, CancellationToken cancellationToken)
        {
            await SendTelegramNotification(notification.UserName, notification.Text);
        }

        private async Task SendTelegramNotification(string userName, string text)
        {
            string message = $"Yangi post yaratildi:\nFoydalanuvchi: {userName}\nMatn: {text}";

            await _botClient.SendTextMessageAsync(_chatId, message);
        }
    }
}
