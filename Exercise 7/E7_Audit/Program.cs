
using Microsoft.EntityFrameworkCore;
using System;

namespace E7_Audit
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: dotnet run <command> [<directory_path>]");
                return;
            }

            string command = args[0].ToLower();
            E7_Logic auditContext = new();

            switch (command)
            {
                case "watch" when args.Length == 2:
                    await auditContext.ExecuteCommand("watch", args[1]);
                    break;

                case "log":
                    await auditContext.ExecuteCommand("log");
                    break;

                case "clean":
                    await auditContext.ExecuteCommand("clean");
                    break;

                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }
    }
}
