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
            _NShapeProject.LibrarySearchPaths.Add( Path.Combine( System.IO.Path.GetDirectoryName( Application.ExecutablePath ), @"lib" ) );
            _NShapeProject.AutoLoadLibraries = true;
            _NShapeProject.AddLibraryByName( "Dataweb.NShape.GeneralShapes", true );


            _NShapeDiagram = new Diagram( "diagram" );
            _NShapeDiagram.Height = _NShapeDisplay.Height;
            _NShapeDiagram.Width = _NShapeDisplay.Width;

            _NShapeProject.Create();
            
            /* Test display */
            RectangleBase shape = (RectangleBase)_NShapeProject.ShapeTypes["Ellipse"].CreateInstance();

            shape.Width = 100;
            shape.Height = 100;

            shape.X = 50;
            shape.Y = 50;
            shape.SetCaptionText( 0, "Hello world" );

            _NShapeDiagram.Shapes.Add( shape );
            /* End of display */

            _NShapeDisplay.Diagram = _NShapeDiagram;
        }
    }
}
