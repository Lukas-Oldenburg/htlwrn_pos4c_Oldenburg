// FileAuditContext.cs
using E7_Audit;
using Microsoft.EntityFrameworkCore;

class E7_Logic
{
    private AuditContext Context { get; set; } = new();

    public async Task ExecuteCommand(string command, string directoryPath = null!)
    {
        switch (command.ToLower())
        {
            case "watch":
                await Watch(directoryPath);
                break;

            case "log":
                await Log();
                break;

            case "clean":
                await Clean();
                break;

            default:
                Console.WriteLine($"Unknown command: {command}");
                break;
        }
    }

    private async Task Watch(string directoryPath = null)
    {
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Error: Directory '{directoryPath}' does not exist.");
            return;
        }

        var watcher = new FileSystemWatcher(directoryPath);

        watcher.Changed += OnFileChanged;
        watcher.Created += OnFileChanged;
        watcher.Deleted += OnFileChanged;
        watcher.Renamed += OnFileRenamed;

        watcher.EnableRaisingEvents = true;

        Console.WriteLine($"Watching directory: {directoryPath}");
        Console.WriteLine("Press 'q' to stop watching.");
        Console.ReadKey();
        await Context.SaveChangesAsync();
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        LogChange(e.FullPath, e.ChangeType.ToString());
    }

    private void OnFileRenamed(object sender, RenamedEventArgs e)
    {
        LogChange(e.FullPath, $"Renamed to {e.FullPath}");
    }

    private void LogChange(string fullPath, string changeType)
    {
        var auditEntry = new FileAudit
        {
            FileName = Path.GetFileName(fullPath),
            ChangeType = changeType,
            Timestamp = DateTime.Now,
            FullPath = fullPath
        };

        Context.FileAudits.Add(auditEntry);
    }

    private async Task Log()
    {
        using (var context = new AuditContext())
        {
            var logEntries = await context.FileAudits.ToListAsync();

            if (logEntries.Any())
            {
                Console.WriteLine("Log entries:");
                foreach (var entry in logEntries)
                {
                    Console.WriteLine($"{entry.Timestamp} - {entry.ChangeType} - {entry.FileName} - {entry.FullPath}");
                }
            }
            else
            {
                Console.WriteLine("Log is empty.");
            }
        }
    }

    private async Task Clean()
    {
        using (var context = new AuditContext())
        {
            context.FileAudits.RemoveRange(context.FileAudits);
            await context.SaveChangesAsync();

            Console.WriteLine("Log entries cleaned up.");
        }
    }
}
