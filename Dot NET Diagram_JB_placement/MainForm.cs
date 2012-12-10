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
        /// <summary>
        /// The assembly loaded in the form. Null on empty.
        /// </summary>
        private Assembly _loadedAssembly;

        public MainForm()
        {
            InitializeComponent();
            _loadedAssembly = null;

            this.AllowDrop = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler( this._OnDragDrop );
            this.DragEnter += new System.Windows.Forms.DragEventHandler( this._OnDragEnter );       
        }

        /// <summary>
        /// "Open" menu item. Used to open an assembly file.
        /// </summary>
        private void openToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( _openFileDialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    if ( _openFileDialog.CheckFileExists )
                    {
                        string filename = _openFileDialog.FileName;
                        loadFile( filename );
                    }
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( "Error: Could not read file from disk.\n" + ex.Message );
                }
            }
        }

        /// <summary>
        /// "Exit" menu item. Close form, and exit application.
        /// </summary>
        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void _OnDragEnter( object sender, DragEventArgs e )
        {
            _toolStripStatusLabel.Text = "DragEnter";
            if ( e.Data.GetDataPresent( DataFormats.FileDrop, false ) )
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void _OnDragDrop( object sender, DragEventArgs e )
        {
            string[] files = (string[])e.Data.GetData( DataFormats.FileDrop );
            foreach ( string filename in files )
            {
                _toolStripStatusLabel.Text = "Opening " + filename + ".";
                loadFile( filename );
            }
        }

        private void loadFile( string filename )
        {
            if ( File.Exists( filename ) )
            {
                try
                {
                    _loadedAssembly = Assembly.LoadFile( filename );
                    _toolStripStatusLabel.Text = "Opened " + filename + ".";
                    _diagramDisplayControl.loadAssembly( _loadedAssembly );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( "Could not load " + filename + ": " + ex.Message, "Open failed", MessageBoxButtons.OK, MessageBoxIcon.Error );
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
    }
}
