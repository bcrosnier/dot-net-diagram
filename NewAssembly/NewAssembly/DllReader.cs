using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NewAssembly
{
    class DllReader
    {
        Assembly _currentDll;
        public DllReader(string access)
        {
            _currentDll = Assembly.LoadFrom(access);
        }

        public Type[] GetAllTypes()
        {
            Console.WriteLine("\nRécupération des types: ");
            Type[] typeAssembly = _currentDll.GetTypes();
            return typeAssembly;
        }

//Récupére les classes mères
        public List<Type> SearchSubClass(Type type)
        {
            Console.WriteLine("\nRecherche de classe mère pour la classe {0}", type.Name);
            List<Type> subClasses = new List<Type>();
            subClasses.Add(type);
            foreach (Type currentType in _currentDll.GetTypes())
            {
                if (type.IsSubclassOf(currentType))
                    subClasses.Add(currentType);
            }
            return subClasses;
        }

//Récupére les interfaces
        public List<Type> SearchInterface(Type type)
        {
            Console.WriteLine("\nRécupération des interfaces de la classe {0}", type.Name);
            return type.GetInterfaces().ToList<Type>();
        }

//Recherche des classes et interfaces implémentées par le type principal
        public List<Type> GetSubClassAndI(Type type)
        {
            List<Type> subClasses = SearchSubClass(type);
            List<Type> interfaces = SearchInterface(type);
            subClasses.AddRange(interfaces);
            subClasses.Remove(type);
            return subClasses;
        }
    }
}
