using System.Collections.Generic;
using System.IO;

namespace Gamanet.Models.ETL
{
    public class CSVReader
    {
        public static List<string[]> GetListOfData(string path)
        {
            List<string[]> splits = new List<string[]>();
            foreach (var line in File.ReadAllLines(path))
                splits.Add(line.Split(';'));
            return splits;
        }
    }
}