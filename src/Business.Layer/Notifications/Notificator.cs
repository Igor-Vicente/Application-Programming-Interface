using Business.Layer.Interfaces;

namespace Business.Layer.Notifications
{
    public class Notificator : INotificator
    {
        private List<string> _notifications;

        public Notificator()
        {
            _notifications = new List<string>();
        }

        public void Handler(string notification) => _notifications.Add(notification);

        public bool HasNotification() => _notifications.Any();

        public List<string> GetAllNotifications() => _notifications;
    }
}
