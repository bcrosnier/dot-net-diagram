using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NewAssembly
{
    class DescriptionClass
    {
        Type _mainType;
        List<Type> _subClasses;
        List<MemberInfo> _memberClass;

        public DescriptionClass(DllReader test, Type type)
        {
            _mainType = type;
            _subClasses = test.GetSubClassAndI(_mainType);
            _memberClass = test.ShowMembers(_mainType);            
        }

        public void printClass()
        {
            Console.WriteLine("Nom: {0}", _mainType.Name);
            Console.WriteLine("Liste des classes mères et interfaces: ");
            foreach(Type type in _subClasses)
                Console.WriteLine(type.Name);
            Console.WriteLine("Liste des variables membres: ");
            foreach (MemberInfo mi in _memberClass)
                Console.WriteLine(mi.Name);
            Console.WriteLine("\n\n\n\n");
        }



    }
}
