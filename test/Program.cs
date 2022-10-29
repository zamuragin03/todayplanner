using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace test
{
    class Program
    {
        static FileTaskEditor fl;
        static void Main(string[] args)
        {
            fl = new();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(fl.GetTask(i).Task1);
            }
        }
    }
    class FileTaskEditor
    {
        private readonly string path = @"C:\Users\zamur\OneDrive\Рабочий стол\planner\tasks.csv";
        private List<Task> tasks;
        private CsvConfiguration config;
        public FileTaskEditor()
        {
            config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };
        }

        public int GetLength(int row)
        {
            using (var reader = new StreamReader(path))
            {
                using (var csvreader = new CsvReader(reader, config))
                {
                    csvreader.Context.Configuration.DetectDelimiter = true;
                    tasks = csvreader.GetRecords<Task>().ToList();
                }
            }
            switch (row)
            {
                case 1:
                    return tasks[0].Task1.Length;
                case 2:
                    return tasks[0].Task2.Length;
                case 3:
                    return tasks[0].Task3.Length;
                case 4:
                    return tasks[0].Task4.Length;
            }
            return 0;
        }


        public Task GetTask(int i)
        {
            using (var reader = new StreamReader(path))
            {
                using (var csvreader = new CsvReader(reader, config))
                {
                    csvreader.Context.Configuration.DetectDelimiter = true;
                    tasks = csvreader.GetRecords<Task>().ToList();
                }
            }

            return tasks[i];
        }
        public void AddRow(Task task)
        {
            tasks = new List<Task>()
            {
                task
            };
            var newconfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";",
                Encoding = Encoding.UTF8
            };
            using (var stream = File.Open(path, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, newconfig))
            {
                csv.Context.RegisterClassMap<TaskMap>();
                csv.WriteRecords(tasks);
            }
        }

    }

    public class Task
    {
        [Name("Task1")]
        public string? Task1 { get; set; }
        [Name("Task2")]
        public string? Task2 { get; set; }
        [Name("Task3")]
        public string? Task3 { get; set; }
        [Name("Task4")]
        public string? Task4 { get; set; }

        public override string ToString()
        {
            return $"{Task1} {Task2} {Task3} {Task4}\n";
        }
    }
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Map(x => x.Task1).Name("Task1");
            Map(x => x.Task2).Name("Task2");
            Map(x => x.Task3).Name("Task3");
            Map(x => x.Task4).Name("Task4");
        }
    }
}
