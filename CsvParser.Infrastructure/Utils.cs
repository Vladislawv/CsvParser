namespace CsvParser.Infrastructure;

public static class Utils
{
    private const short BarLength = 30;
    
    public static void LogProgressToConsole(int processed, int total)
    {
        var percent = total > 0 ? processed * 100.0 / total : 100.0;
        var filled = (int)(percent / (100.0 / BarLength));

        Console.CursorLeft = 0;
        Console.Write($"[{new string('#', filled)}{new string('-', BarLength - filled)}] {percent,5:F1}% ({processed}/{total})");
    }
}