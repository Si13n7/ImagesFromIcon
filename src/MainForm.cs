namespace ImagesFromIcon
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using SilDev;
    using SilDev.Drawing;

    public partial class MainForm : Form
    {
        public MainForm() =>
            InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = @"Icon files (*.ico)|*.ico",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            })
                try
                {
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                    {
                        Application.Exit();
                        return;
                    }
                    var file = dialog.FileName;
                    var dir = Path.GetDirectoryName(file);
                    if (string.IsNullOrEmpty(dir))
                        throw new ArgumentNullException(nameof(dir));
                    var name = Path.GetFileNameWithoutExtension(file);
                    if (string.IsNullOrEmpty(name))
                        throw new ArgumentNullException(nameof(name));
                    dir = Path.Combine(dir, $"{name} sources");
                    IconFactory.Extract(dialog.FileName);
                    if (!Directory.Exists(dir))
                        throw new PathNotFoundException(dir);
                    if (DirectoryEx.EnumerateFiles(dir, "*.png")?.Any() != true)
                        throw new FileNotFoundException();
                    Process.Start(dir);
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            Application.Exit();
        }
    }
}
