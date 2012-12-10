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

            _NShapeDiagram.Height = _NShapeDisplay.Height +1000;
            _NShapeDiagram.Width = _NShapeDisplay.Width +1000;
        }

        public void loadAssembly( Assembly assembly )
        {
/***********placer les classes sur le display***********************/
            DllReader test = new DllReader(assembly.Location);
            MessageBox.Show( "Assembly loaded!\n" + assembly.GetName() + "\n" + assembly.FullName );
            Dictionary<String, CaptionedShapeBase> dShape = new Dictionary<String, CaptionedShapeBase>();
            List<DescriptionClass> lDc;
            int x = 100;
            int y = 100;
        /*    foreach (DescriptionClass dc in DescriptionClass.PutTypeInList(test))
            {
                if (dc.IsAnInterface())
                {
                    DrawInterfaceShape(dc.GetName(), i, j, dShape);
                    j += 200;
                }
                else
                {
                    DrawClassShape(dc.GetName(), i, j, dShape);
                    i += 200;
                }
                MessageBox.Show(dc.GetName());
            }*/

            IEnumerable<DescriptionClass> requete = from dc in DescriptionClass.PutTypeInList(test)
                                                    orderby CountSubClass(dc)
                                                    select dc;
            lDc = requete.ToList<DescriptionClass>();
            lDc.Reverse(0, lDc.Count);
            DescriptionClass currentDc = null;
            int index=0;
            int unblock = 0;
            while(lDc.Count != 0)
            {
                currentDc = lDc.ElementAt(0);
                DrawClassShape(currentDc.GetName(), x, y, dShape);
                y += 200;
                lDc.RemoveAt(0);
                
                foreach(Type type in currentDc.SortListType(currentDc._subClasses, currentDc._mainType))
                {
                    unblock = 0;
                    index = 0;

                    if (lDc.Count == 0)
                        break;
                    foreach (DescriptionClass dc in lDc)
                    {
                        
                        if (dc.GetName() == type.Name)
                        {
                            if (dc.IsAnInterface())
                                DrawInterfaceShape(dc.GetName(), x, y, dShape);
                            
                            else
                                DrawClassShape(dc.GetName(), x, y, dShape);
      
                            x += 200;                       
                            unblock = 1;
                            break;
                        }
                        index++;
                    }
                    if(unblock==1)
                        lDc.RemoveAt(index);
                }
                y += 200;
                x = 100;
            }
           
/*****************Dessiner les relations****************************/
            foreach (DescriptionClass dc in DescriptionClass.PutTypeInList(test))
                foreach (Type type in dc.SortListType(dc._subClasses, dc._mainType))
                {
                    DrawRelation(type.Name, dc.GetName(), dShape);
                }
        }

        private void _NShapeDisplay_Load(object sender, EventArgs e)
        {

        }

        public int CountSubClass(DescriptionClass dc)
        {
            int counter = 0;
            foreach (Type type in dc.SortListType(dc._subClasses, dc._mainType))
            {
                counter++;
            }
            return counter;
        }

        public void DrawClassShape(string s, int x, int y, Dictionary<String, CaptionedShapeBase> dShape)
        {
            CircleBase shape = (CircleBase)_NShapeProject.ShapeTypes["Circle"].CreateInstance();
            shape.Diameter = 100;
            shape.X = x;
            shape.Y = y;
            shape.SetCaptionText(0, s);

            _NShapeDiagram.Shapes.Add(shape);
            if (!dShape.ContainsKey(s))
                dShape.Add(s, shape);
        }

        public void DrawInterfaceShape(string s, int x, int y, Dictionary<String, CaptionedShapeBase> dShape)
        {
            CircleBase shape = (CircleBase)_NShapeProject.ShapeTypes["Circle"].CreateInstance();
            shape.Diameter = 100;
            shape.X = x;
            shape.Y = y;

            ColorStyle myColorStyle = new ColorStyle("test", System.Drawing.Color.Green);
            ColorStyle mySecondColorStyle = new ColorStyle("test", System.Drawing.Color.White);
            FillStyle myFillStyle = new FillStyle("test", myColorStyle, mySecondColorStyle);
            shape.FillStyle = myFillStyle;
            shape.SetCaptionText(0, s);

            _NShapeDiagram.Shapes.Add(shape);

            if(!dShape.ContainsKey(s))
                dShape.Add(s, shape);
        }

        public void DrawMemberClass(string s, int x, int y)
        {
            RectangleBase shape = (RectangleBase)_NShapeProject.ShapeTypes["Ellipse"].CreateInstance();
            shape.Width = 100;
            shape.Height = 50;
            shape.X = x;
            shape.Y = y;
            shape.SetCaptionText(0, s);
             
            _NShapeDiagram.Shapes.Add(shape);
        }

        public void DrawRelation(string class1, string class2, Dictionary<string, CaptionedShapeBase> dShape)
        {
            LineShapeBase line = (LineShapeBase)_NShapeProject.ShapeTypes["Polyline"].CreateInstance();                
            ThickArrow arrow = (ThickArrow)_NShapeProject.ShapeTypes["ThickArrow"].CreateInstance();
            
            line.Connect(ControlPointId.FirstVertex, dShape[class1], ControlPointId.Reference);
            line.Connect(ControlPointId.LastVertex, dShape[class2], ControlPointId.Reference);

            arrow.MoveControlPointTo(1, line.GetControlPointPosition(ControlPointId.FirstVertex).X,
                                    line.GetControlPointPosition(ControlPointId.FirstVertex).Y, 0);
            arrow.MoveControlPointTo(6, line.GetControlPointPosition(ControlPointId.LastVertex).X,
                                    line.GetControlPointPosition(ControlPointId.LastVertex).Y, 0);
                      
            _NShapeDiagram.Shapes.Add(arrow);
        }

    }
}
