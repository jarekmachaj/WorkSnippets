using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibExtensions
{
    public static class CsvHelperExtensions
    {
        public static IList<T> GetRecords<T>(Encoding encoding, string delimiter, string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(delimiter);

            if (String.IsNullOrWhiteSpace(delimiter))
                throw new ArgumentNullException(delimiter);

            var config = new CsvConfiguration { Encoding = encoding, Delimiter = delimiter };

            using (var reader = new StreamReader(Path.GetFullPath(filePath), encoding))
            {
                try
                {
                    var csv = new CsvReader(reader, config);
                    return csv.GetRecords<T>().ToList();
                }
                catch (Exception exception)
                {
                    throw;
                }
            }

            return null;
        }

        public static void SaveRecordsToCsv<T>(this IList<T> records, Encoding encoding, string delimiter, string outputFilePath)
        {
            if (String.IsNullOrWhiteSpace(outputFilePath))
                throw new ArgumentNullException(delimiter);

            if (String.IsNullOrWhiteSpace(delimiter))
                throw new ArgumentNullException(delimiter);

            var config = new CsvConfiguration { Encoding = encoding, Delimiter = delimiter };

            using (var writer = new StreamWriter(outputFilePath, false, encoding))
            {
                try
                {
                    var csw = new CsvWriter(writer, config);
                    csw.WriteRecords(records);
                }
                catch (Exception exception)
                {
                    throw;
                }
            }
        }
    }
}
