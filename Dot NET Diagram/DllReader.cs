using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Dot_NET_Diagram
{
    public class DllReader
    {
        Assembly _currentDll;
        public DllReader(string access)
        {
            _currentDll = Assembly.LoadFrom(access);
        }


        public Type[] GetAllTypes()
        {
            MainForm.LogOnDebug("\nRécupération des types: ");
            Type[] typeAssembly = _currentDll.GetTypes();
            return typeAssembly;
        }


        public List<Type> SearchSubClass(Type type)
        {
            MainForm.LogOnDebug(String.Format("Recherche de classe mère pour la classe {0}", type.Name));
            List<Type> subClasses = new List<Type>();
            subClasses.Add(type);
            foreach (Type currentType in _currentDll.GetTypes())
            {
                if (type.IsSubclassOf(currentType))
                    subClasses.Add(currentType);
            }
            return subClasses;
        }


        public List<Type> SearchInterface(Type type)
        {
            MainForm.LogOnDebug(String.Format("\nRécupération des interfaces de la classe {0}", type.Name));
            return type.GetInterfaces().ToList<Type>();
        }


        public List<Type> GetSubClassAndI(Type type)
        {
            List<Type> subClasses = SearchSubClass(type);
            List<Type> interfaces = SearchInterface(type);
            subClasses.AddRange(interfaces);
            subClasses.Remove(type);
            return subClasses;
        }


        public List<MemberInfo> ShowMembers(Type type)
        {
            return type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList<MemberInfo>();
        }
    }
}
