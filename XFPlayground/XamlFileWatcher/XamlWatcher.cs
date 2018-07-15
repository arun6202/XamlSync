using System;
using System.IO;
using XamlFileWatcher;

public class FileWatcher
{
    private const string MonitorPath = @"D:\XamlSync\XFPlayground\XFPlayground";

    private const string XamlLocation = "XFPlayground\\XFPlayground";

    private const string appLocation = @"../../../../../../";

    private static readonly string projectRoot = Path.GetFullPath(Path.Combine(typeof(XamlSync).Assembly.Location, appLocation));

    private static readonly string XamlFilePath = Path.Combine(projectRoot, XamlLocation);

    public static void Main(string[] args)
    {

        try
        {

            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = XamlFilePath,

                IncludeSubdirectories = true,

                NotifyFilter = NotifyFilters.Attributes |
            NotifyFilters.CreationTime |
            NotifyFilters.DirectoryName |
            NotifyFilters.FileName |
            NotifyFilters.LastAccess |
            NotifyFilters.LastWrite |
            NotifyFilters.Security |
            NotifyFilters.Size,

                Filter = "*.xaml"
            };


            watcher.Changed += new FileSystemEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Watching:" + XamlFilePath);
            while (Console.Read() != 'q')
            {
                ;
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("A Exception Occurred :" + e);
        }

        catch (Exception oe)
        {
            Console.WriteLine("An Exception Occurred :" + oe);
        }

        Console.Read();
    }

    public static void OnChanged(object source, FileSystemEventArgs e)
    {
        XamlSync.Start(e.FullPath).Wait();
    }

}
