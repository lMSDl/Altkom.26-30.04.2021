using ConsoleApp.Config;
using ConsoleApp.Services;
using DataService;
using DataService.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static IStudentsService StudentsService { get; } = new StudentsService();
        private static IService<Educator> EducatorsService { get; } = new Service<Educator>();
        static ServiceProvider ServiceProvider { get; set; }

        private static Settings Settings { get; } = new Settings();
        private static IConfigurationRoot Config { get; set; }

        private static async Task Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/signalR/Students")
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnecting += connetionId =>
            {
                Console.WriteLine("Reconnecting...");
                return Task.CompletedTask;
            };
            connection.Closed += connetionId =>
            {
                Console.WriteLine("Closed");
                return Task.CompletedTask;
            };

            connection.On<string>("NewClient", x => Console.WriteLine($"new client: {x}"));
            connection.On<Student>("Post", x => Console.WriteLine(x.ToJson()));

            while (connection.State != HubConnectionState.Connected)
            {
                Console.WriteLine("Connecting...");
                try
                {
                    await connection.StartAsync();
                    Console.WriteLine("Connected");
                }
                catch
                {
                    Console.WriteLine("Connection failed");
                }
            }

            await connection.SendAsync("JoinGroup", "2");
            await Task.Delay(2000);

            await connection.SendAsync("AddStudent", new Student { FirstName = "Ewa", LastName = "Ewowska" });


            Console.ReadLine();
            await connection.DisposeAsync();
        }

        private static void OldMain()
        {
            Configuration();
            ConfigureServiceProvider();

            var writer = ServiceProvider.GetService<IWriteService>();
            writer.WriteLine("service from provider");

            writer = ServiceProvider.GetService<IFiggleWriteService>();
            writer.WriteLine("Figgle service from provider");
            TaskExample();
        }

        private static async void TaskExample()
        {
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Hello from Task!");
            });

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var task = Task<int>.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(3000);
                return 100;
            });

            /*task.ContinueWith(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId); Console.WriteLine(x.Result); })
                .ContinueWith(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId); Task.Delay(2000); })
                .ContinueWith(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId); Task.Delay(2000); })
                .ContinueWith(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId); Task.Delay(2000); })
                .ContinueWith(x => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId); Task.Delay(2000); });*/

            var result = await task;
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);

        }

        private static void ThreadExample()
        {
            var thread = new Thread(() =>
            {
                var counter = 0;
                while (true)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(-1);
                    }
                    catch (ThreadInterruptedException)
                    {

                    }
                    Console.WriteLine(counter++);
                }
            });
            thread.Start();

            System.Threading.Thread.Sleep(1000);

            thread.Interrupt();
            System.Threading.Thread.Sleep(1000);
            thread.Interrupt();
            System.Threading.Thread.Sleep(2000);
            thread.Interrupt();
            System.Threading.Thread.Sleep(1000);
            thread.Interrupt();
            System.Threading.Thread.Sleep(10);
            thread.Interrupt();
        }

        private static void ConfigureServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddScoped<IWriteService, ConsoleService>()
                .AddScoped<IFiggleWriteService, FiggleWriteService>()
                .AddLogging(x => x.AddConsole().AddDebug().AddConfiguration(Config.GetSection("Logging")));
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigurationExample()
        {
            Hello(Config["HelloJson"]);
            Hello(Config["HelloYaml"]);
            Hello(Config["HelloIni"]);

            Console.WriteLine(Config["Data"]);

            Settings settings = new Settings();
            Console.WriteLine(settings.Data);

            Hello(Config["Section:Key2"], Config["Section:Subsection:Key1"]);
            Hello(settings.Section.Key2, settings.Section.Subsection.Key1);
        }

        private static void Configuration()
        {
            Config = new ConfigurationBuilder()
                .AddXmlFile("Config\\config.xml")
                .AddYamlFile("Config\\config.yaml")
                .AddJsonFile("Config\\config.json", false, true)
                .AddIniFile("Config\\config.ini")
                .AddIniFile("Config\\config2.ini", true)
                .Build();
            Config.Bind(Settings);
        }

        private static void Hello(string hello, string from)
        {
            Console.WriteLine($"{hello} {from}");
        }

        private static void Hello(string @string)
        {
            Console.WriteLine($"Hello {@string}");
        }

        private static void Service()
        {
            Console.WriteLine("Hello World!");

            Student student1 = new Student();
            Student student2 = new Student("Ewa", "Ewowska") { BirthDate = new DateTime(1985, 4, 22) };
            Educator educator1 = new Educator() { FirstName = "Damian", LastName = "Damianowski" };
            Educator educator2 = new Educator() { FirstName = "Damian", LastName = "Damianowski", Address = "Warszawska 12" };

            Person person;
            person = student1;
            person = educator1;

            StudentsService.Create(student1);
            student2 = StudentsService.Create(student2);
            Student student3 = new Student() { Address = "Kwiatowa 44" };
            StudentsService.Update(student2.Id, student3);

            foreach (Student item in StudentsService.Read())
            {
                Console.WriteLine(item.ToJson());
            }

            EducatorsService.Create(educator1);
            EducatorsService.Create(educator2);
            foreach (Educator item in EducatorsService.Read())
            {
                Console.WriteLine(item.ToJson());
            }
        }
    }
}
