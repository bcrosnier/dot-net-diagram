using System;
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
        private Assembly _loadedAssembly;

        public MainForm()
        {
            InitializeComponent();
            
            _loadedAssembly = null;
        }

        private void openToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( _openFileDialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    if ( _openFileDialog.CheckFileExists )
                    {
                        string filename = _openFileDialog.FileName;
                        _loadedAssembly = Assembly.LoadFile( filename );
                        _toolStripStatusLabel.Text = "Opened " + filename + ".";
                    }
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( "Error: Could not read file from disk. Original error: " + ex.Message );
                }
            }
        }
    }
}
