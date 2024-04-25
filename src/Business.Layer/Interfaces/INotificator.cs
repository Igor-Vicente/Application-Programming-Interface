namespace Business.Layer.Interfaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<string> GetAllNotifications();
        void Handler(string notification);
    }
}
