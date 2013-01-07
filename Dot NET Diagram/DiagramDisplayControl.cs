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
    /// Diagram display user control. Contains all methods to create and display element shapes. Uses NShape.
    /// </summary>
    public partial class DiagramDisplayControl : UserControl
    {
        private Dictionary<String, Shape> _shapeDict = new Dictionary<String, Shape>();
        private List<ThickArrow> _arrowList = new List<ThickArrow>();
        private Dataweb.NShape.Diagram _NShapeDiagram;
        private DllReader dllReader;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DiagramDisplayControl()
        {
            InitializeComponent();

            _xmlStore.DirectoryName = System.IO.Path.GetDirectoryName( Application.ExecutablePath );
            _xmlStore.FileExtension = ".nspj";

            _NShapeProject.Name = ".NET Diagram";
            _NShapeProject.AddLibrary( typeof( Ellipse ).Assembly, false );
            _NShapeProject.Create();

            _NShapeDiagram = new Diagram( "diagram" );
            _NShapeDiagram.Height = _NShapeDisplay.Height+1000;
            _NShapeDiagram.Width = _NShapeDisplay.Width+1000;

            _NShapeDisplay.Diagram = _NShapeDiagram;
            textBox1.ReadOnly = true;

            // Init custom styles for shape FillStyles

            ColorStyle greenColorStyle = new ColorStyle( "green", System.Drawing.Color.Green );
            ColorStyle whiteColorStyle = new ColorStyle( "white", System.Drawing.Color.White );
            ColorStyle yellowColorStyle = new ColorStyle( "yellow", System.Drawing.Color.Yellow );
            ColorStyle redColorStyle = new ColorStyle( "red", System.Drawing.Color.Red );

            FillStyle redWhiteFillStyle = new FillStyle( "red-white", redColorStyle, whiteColorStyle );
            FillStyle greenWhiteFillStyle = new FillStyle( "green-white", greenColorStyle, whiteColorStyle );
            FillStyle yellowWhiteFillStyle = new FillStyle( "yellow-white", yellowColorStyle, whiteColorStyle );

            _NShapeProject.Design.FillStyles.Add( redWhiteFillStyle, redWhiteFillStyle );
            _NShapeProject.Design.FillStyles.Add( greenWhiteFillStyle, greenWhiteFillStyle );
            _NShapeProject.Design.FillStyles.Add( yellowWhiteFillStyle, yellowWhiteFillStyle );
        }

        private void _NShapeDisplay_Layout( object sender, LayoutEventArgs e )
        {
            _NShapeDiagram.Height = _NShapeDisplay.Height;
            _NShapeDiagram.Width = _NShapeDisplay.Width;
        }

        /// <summary>
        /// Creates and displays all shapes from a given assembly.
        /// </summary>
        /// <param name="assembly">Assembly to load</param>
        public void loadAssembly( Assembly assembly )
        {
            /********************Algorithme de placement *********************************/
            dllReader = new DllReader(assembly.Location);

            /**********Interfaces**************/
            int x = 100;
            int y = 100;
            int xCount;
        
            IEnumerable<DescriptionClass> requete = from dc in PutInterfaceInList(dllReader)
                                                    orderby CountNbTimeCall(dc._mainType, dllReader)
                                                    select dc;
            List<DescriptionClass> interfaceList = requete.ToList<DescriptionClass>();
            int index = 0;

            while (index != interfaceList.Count)
            {
                if (interfaceList.ElementAt(index)._subClasses.Count == 0)
                {
                    DrawInterfaceShape(interfaceList.ElementAt(index).GetName(), x, y, _shapeDict);
                    interfaceList.RemoveAt(index);
                    index--;
                    x += 200;
                }
                index++;
            }

            y += 400;
            x = 100;
            index = 0;
            while (index != interfaceList.Count)
            {
                if (CountNbTimeCall(interfaceList.ElementAt(index)._mainType, dllReader) == 0)
                {
                    DrawInterfaceShape(interfaceList.ElementAt(index).GetName(), x, y, _shapeDict);
                    interfaceList.RemoveAt(index);
                    index--;
                    x += 200;
                }
                index++;
            }
            y -= 200;
            x = 600;

            foreach (DescriptionClass dc in interfaceList)
            {
                DrawInterfaceShape(dc.GetName(), x, y, _shapeDict);
                x += 200;
            }

            foreach (DescriptionClass dc in PutInterfaceInList(dllReader))
                foreach (Type type in dc.SortListType(dc._subClasses, dc._mainType))
                {
                    DrawLineRelation(type.Name, dc.GetName(), _shapeDict);
                }

            /************Classes***********************/

            x = 0;
            y = 800;
            requete = from dc in PutClassInList(dllReader)
                      orderby CountNbTimeCall(dc._mainType, dllReader)
                      select dc;
            List<DescriptionClass> classList = requete.ToList<DescriptionClass>();
            index = 0;

            while (index != classList.Count)
            {
                if (classList.ElementAt(index)._subClasses.Count == 0)
                {
                    if(classList.ElementAt(index)._mainType.IsValueType)
                        DrawStructShape(classList.ElementAt(index).GetName(), x, y, _shapeDict);
                    else
                        DrawClassShape(classList.ElementAt(index).GetName(), x, y, _shapeDict);
                    xCount = 0;
                    foreach (Type type in classList.ElementAt(index)._nestedClass)
                    {
                        DrawNestedClassShape(type.Name, x + xCount, y - 200, _shapeDict);
                        xCount += 100;
                    }
                    classList.RemoveAt(index);
                    index--;
                    x += 200;
                }

                index++;
            }
            y += 400;
            x = 100;
            index = 0;
            while (index != classList.Count)
            {
                if (CountNbTimeCall(classList.ElementAt(index)._mainType, dllReader) == 0)
                {
                    if (classList.ElementAt(index)._mainType.IsValueType)
                        DrawStructShape(classList.ElementAt(index).GetName(), x, y, _shapeDict);
                    else
                        DrawClassShape(classList.ElementAt(index).GetName(), x, y, _shapeDict);
                    classList.RemoveAt(index);
                    index--;
                    x += 200;
                }

                index++;
            }
            y -= 200;
            x = 600;

            foreach (DescriptionClass dc in classList)
            {
                DrawClassShape(dc.GetName(), x, y, _shapeDict);
                x += 200;
            }

            foreach (DescriptionClass dc in PutClassInList(dllReader))
                foreach (Type type in dc.SortListType(dc._subClasses, dc._mainType))
                {
                    if (type.IsInterface)
                        DrawLineRelation(type.Name, dc.GetName(), _shapeDict);
                    else
                        DrawLineRelation(type.Name, dc.GetName(), _shapeDict);
                }

            foreach (DescriptionClass dc in PutClassInList(dllReader))
                foreach (Type type in dc._nestedClass)
                {
                    DrawLineRelation(type.Name, dc.GetName(), _shapeDict);
                }
          
        }

/***********************Fin d'algo*************************************/


        public int CountSubClass( DescriptionClass dc )
        {
            int counter = 0;
            foreach ( Type type in dc.SortListType( dc._subClasses, dc._mainType ) )
            {
                counter++;
            }
            return counter;
        }

        public int CountNbTimeCall(Type type, DllReader test)
        {
            int counter = 0;
            foreach (DescriptionClass dc in DescriptionClass.PutTypeInList(test))
            {
                if (!dc._mainType.IsInterface)
                {
                    foreach (Type t in dc._subClasses)
                        if (t == type)
                            counter++;
                }           
            }
            return counter;
        }

        /// <summary>
        /// Draws a circle shape containing a string at a given position and a given FillStyle, and insert it in the shape dictionary.
        /// Used by DrawClassShape, DrawInterfaceShape, etc.
        /// </summary>
        /// <param name="contentString">String to display on the shape</param>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        /// <param name="shapeDict">Shape dictionary to edit</param>
        /// <param name="fillStyle">FillStyle to use</param>
        private void DrawElementShape( string contentString, int x, int y, Dictionary<String, Shape> shapeDict, FillStyle fillStyle = null )
        {
            CircleBase shape = (CircleBase) _NShapeProject.ShapeTypes["Circle"].CreateInstance();

            shape.Diameter = 100;
            shape.X = x;
            shape.Y = y;

            shape.SetCaptionText( 0, contentString );

            if( fillStyle != null )
                shape.FillStyle = fillStyle;

            shape.SecurityDomainName = 'A'; // User permissions

            _NShapeDiagram.Shapes.Add( shape );
            if ( !shapeDict.ContainsKey( contentString ) )
                shapeDict.Add( contentString, shape );
        }

        public void DrawClassShape( string s, int x, int y, Dictionary<String, Shape> dShape )
        {
            DrawElementShape( s, x, y, dShape, null );
        }

        public void DrawInterfaceShape( string s, int x, int y, Dictionary<String, Shape> dShape )
        {
            DrawElementShape( s, x, y, dShape, _NShapeProject.Design.FillStyles["green-white"] );
        }

        public void DrawNestedClassShape(string s, int x, int y, Dictionary<String, Shape> dShape)
        {
            DrawElementShape( s, x, y, dShape, _NShapeProject.Design.FillStyles["yellow-white"] );
        }

        public void DrawStructShape(string s, int x, int y, Dictionary<String, Shape> dShape)
        {
            DrawElementShape( s, x, y, dShape, _NShapeProject.Design.FillStyles["red-white"] );
        }

        /// <summary>
        /// Draw a line connection between two shape classes, provided the class exists in given dictionary
        /// </summary>
        /// <param name="class1Name">Class 1 name. (Departing)/</param>
        /// <param name="class2Name">Class 2 name. (Arrival)</param>
        /// <param name="shapeDictionary">Shape dictionary to use</param>
        public void DrawLineRelation(string class1Name, string class2Name, Dictionary<string, Shape> shapeDictionary)
        {
            if (!shapeDictionary.ContainsKey(class1Name))
            {
                MainForm.LogOnDebug( "DrawLineRelation: Departing class missing from dictionary: " + class1Name );
                return;
            }

            if (!shapeDictionary.ContainsKey(class2Name))
            {
                MainForm.LogOnDebug( "DrawLineRelation: Arrival class missing from dictionary: " + class2Name );
                return;
            }

            Polyline line = (Polyline) _NShapeProject.ShapeTypes["Polyline"].CreateInstance();
            line.EndCapStyle = _NShapeProject.Design.CapStyles.ClosedArrow;

            line.Connect(ControlPointId.FirstVertex, shapeDictionary[class1Name], ControlPointId.Reference);
            line.Connect(ControlPointId.LastVertex, shapeDictionary[class2Name], ControlPointId.Reference);

            _NShapeDiagram.Shapes.Add(line);
        }

        /// <summary>
        /// Draw an arrow shape and a line connection between two classes in the diagram, provided the class exists in given dictionary
        /// </summary>
        /// <param name="class1Name">Class 1 name. (Departing)/</param>
        /// <param name="class2Name">Class 2 name. (Arrival)</param>
        /// <param name="shapeDictionary">Shape dictionary to use</param>
        public void DrawShapeRelation( string class1Name, string class2Name, Dictionary<string, Shape> shapeDictionary )
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
            _arrowList.Add(arrow);
        }

        public void DrawRelationNested(string class1Name, string class2Name, Dictionary<string, Shape> shapeDictionary)
        {
            if (!shapeDictionary.ContainsKey(class1Name))
            {
                MainForm.LogOnDebug("DrawRelation: Departing class missing from dictionary: " + class1Name);
                return;
            }

            if (!shapeDictionary.ContainsKey(class2Name))
            {
                MainForm.LogOnDebug("DrawRelation: Arrival class missing from dictionary: " + class2Name);
                return;
            }
            LineShapeBase line = (LineShapeBase)_NShapeProject.ShapeTypes["Polyline"].CreateInstance();
            ThickArrow arrow = (ThickArrow)_NShapeProject.ShapeTypes["ThickArrow"].CreateInstance();

            line.Connect(ControlPointId.FirstVertex, shapeDictionary[class1Name], ControlPointId.Reference);
            line.Connect(ControlPointId.LastVertex, shapeDictionary[class2Name], ControlPointId.Reference);

            arrow.FillStyle = _NShapeProject.Design.FillStyles["red-white"];


            arrow.MoveControlPointTo(1, line.GetControlPointPosition(ControlPointId.FirstVertex).X,
                                    line.GetControlPointPosition(ControlPointId.FirstVertex).Y, 0);
            arrow.MoveControlPointTo(6, line.GetControlPointPosition(ControlPointId.LastVertex).X,
                                    line.GetControlPointPosition(ControlPointId.LastVertex).Y, 0);

            _NShapeDiagram.Shapes.Add(arrow);
            _arrowList.Add(arrow);
            
        }

        public void DrawRelationInterface(string class1Name, string class2Name, Dictionary<string, Shape> shapeDictionary)
        {
            if (!shapeDictionary.ContainsKey(class1Name))
            {
                MainForm.LogOnDebug("DrawRelation: Departing class missing from dictionary: " + class1Name);
                return;
            }

            if (!shapeDictionary.ContainsKey(class2Name))
            {
                MainForm.LogOnDebug("DrawRelation: Arrival class missing from dictionary: " + class2Name);
                return;
            }
            LineShapeBase line = (LineShapeBase)_NShapeProject.ShapeTypes["Polyline"].CreateInstance();
            ThickArrow arrow = (ThickArrow)_NShapeProject.ShapeTypes["ThickArrow"].CreateInstance();

            line.Connect(ControlPointId.FirstVertex, shapeDictionary[class1Name], ControlPointId.Reference);
            line.Connect(ControlPointId.LastVertex, shapeDictionary[class2Name], ControlPointId.Reference);

            arrow.FillStyle = _NShapeProject.Design.FillStyles["green-white"];

            arrow.MoveControlPointTo(1, line.GetControlPointPosition(ControlPointId.FirstVertex).X,
                                    line.GetControlPointPosition(ControlPointId.FirstVertex).Y, 0);
            arrow.MoveControlPointTo(6, line.GetControlPointPosition(ControlPointId.LastVertex).X,
                                    line.GetControlPointPosition(ControlPointId.LastVertex).Y, 0);

            _NShapeDiagram.Shapes.Add(arrow);
            _arrowList.Add(arrow);

        }

        public List<DescriptionClass> PutInterfaceInList(DllReader dll)
        {
            List<DescriptionClass> lDc = new List<DescriptionClass>();
            foreach (DescriptionClass dc in DescriptionClass.PutTypeInList(dll))
            {
                if(dc._mainType.IsInterface)
                    lDc.Add(dc);
            }
            return lDc;
        }

        public List<DescriptionClass> PutClassInList(DllReader dll)
        {
            List<DescriptionClass> lDc = new List<DescriptionClass>();
            foreach (DescriptionClass dc in DescriptionClass.PutTypeInList(dll))
            {
                if (!dc._mainType.IsInterface && !dc._mainType.IsNested)
                    lDc.Add(dc);
            }
            return lDc;
        }

        public void DrawAllRelation(DllReader dll)
        {
            foreach (DescriptionClass dc in DescriptionClass.PutTypeInList(dll))
            {
                if (dc._mainType.IsInterface)
                    foreach (Type type in dc.SortListType(dc._subClasses, dc._mainType))
                    {
                        DrawRelationInterface(type.Name, dc.GetName(), _shapeDict);
                    }
            }
            foreach (DescriptionClass dc in PutClassInList(dll))
            {                  
                    foreach (Type type in dc.SortListType(dc._subClasses, dc._mainType))
                    {
                        if (type.IsInterface)
                            DrawRelationInterface(type.Name, dc.GetName(), _shapeDict);
                        else
                            DrawShapeRelation(type.Name, dc.GetName(), _shapeDict);
                    }
                    
                    foreach (Type type in dc._nestedClass)
                    {
                        DrawRelationNested(type.Name, dc.GetName(), _shapeDict);
                    }
            }
        }

        public void RemoveAllRelation()
        {
            foreach (ThickArrow ta in _arrowList)
            {
                _NShapeDiagram.Shapes.Remove(ta);
            }
            _arrowList = new List<ThickArrow>();
        }

        private void _NShapeDisplay_ShapeClick(object sender, Dataweb.NShape.Controllers.DiagramPresenterShapeClickEventArgs e)
        {
            textBox1.Clear();
            string searchName = string.Empty;
            Type searchType = null;
            for (int i = 0; i < _shapeDict.Count; i++)
            {
                if (_shapeDict.ElementAt(i).Value == e.Shape)
                {
                    searchName = _shapeDict.ElementAt(i).Key;
                    break;
                }
            }

            textBox1.AppendText(searchName + "\n");
            foreach (Type type in dllReader.GetAllTypes())
            {              
                if (type.Name == searchName)
                {                  
                    searchType = type;
                    break;
                }
            }
      
            DescriptionClass dc = new DescriptionClass(dllReader, searchType);
            textBox1.AppendText("Propriétés: \n");
            if(dc._property!=null)
                foreach (PropertyInfo pi in dc._property)
                    textBox1.AppendText("\t"+pi.Name + "\n");
            textBox1.AppendText("Champs: \n");
            if (dc._field != null)
                foreach (FieldInfo fi in dc._field)
                    textBox1.AppendText("\t" + fi.Name + "\n");
            textBox1.AppendText("Methodes: \n");
            if (dc._method != null)
                foreach (MethodInfo mi in dc._method)
                    textBox1.AppendText("\t" + mi.Name + "\n");
            
        }
    }
}
