
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

            await auditContext.ExecuteCommand(command, args.Length == 2 ? args[1] : null!);
        }
    }
}
