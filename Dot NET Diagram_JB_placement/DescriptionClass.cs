using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Dot_NET_Diagram
{
     public class DescriptionClass
    {
        public Type _mainType;
        public List<Type> _subClasses;
        List<MemberInfo> _memberClass;

        public DescriptionClass(DllReader test, Type type)
        {
            _mainType = type;
            _subClasses = SortListType(test.GetSubClassAndI(_mainType), _mainType);
            _memberClass = SortListMi(test.ShowMembers(_mainType));       

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

        public List<MemberInfo> SortListMi(List<MemberInfo> listMi)
        {
            List<MemberInfo> lIndex = new List<MemberInfo>();
            foreach (MemberInfo mi in listMi)
            {
                if (mi.Name == "ToString" || mi.Name == "GetHashCode" || mi.Name == "Equals" || mi.Name == "GetType")
                {
                    lIndex.Add(mi);
                }
            }
            foreach (MemberInfo index in lIndex)
            {
                listMi.Remove(index);
            }
            return listMi;
        }
    }
}
