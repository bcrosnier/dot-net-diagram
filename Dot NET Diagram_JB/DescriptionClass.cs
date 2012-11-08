using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Dot_NET_Diagram
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

        public string GetName()
        {
            return this._mainType.Name;
        }

        public bool IsAnInterface()
        {
            return _mainType.IsInterface;
        }

        public List<MemberInfo> GetListMember()
        {
            return this._memberClass;
        }

        public void printClass()
        {
            Console.WriteLine("Nom: {0}", _mainType.Name);
            Console.WriteLine("\nListe des classes mères et interfaces: ");
            foreach (Type type in _subClasses)
                Console.WriteLine(type.Name);
            Console.WriteLine("\nListe des variables membres: ");
            foreach (MemberInfo mi in _memberClass)
                Console.WriteLine(mi.Name);
            Console.WriteLine("\n\n\n\n");
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
    }
}
