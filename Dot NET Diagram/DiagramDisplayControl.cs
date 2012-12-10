using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dataweb.NShape;
using Dataweb.NShape.Advanced;
using Dataweb.NShape.GeneralShapes;
using System.IO;
using System.Reflection;

namespace Dot_NET_Diagram
{
    /// <summary>
    /// Diagram display user control. Handles the display of data. Uses NShape.
    /// </summary>
    public partial class DiagramDisplayControl : UserControl
    {
        private Dataweb.NShape.Diagram _NShapeDiagram;

        public DiagramDisplayControl()
        {
            InitializeComponent();

            _xmlStore.DirectoryName = System.IO.Path.GetDirectoryName( Application.ExecutablePath );
            _xmlStore.FileExtension = ".nspj";
            
            _NShapeProject.Name = ".NET Diagram";
            _NShapeProject.AddLibrary( typeof( Ellipse ).Assembly, false );
            _NShapeProject.Create();

            _NShapeDiagram = new Diagram( "diagram" );
            _NShapeDiagram.Height = _NShapeDisplay.Height;
            _NShapeDiagram.Width = _NShapeDisplay.Width;

            /* Test shape display */
            RectangleBase shape = (RectangleBase)_NShapeProject.ShapeTypes["Ellipse"].CreateInstance();
            shape.Width = 100;
            shape.Height = 100;
            shape.X = 100;
            shape.Y = 100;
            shape.SetCaptionText(0, "Hello World");

            _NShapeDiagram.Shapes.Add(shape);
    
            /* End of display */

            _NShapeDisplay.Diagram = _NShapeDiagram;
        }

        private void _NShapeDisplay_Layout( object sender, LayoutEventArgs e )
        {
            //MessageBox.Show( "Hi" );

            _NShapeDiagram.Height = _NShapeDisplay.Height - 100;
            _NShapeDiagram.Width = _NShapeDisplay.Width - 100;
        }

        public void loadAssembly( Assembly assembly )
        {
            /**
             * INSERT INTELLIGENCE HERE
             * */

            MessageBox.Show( "Assembly loaded!\n" + assembly.GetName() + "\n" + assembly.FullName );
        }

        private void _NShapeDisplay_Load(object sender, EventArgs e)
        {

        }
    }
}
