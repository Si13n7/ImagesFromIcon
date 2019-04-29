namespace IconToImage
{
    using System;
    using System.Windows.Forms;
    using SilDev;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Log.AllowLogging();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
