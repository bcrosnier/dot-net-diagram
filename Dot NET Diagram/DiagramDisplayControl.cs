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



            _NShapeDisplay.Diagram = _NShapeDiagram;
        }

        private void _NShapeDisplay_Layout( object sender, LayoutEventArgs e )
        {
            // MessageBox.Show( "Hi" );

            _NShapeDiagram.Height = _NShapeDisplay.Height + 1000;
            _NShapeDiagram.Width = _NShapeDisplay.Width + 1000;
        }

        public void loadAssembly( Assembly assembly )
        {
            /***********placer les classes sur le display***********************/
            DllReader test = new DllReader( assembly.Location );

            //MessageBox.Show( "Assembly loaded!\n" + assembly.GetName() + "\n" + assembly.FullName );

            Dictionary<String, CaptionedShapeBase> shapeDict = new Dictionary<String, CaptionedShapeBase>();
            List<DescriptionClass> classList;

            int x = 100;
            int y = 100;

            IEnumerable<DescriptionClass> requete = from dc in DescriptionClass.PutTypeInList( test )
                                                    orderby CountSubClass( dc )
                                                    select dc;
            classList = requete.ToList<DescriptionClass>();
            classList.Reverse( 0, classList.Count );
            DescriptionClass currentDc = null;
            int index = 0;
            int unblock = 0;
            while ( classList.Count != 0 )
            {
                currentDc = classList.ElementAt( 0 );
                if ( currentDc.IsAnInterface() )
                    DrawInterfaceShape( currentDc.GetName(), x, y, shapeDict );

                else
                    DrawClassShape( currentDc.GetName(), x, y, shapeDict );
                y += 200;
                classList.RemoveAt( 0 );

                foreach ( Type type in currentDc.SortListType( currentDc._subClasses, currentDc._mainType ) )
                {
                    unblock = 0;
                    index = 0;

                    if ( classList.Count == 0 )
                        break;
                    foreach ( DescriptionClass dc in classList )
                    {

                        if ( dc.GetName() == type.Name )
                        {
                            if ( dc.IsAnInterface() )
                                DrawInterfaceShape( dc.GetName(), x, y, shapeDict );

                            else
                                DrawClassShape( dc.GetName(), x, y, shapeDict );

                            x += 200;
                            unblock = 1;
                            break;
                        }
                        index++;
                    }
                    if ( unblock == 1 )
                        classList.RemoveAt( index );
                }
                y += 200;
                x = 100;
            }

            /*****************Dessiner les relations****************************/
            foreach ( DescriptionClass dc in DescriptionClass.PutTypeInList( test ) )
                foreach ( Type type in dc.SortListType( dc._subClasses, dc._mainType ) )
                {
                    DrawRelation( type.Name, dc.GetName(), shapeDict );
                }
        }

        private void _NShapeDisplay_Load( object sender, EventArgs e )
        {

        }

        public int CountSubClass( DescriptionClass dc )
        {
            int counter = 0;
            foreach ( Type type in dc.SortListType( dc._subClasses, dc._mainType ) )
            {
                counter++;
            }
            return counter;
        }

        public void DrawClassShape( string s, int x, int y, Dictionary<String, CaptionedShapeBase> dShape )
        {
            CircleBase shape = (CircleBase) _NShapeProject.ShapeTypes["Circle"].CreateInstance();
            shape.Diameter = 100;
            shape.X = x;
            shape.Y = y;
            shape.SetCaptionText( 0, s );

            _NShapeDiagram.Shapes.Add( shape );
            if ( !dShape.ContainsKey( s ) )
                dShape.Add( s, shape );
        }

        public void DrawInterfaceShape( string s, int x, int y, Dictionary<String, CaptionedShapeBase> dShape )
        {
            CircleBase shape = (CircleBase) _NShapeProject.ShapeTypes["Circle"].CreateInstance();
            shape.Diameter = 100;
            shape.X = x;
            shape.Y = y;

            ColorStyle myColorStyle = new ColorStyle( "test", System.Drawing.Color.Green );
            ColorStyle mySecondColorStyle = new ColorStyle( "test", System.Drawing.Color.White );
            FillStyle myFillStyle = new FillStyle( "test", myColorStyle, mySecondColorStyle );
            shape.FillStyle = myFillStyle;
            shape.SetCaptionText( 0, s );

            _NShapeDiagram.Shapes.Add( shape );

            if ( !dShape.ContainsKey( s ) )
                dShape.Add( s, shape );
        }

        public void DrawMemberClass( string s, int x, int y )
        {
            RectangleBase shape = (RectangleBase) _NShapeProject.ShapeTypes["Ellipse"].CreateInstance();
            shape.Width = 100;
            shape.Height = 50;
            shape.X = x;
            shape.Y = y;
            shape.SetCaptionText( 0, s );

            _NShapeDiagram.Shapes.Add( shape );
        }

        /// <summary>
        /// Draw relation line/arrow between two classes in the diagram, provided the class exists in given dictionary
        /// </summary>
        /// <param name="class1Name">Class 1 name. (Departing)/</param>
        /// <param name="class2Name">Class 2 name. (Arrival)</param>
        /// <param name="shapeDictionary">Shape dictionary to use</param>
        public void DrawRelation( string class1Name, string class2Name, Dictionary<string, CaptionedShapeBase> shapeDictionary )
        {
            if ( !shapeDictionary.ContainsKey( class1Name ) )
            {
                MainForm.LogOnDebug( "DrawRelation: Departing class missing from dictionary: " + class1Name );
                return;
            }
            
            if( !shapeDictionary.ContainsKey( class2Name ) )
            {
                MainForm.LogOnDebug( "DrawRelation: Arrival class missing from dictionary: " + class2Name );
                return;
            }

            LineShapeBase line = (LineShapeBase) _NShapeProject.ShapeTypes["Polyline"].CreateInstance();
            ThickArrow arrow = (ThickArrow) _NShapeProject.ShapeTypes["ThickArrow"].CreateInstance();

            line.Connect( ControlPointId.FirstVertex, shapeDictionary[class1Name], ControlPointId.Reference );
            line.Connect( ControlPointId.LastVertex, shapeDictionary[class2Name], ControlPointId.Reference );

            arrow.MoveControlPointTo( 1, line.GetControlPointPosition( ControlPointId.FirstVertex ).X,
                                    line.GetControlPointPosition( ControlPointId.FirstVertex ).Y, 0 );
            arrow.MoveControlPointTo( 6, line.GetControlPointPosition( ControlPointId.LastVertex ).X,
                                    line.GetControlPointPosition( ControlPointId.LastVertex ).Y, 0 );

            _NShapeDiagram.Shapes.Add( arrow );
        }

    }
}
