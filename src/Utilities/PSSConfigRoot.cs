
namespace PSSimpleConfig;

public static class PSSimpleConfig
{
    public static string Scope { get; private set; } = "User";
    public static string Root { get; private set; }

    public static string ProjectRoot { get { return Path.Combine(Root, "projects"); }}
    static PSSimpleConfig()
    {
        UpdateRoot();

        if (Root == null) {
            Root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
    }
    public static void SetScope(string configScope)
    {
        if (configScope == "User" || configScope == "Machine")
        {
            Scope = configScope;
            UpdateRoot();
        }
        else
        {
            throw new ArgumentException("Scope must be either 'User' or 'Machine'.");
        }
    }
    private static void UpdateRoot()
    {
        // Allow a user to override the default root folder with environment variable
        if (Environment.GetEnvironmentVariable("PSSC_ROOT") != null)
        {
            Root = Environment.GetEnvironmentVariable("PSSC_ROOT");
            return;
        }
        else if (Environment.GetEnvironmentVariable("PSSC_ROOT") == null && Root != "")
        {
            string rootFolder = Environment.GetFolderPath(
                Scope == "User" ? Environment.SpecialFolder.LocalApplicationData : Environment.SpecialFolder.CommonApplicationData
            );
            Root = Path.Combine(rootFolder, "PSSimpleConfig");
        }
    }
}

