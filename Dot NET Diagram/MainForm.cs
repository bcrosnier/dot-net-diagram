﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Dot_NET_Diagram
{

    public partial class MainForm : Form
    {

        /// <summary>
        /// The assembly loaded in the form. Null on empty.
        /// </summary>
        private Assembly _loadedAssembly;

        public MainForm()
        {
            InitializeComponent();
            _loadedAssembly = null;

            InitConsole();

            this.AllowDrop = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this._OnDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this._OnDragEnter);
        }

        /// <summary>
        /// Show console.
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        /// <summary>
        /// Create and allocate a console, as defined above.
        /// </summary>
        [System.Diagnostics.ConditionalAttribute("DEBUG")]
        private void InitConsole()
        {
            AllocConsole();
        }

        /// <summary>
        /// Log to console in debug mode.
        /// </summary>
        /// <param name="str">String to log</param>
        [System.Diagnostics.ConditionalAttribute("DEBUG")]
        public static void LogOnDebug(string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// "Open" menu item. Used to open an assembly file.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (_openFileDialog.CheckFileExists)
                    {
                        string filename = _openFileDialog.FileName;
                        loadFile(filename);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk:\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// "Exit" menu item. Close form, and exit application.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _OnDragEnter(object sender, DragEventArgs e)
        {
            _toolStripStatusLabel.Text = "DragEnter";
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void _OnDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string filename in files)
            {
                _toolStripStatusLabel.Text = "Opening " + filename + ".";
                loadFile(filename);
            }
        }

        private void loadFile(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    _loadedAssembly = Assembly.LoadFile(filename);
                }
                catch (Exception ex)
                {
                    // Couldn't load file.                   
                    MessageBox.Show("Could not file " + filename + ": " + ex.Message, "Open failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _toolStripStatusLabel.Text = "Opened " + filename + ".";

                try
                {
                    // Couldn't load assembly.
                    _diagramDisplayControl.loadAssembly(_loadedAssembly);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load assembly from " + filename + ": " + ex.Message, "Loading failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _toolStripStatusLabel.Text = "Could not load " + filename + ": " + ex.Message;
                }
            }
            else
            {
                _toolStripStatusLabel.Text = "File " + filename + " does not exist.";
            }
        }

        private void _diagramDisplayControl_Load(object sender, EventArgs e)
        {

        }

        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DllReader test = new DllReader(_loadedAssembly.Location);
            _diagramDisplayControl.DrawAllRelation(test);
        }
    }
}