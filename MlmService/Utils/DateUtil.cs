namespace MlmService.Helper;

public static class DateUtil
{
    public static DateTimeOffset DateConvertServerTimeToClientTime(DateTime serverDateTime, TimeSpan clientOffset)
    {
        var serverDateTimeOffset = GetServerTimeOffset(serverDateTime);
        return serverDateTimeOffset.ToOffset(clientOffset);
    }

    private static DateTimeOffset GetServerTimeOffset(DateTime serverDateTime)
    {
        return DateTime.SpecifyKind(serverDateTime, DateTimeKind.Local);
    }
}