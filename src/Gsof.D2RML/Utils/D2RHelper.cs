using System;
using Microsoft.Win32;

namespace Gsof.D2RML.Utils;

public class D2RHelper
{
    public static string? GetPath()
    {
        var registry = Registry.LocalMachine;

        var folder = OpenRegistryKey(registry, "/SOFTWARE/WOW6432Node/Microsoft/Windows/CurrentVersion/Uninstall/Diablo II Resurrected");

        return folder?.GetValue("InstallLocation") as string;
    }

    //打开，指定根节点和路径的注册表项
    private static RegistryKey? OpenRegistryKey(RegistryKey? root, string str)
    {
        str = str.Remove(0, 1) + @"/";
        while (str.Contains('/', StringComparison.Ordinal))
        {
            if (root == null)
            {
                break;
            }

            var sub = str[..str.IndexOf('/', StringComparison.Ordinal)];

            root = root.OpenSubKey(sub);

            str = str.Remove(0, str.IndexOf('/', StringComparison.Ordinal) + 1);
        }
        return root;
    }
}