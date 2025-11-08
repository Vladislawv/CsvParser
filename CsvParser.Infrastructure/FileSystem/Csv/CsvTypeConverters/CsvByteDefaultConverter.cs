using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CsvParser.Infrastructure.FileSystem.Csv.CsvTypeConverters;

public class ByteDefaultConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return (byte)0;
        }

        if (byte.TryParse(text, out var result))
        {
            return result;
        }

        throw new TypeConverterException(this, memberMapData, text, row.Context, $"Cannot convert '{text}' to byte.");
    }
}