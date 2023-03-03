using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var files = new[] { "file1.txt", "file2.txt", "file3.txt" };
        var words = new List<string>();

        Func<string, IEnumerable<string>> tokenizer = text =>
            text.Split(new[] { ' ', '.', ',', ';', ':', '-', '\n', '\r' },
            StringSplitOptions.RemoveEmptyEntries);

        Func<IEnumerable<string>, IDictionary<string, int>> calculator = words =>
            words.GroupBy(word => word.ToLower())
            .ToDictionary(group => group.Key, group => group.Count());

        Action<IDictionary<string, int>> displayStatistics = wordFrequencies =>
        {
            Console.WriteLine("Word frequency statistics:");
            foreach (var pair in wordFrequencies.OrderByDescending(pair => pair.Value))
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        };

       
        foreach (var file in files)
        {
            var text = File.ReadAllText(file);
            var fileWords = tokenizer(text);
            words.AddRange(fileWords);
        }

      
        var wordFrequencies = calculator(words);

       
        displayStatistics(wordFrequencies);
    }
}
