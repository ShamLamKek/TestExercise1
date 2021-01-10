using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;

namespace TestExercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Quantity of iterations
            Console.WriteLine("Сколько нужно JSON");
            int quantity = Convert.ToInt32(Console.ReadLine());
           
            for (int i = 1; i < quantity + 1; i++)
            {
                //Need to change this path 
                string JsonPath = @"E:\TestExercise1\TestExercise1\JSON";

                //Path to inital directory
                Console.WriteLine("Введите исходный путь: ");
                string InitDir = Console.ReadLine();

                //Path to end directory
                Console.WriteLine("Введите конечный путь");
                string EndDir = Console.ReadLine();

                Settings settings = new Settings
                {
                    InitialDirectory = InitDir,
                    EndDirectory = EndDir

                };

                //Serialization
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(Path.Combine(JsonPath, $"json_{i}.json"), json);

                //Deserialization 
                var obj = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(JsonPath, $"json_{i}.json")));

                string initDir = obj.InitialDirectory;
                string endDir = obj.EndDirectory;

                Backup(initDir, endDir);

                Console.WriteLine("_________________________________________________________");
            }

            Console.ReadKey();
        }

        static void Backup(string initDir, string endDir)
        {
            //Create zip file from directory with creation time
            string timestamp = DateTime.Now.ToString("dddd, dd MMMM yyyy HH.mm.ss");
            string destination = endDir + @"\" + timestamp + ".zip";
            ZipFile.CreateFromDirectory(initDir, destination);

        }

    }
}
