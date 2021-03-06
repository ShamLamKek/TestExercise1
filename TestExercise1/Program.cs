using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.IO.Compression;


namespace TestExercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.File(Directory.GetCurrentDirectory()+@"\Logs\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Logger.Information("Application start at {datetime}", DateTime.UtcNow);

            

            Backup();

            Console.ReadKey();
        }
    
        static void Backup()
        {
            //Quantity of iterations
            Console.WriteLine("Сколько нужно JSON");
            int quantity = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i < quantity + 1; i++)
            {

                string JsonPath = Directory.GetCurrentDirectory()+@"\JSON";

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

                //Create zip file from directory with creation time
                string timestamp = DateTime.Now.ToString("dddd, dd MMMM yyyy HH.mm.ss");
                string destination = endDir + @"\" + timestamp + ".zip";
                ZipFile.CreateFromDirectory(initDir, destination);

                Console.WriteLine("_________________________________________________________");
            }

        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        }

    }
}
  