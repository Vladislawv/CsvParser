namespace CsvParser.Application.Abstractions;

public interface IFileService
{
    IAsyncEnumerable<T> ReadStreamAsync<T>(string path);
    Task WriteRangeAsync<T>(string path, IEnumerable<T> data, int chunkSize = 5000);
}