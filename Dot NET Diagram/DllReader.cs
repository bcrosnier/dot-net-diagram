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

        /// <summary>
        /// DLL reader instance. Loads assembly.
        /// </summary>
        /// <param name="assemblyFileName">Filename of assembly to load.</param>
        public DllReader(string assemblyFileName )
        {
            _currentDll = Assembly.LoadFrom( assemblyFileName );
        }

        /// <summary>
        /// Get types defined in the loaded assembly.
        /// </summary>
        /// <returns>Array with all types from assembly.</returns>
        public Type[] GetAllTypes()
        {
            MainForm.LogOnDebug( "\nRécupération des types: " );

            Type[] typeAssembly = _currentDll.GetTypes();

            return typeAssembly;
        }

        /// <summary>
        /// Get the parent types, derived from parameter type.
        /// </summary>
        /// <param name="type">Type to get the parent types from</param>
        /// <returns>Parent type(s) of type, can be empty.</returns>
        public List<Type> GetParentTypesOf( Type type )
        {
            MainForm.LogOnDebug( String.Format( "Recherche de classe mère pour la classe {0}", type.Name ) );

            List<Type> parentTypes = new List<Type>();

            parentTypes.Add( type );

            foreach ( Type currentType in _currentDll.GetTypes() )
            {
                if ( type.IsSubclassOf( currentType ) )
                    parentTypes.Add( currentType );
            }
            return parentTypes;
        }

        /// <summary>
        /// Get interfaces implemented by a given type.
        /// </summary>
        /// <param name="type">Target type</param>
        /// <returns>List: interfaces implemented by type.</returns>
        public List<Type> GetInterfacesOf( Type type )
        {
            MainForm.LogOnDebug( String.Format( "\nRécupération des interfaces de la classe {0}", type.Name ) );

            return type.GetInterfaces().ToList<Type>();
        }

        /// <summary>
        /// Get the parent derived from, and interfaces implemented by given type.
        /// </summary>
        /// <param name="type">Type to get parent and interfaces from.</param>
        /// <returns>List of types, including parents and interfaces. Can be empty.</returns>
        public List<Type> GetParentsAndInterfaces( Type type )
        {
            List<Type> parentTypes = GetParentTypesOf( type );
            List<Type> interfaces = GetInterfacesOf( type );

            parentTypes.AddRange( interfaces );
            parentTypes.Remove( type );
            return parentTypes;
        }

        /// <summary>
        /// Get members of given type. Binding constraints are defined here.
        /// </summary>
        /// <param name="type">Target type to get members from.</param>
        /// <returns>List of members. Can be empty.</returns>
        public List<MemberInfo> GetMembersOf( Type type )
        {
            return type.GetMembers( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance ).ToList<MemberInfo>();
        }
    }
}
