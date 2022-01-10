using Proyect_Base.app.socket;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Proyect_Base.forms
{
    public partial class App : Form
    {
        public static App Form;
        private string logsPath;
        public App()
        {
            this.FormClosing += App_FormClosing;
            this.logsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Logs\errors.txt");
            makeLogsFile();
            InitializeComponent();
            tabControl1.SelectedIndexChanged += new EventHandler(TabControl1_SelectedIndexChanged);
        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Console":
                    console.Focus();
                    break;
                case "Logs":
                    loadLogsHandler();
                    richTextBox1.Focus();
                    break;
            }
        }
        private void makeLogsFile()
        {
            if (!File.Exists(logsPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(@"Logs\"));
                using (FileStream fs = File.Create(logsPath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("\nThe logs file was created successfully.\n");
                    fs.Write(info, 0, info.Length);
                }
            }
        }
        private void loadLogsHandler()
        {
            richTextBox1.Text = System.IO.File.ReadAllText(this.logsPath);
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        private void App_Load(object sender, EventArgs e)
        {
            Text = Config.APP_NAME;
            initProyect();
        }
        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        private void initProyect()
        {
            try
            {
                DateTime now = DateTime.Now;
                WriteLine("Iniciando servidor...", "success");

                Form = this;
                loadExternalDll();
                initWebSocket();
                AppInit.init();

                AppInit.appStarted(now);
                Console.Beep();
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString(), "error");
            }
        }
        private static void loadExternalDll()
        {
            string resource = "Proyect_Base.libs.MySql.Data.dll";
            EmbeddedAssembly.Load(resource, "MySql.Data.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
        private void initWebSocket()
        {
            Thread thread = new Thread(() =>
            {
                WebSocketServer.Initialize();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        public void WriteLine(string text)
        {
            CheckForIllegalCrossThreadCalls = false;

            console.SelectionColor = Color.White;

            string output = DateTime.Now.ToString("HH:mm:ss") + " -> " + text;
            console.AppendText(Environment.NewLine + output);
            console.SelectionStart = console.Text.Length;
            console.ScrollToCaret();
        }

        public void WriteLine(string text, string status)
        {
            CheckForIllegalCrossThreadCalls = false;
            switch (status)
            {
                case "warning":
                    console.SelectionColor = Color.Yellow;
                    break;
                case "success":
                    console.SelectionColor = Color.GreenYellow;
                    break;
                case "error":
                    console.SelectionColor = Color.Red;
                    break;
                case "normal":
                    console.SelectionColor = Color.White;
                    break;
            }
            string output = DateTime.Now.ToString("HH:mm:ss") + " -> " + text;
            console.AppendText(Environment.NewLine + output);
            console.SelectionColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Directory.Delete(Path.GetDirectoryName(@"Logs\"), true);
            makeLogsFile();
            loadLogsHandler();
        }
    }
}
