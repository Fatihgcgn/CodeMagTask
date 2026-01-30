namespace WinForm
{
    using Serilog;

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .WriteTo.File(
                    path: "Logs/winform-log-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14,
                    shared: true
                )
                .CreateLogger();

            // UI thread exception’larý
            Application.ThreadException += (sender, args) =>
            {
                Log.Error(args.Exception, "WINFORMS UI Thread Exception");
            };

            // Background / unhandled exception’lar
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                if (args.ExceptionObject is Exception ex)
                    Log.Fatal(ex, "WINFORMS Unhandled Exception");
            };

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WINFORMS Application Crash");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}