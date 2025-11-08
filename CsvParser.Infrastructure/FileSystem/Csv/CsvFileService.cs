using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvParser.Application.Abstractions;
using CsvParser.Infrastructure.FileSystem.Csv.CsvDtoMaps;

namespace CsvParser.Infrastructure.FileSystem.Csv;

public class CsvFileService : IFileService
{
    private readonly CsvConfiguration _readConfiguration = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        TrimOptions = TrimOptions.Trim,
        MissingFieldFound = null
    };

    private readonly CsvConfiguration _writeChunkConfiguration = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = false,
        TrimOptions = TrimOptions.Trim,
        MissingFieldFound = null
    };
    
    public async IAsyncEnumerable<T> ReadStreamAsync<T>(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, _readConfiguration);
        csv.Context.RegisterClassMap<TripCsvDtoMap>();

        await foreach (var record in csv.GetRecordsAsync<T>())
        {
            yield return record;
        }
    }

    public async Task WriteRangeAsync<T>(string path, IEnumerable<T> data, int chunkSize = 5000)
    {
        await using var writer = new StreamWriter(path);
        await using var csv = new CsvWriter(writer, _writeChunkConfiguration);

        csv.WriteHeader<T>();
        await csv.NextRecordAsync();

        foreach (var record in data.Chunk(chunkSize))
        {
            await csv.WriteRecordsAsync(record);
            await csv.FlushAsync();
        }
    }
}