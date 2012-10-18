using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dot_NET_Diagram
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Stream fileStream = null;
            if ( _openFileDialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    if ( ( fileStream = _openFileDialog.OpenFile() ) != null )
                    {
                        using ( fileStream )
                        {
                            // Use file stream here.
                        }
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
