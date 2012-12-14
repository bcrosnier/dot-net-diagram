using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;


namespace Dot_NET_Diagram
{
     public class DescriptionClass
    {
        public Type _mainType;
        public List<Type> _subClasses;
        public List<Type> _nestedClass;
        public List<PropertyInfo> _property;
        public List<FieldInfo> _field;
        public List<MethodInfo> _method;

        public DescriptionClass(DllReader test, Type type)
        {
            _mainType = type;
            _subClasses = SortListSubClass(test.GetParentsAndInterfaces(_mainType), test);
            _nestedClass = type.GetNestedTypes().ToList();
            _property = type.GetProperties().ToList();
            _field = SortListFi(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList());
            _method = SortListMi(type.GetMethods().ToList());
        }

        public string GetName()
        {
            return this._mainType.Name;
        }

        public bool IsAnInterface()
        {
            return _mainType.IsInterface;
        }

        public void printClass()
        {
            Debug.WriteLine("Nom: {0}", _mainType.Name);
            Debug.WriteLine("Liste des classes mères et interfaces: ");
            foreach (Type type in _subClasses)
                Debug.WriteLine(type.Name);
            if (_nestedClass.Count > 0)
            {
                Debug.WriteLine("\nClasse imbriqué: ");
                foreach (Type type in _nestedClass)
                    Debug.WriteLine(type.Name);
            }
            Debug.WriteLine("\nListe des membres: \n");

            //Recherche et affichage des "get" et "set" pour les propriétés
            Debug.WriteLine("   Propriété: \n");
            IEnumerable<MethodInfo> getOption;
            foreach (PropertyInfo pi in _property)
            {
                Debug.Write("\t" + pi.Name + " {");
                getOption = from mi in _mainType.GetMethods()
                            where mi.Name == "get_" + pi.Name
                            select mi;
                if (getOption != null && getOption.ToList<MethodInfo>().Count > 0)
                    Debug.Write("get; ");
                getOption = null;
                getOption = from mi in _mainType.GetMethods()
                            where mi.Name == "set_" + pi.Name
                            select mi;
                if (getOption != null && getOption.ToList<MethodInfo>().Count > 0)
                    Debug.Write("set; ");
                getOption = null;
                Debug.WriteLine("}");
            }

            Debug.WriteLine("   Champs: \n");

            foreach (FieldInfo fi in _field)
            {
                Debug.WriteLine( "\t" + fi.Name );
            }

            Debug.WriteLine("   Method: \n");
            foreach (MethodInfo mi in _method)
            {
                Debug.WriteLine( "\t" + mi.Name );
            }

            Debug.Flush();
        }

        //Triage de liste FieldInfo, enlève les attributs inutiles
        public List<FieldInfo> SortListFi(List<FieldInfo> listFi)
        {
            List<FieldInfo> lIndex = new List<FieldInfo>();
            foreach (PropertyInfo pi in _mainType.GetProperties())
            {
                foreach (FieldInfo fi in listFi)
                {
                    if (fi.Name == "<" + pi.Name + ">k__BackingField")
                    {
                        lIndex.Add(fi);
                    }
                }
            }
            foreach (FieldInfo index in lIndex)
            {
                listFi.Remove(index);
            }

            return listFi;
        }


        //triage de liste MethodInfo, enlève les propriétés et les méthodes inutiles 
        public List<MethodInfo> SortListMi(List<MethodInfo> listMi)
        {
            List<MethodInfo> lIndex = new List<MethodInfo>();
            foreach (PropertyInfo pi in _mainType.GetProperties())
            {
                foreach (MethodInfo mi in listMi)
                {
                    if (mi.Name == "get_" + pi.Name || mi.Name == "set_" + pi.Name)
                    {
                        lIndex.Add(mi);
                    }
                }
            }

            foreach (MethodInfo mi in listMi)
            {
                if (mi.Name == "ToString" || mi.Name == "GetHashCode" || mi.Name == "Equals" || mi.Name == "GetType")
                {
                    lIndex.Add(mi);
                }
            }
            foreach (MethodInfo index in lIndex)
            {
                listMi.Remove(index);
            }

            return listMi;
        }

        //triage de liste subClasse pour enlever les classes déjà utilisé par les classes mères.
        public List<Type> SortListSubClass(List<Type> sClass, DllReader test)
        {

            List<Type> lIndex = new List<Type>();
            foreach (Type type in sClass)
            {
                foreach (Type t in test.GetParentsAndInterfaces(type))
                {
                    if (sClass.Contains(t))
                        lIndex.Add(t);
                }
            }
            foreach (Type t in lIndex)
                sClass.Remove(t);
            return sClass;
        }

        public static List<DescriptionClass> PutTypeInList(DllReader dllReader)
        {
            List<DescriptionClass> allClasses = new List<DescriptionClass>();
            DescriptionClass dClass;
            foreach (Type type in dllReader.GetAllTypes())
            {
                dClass = new DescriptionClass(dllReader, type);
                allClasses.Add(dClass);
            }
            return allClasses;
        }

        public List<Type> SortListType(List<Type> l, Type mainType)
        {
            l.Remove(mainType);
            return l;
        }
    }
}
