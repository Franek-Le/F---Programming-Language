namespace Installer {
    public static class FmmInstaller
    {
        public static void Main() {
            Install();
            AddToPath();
        }

        static void Install()
        {
            string CurrentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Directory: " + CurrentPath);
            string AppPath = Path.Combine(CurrentPath + "/", "F--/");
            Console.WriteLine("App Directory" + AppPath);
            var dir = new DirectoryInfo("C:\\Program Files\\F--\\");

            if (!dir.Exists)
            {
                Directory.CreateDirectory(dir.FullName);
            }

            if (dir.Exists)
            {
                foreach (var file in Directory.GetFiles(dir.FullName))
                    File.Delete(file);
            }

            Copy(AppPath, dir.FullName);
        }

        static void AddToPath()
        {
            string newPath = @"C:\Program Files\F--\";
            string currentPath = Environment.GetEnvironmentVariable("PATH");
            if (!currentPath.Contains(newPath))
            {
                string updatedPath = currentPath + ";" + newPath;
                Environment.SetEnvironmentVariable("PATH", updatedPath, EnvironmentVariableTarget.Machine);
                Console.WriteLine("Executable path added to PATH successfully.");
            }
            else
            {
                Console.WriteLine("The specified path is already in the PATH.");
            }
        }

        static void Copy(string sourceDir, string targetDir)
        {
            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }
    }
}