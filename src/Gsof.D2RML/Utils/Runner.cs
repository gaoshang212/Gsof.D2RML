using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Gsof.D2RML.Models;

namespace Gsof.D2RML.Utils;

public class Runner
{
    private static string? _d2rPath;

    public static async Task Runs(IEnumerable<D2RUser> users, int second)
    {
        foreach (var user in users)
        {
            await Run(user);
            await Task.Delay(TimeSpan.FromSeconds(second));
        }
    }

    public static async Task Run(D2RUser user)
    {
        var result = await Cli.Wrap("./Handle/handle.exe")
            .WithArguments(["-a", "Check For Other Instances", "-nobanner"])
            .ExecuteBufferedAsync();


        if (result.IsSuccess)
        {
            var processes = result.StandardOutput.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var process in processes)
            {
                var values = process.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (values.Length < 6)
                {
                    continue;
                }

                var pid = values[2];
                var handle = values[5];

                await Cli.Wrap("./Handle/handle.exe")
                    .WithArguments(["-p", pid, "-c", handle, "-y"])
                    .ExecuteBufferedAsync();
            }
        }

        await Start(user);
    }

    public static Task Start(D2RUser user)
    {
        if (string.IsNullOrWhiteSpace(_d2rPath))
        {
            _d2rPath = D2RHelper.GetPath();
        }

        if (_d2rPath == null)
        {
            throw new ArgumentException("D2R Path can not found");
        }

        List<string> args = ["-username", user.Username ?? "", "-password", $"{user.Password}", "-address", user.Address ?? ""];

        if (!string.IsNullOrEmpty(user.Mod))
        {
            args.AddRange(["-mod", user.Mod ?? ""]);
        }

        var d2rPath = Path.Combine(_d2rPath, "D2R.exe");

        var command = Cli.Wrap(d2rPath)
            .WithArguments(args)
            .WithWorkingDirectory(Path.GetDirectoryName(d2rPath) ?? string.Empty);

        Debug.WriteLine(command.ToString());

        var task = command.ExecuteAsync();
        var processId = task.ProcessId;

        Console.WriteLine($"ProcessId: {processId}");

        return Task.CompletedTask;
    }
}