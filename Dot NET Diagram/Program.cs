using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dot_NET_Diagram
{
    static class Program
    {
        private const string DEBUG_LOG_FILE = "debug.log";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            initDebug();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new MainForm() );

            Application.ExitThread();
            openLog();
        }

        /// <summary>
        /// Initiates debug log.
        /// </summary>
        [System.Diagnostics.ConditionalAttribute("DEBUG")]
        private static void initDebug()
        {
            TextWriterTraceListener traceListener = new TextWriterTraceListener(System.IO.File.CreateText(DEBUG_LOG_FILE));
            Debug.Listeners.Add(traceListener);
        }//Process.Start("myapp.exe", "file1.txt");

        /// <summary>
        /// Opens debug log.
        /// </summary>
        [System.Diagnostics.ConditionalAttribute("DEBUG")]
        private static void openLog()
        {
            Process.Start(DEBUG_LOG_FILE);
        }
    }
}
