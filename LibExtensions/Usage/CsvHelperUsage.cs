using System.Text;
using Extensions;

namespace LibExtensions.Usage
{
    public static class CsvHelperUsage
    {
        public class SampleClass1
        {
            public string Property1 { get; set; }
            public string Property2 { get; set; }
        }

        public class SampleClass2
        {
            public string Property1 { get; set; }
            public string Property2 { get; set; }
        }

        public static void CsvListsMerge()
        {
            var standardMenuItems = CsvHelperExtensions.GetRecords<SampleClass1>(Encoding.UTF8, "|", "sample.csv");
            var newIconsList = CsvHelperExtensions.GetRecords<SampleClass2>(Encoding.UTF8, "|", "sample2.csv");

            var merged = standardMenuItems.Merge(newIconsList, input => input.Property1, output => output.Property1, input => input.Property2, output => output.Property2);
            merged.SaveRecordsToCsv(Encoding.UTF8, "|", "merged.csv");
        }
    }
}
