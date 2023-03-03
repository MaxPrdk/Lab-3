using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
       
        var inputFile = "transactions.csv";
        var outputFileFormat = "transactions_{0}.csv";
        var maxRecordsPerFile = 10;

        
        Func<string, DateTime> getDate = line => DateTime.ParseExact(line.Split(',')[0], "yyyy-MM-dd", null);
        Func<string, double> getAmount = line => double.Parse(line.Split(',')[1]);

        
        var totalsByDay = new Dictionary<DateTime, double>();

       
        using (var reader = new StreamReader(inputFile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var date = getDate(line);
                var amount = getAmount(line);

                if (!totalsByDay.ContainsKey(date))
                {
                    totalsByDay[date] = 0;
                }

                totalsByDay[date] += amount;
            }
        }

       
        var recordsWritten = 0;
        using (var writer = new StreamWriter(string.Format(outputFileFormat, DateTime.Now.ToString("yyyyMMdd"))))
        {
            foreach (var entry in totalsByDay.OrderBy(e => e.Key))
            {
                writer.WriteLine("{0},{1}", entry.Key.ToString("yyyy-MM-dd"), entry.Value);
                recordsWritten++;

                if (recordsWritten >= maxRecordsPerFile)
                {
                    writer.Flush();
                    writer.Close();

                    recordsWritten = 0;
                    writer = new StreamWriter(string.Format(outputFileFormat, DateTime.Now.ToString("yyyyMMdd")));
                }
            }
        }

        
        Action<DateTime, double> displayTotal = (date, total) => Console.WriteLine("{0}: {1:C}", date.ToString("yyyy-MM-dd"), total);

       
        foreach (var entry in totalsByDay.OrderBy(e => e.Key))
        {
            displayTotal(entry.Key, entry.Value);
        }
    }
}
