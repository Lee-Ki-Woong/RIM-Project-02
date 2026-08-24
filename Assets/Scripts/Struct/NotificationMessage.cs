public readonly struct NotificationMessage
{
    public readonly string Text;
    public readonly NotificationType Type;

    public NotificationMessage(string text, NotificationType type)
    {
        Text = text;
        Type = type;
    }

    public static NotificationMessage Empty()
    {
        NotificationMessage empty = new(string.Empty, NotificationType.None);

        return empty;
    }
}