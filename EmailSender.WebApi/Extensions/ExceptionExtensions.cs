namespace EmailSender.WebApi.Extensions;

public static class ExceptionExtensions
{
    public static string ConcatInnerMessages(this Exception error)
    {
        var e = error;
        var messages = new List<string>();
        while (e != null)
        {
            messages.Add(e.Message);
            e = e.InnerException;
        }

        return string.Join(" ", messages);
    }
}