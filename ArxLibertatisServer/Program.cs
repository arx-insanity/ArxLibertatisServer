using ArxLibertatisServer.Messages;
using NLog;
using NLog.Config;
using NLog.Targets;
using Plexdata.ArgumentParser.Extensions;
using System;
using System.Linq;
using System.Net;

namespace ArxLibertatisServer
{
    public class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static readonly CommandLineArgs cmdLineArgs = new CommandLineArgs();

        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "\\?")
            {
                Console.WriteLine(cmdLineArgs.Generate());
                return;
            }

            ConfigureLogging();

            RegisterMessageTypes();

            RunServer(args);
        }

        static void ConfigureLogging()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ConsoleTarget
            {
                Name = "console",
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=tostring}",
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget, "*");
            LogManager.Configuration = config;
        }

        static void RegisterMessageTypes()
        {
            var mt = typeof(Message);
            var messageTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => mt.IsAssignableFrom(type) && type != mt && !type.IsAbstract);

            foreach (var t in messageTypes)
            {
                Message.RegisterMessageType(t);
            }
        }

        static void RunServer(string[] args)
        {
            if (args.Length > 0)
            {
                cmdLineArgs.Process(args);
            }

            var server = new Server(new IPEndPoint(IPAddress.Any, cmdLineArgs.Port));
            server.Start();
            server.Join();
        }
    }
}
