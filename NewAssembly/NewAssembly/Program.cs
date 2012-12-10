using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            DllReader test = new DllReader("GridMapper.exe");
            DescriptionClass dClass;
            List<DescriptionClass> allClasses = new List<DescriptionClass>();
            Type[] typeAssembly = test.GetAllTypes();

            foreach (Type type in typeAssembly)
            {
                dClass = new DescriptionClass(test, type);
                allClasses.Add(dClass);
            }
            foreach (DescriptionClass dc in allClasses)
                dc.printClass();
                     
        }
    }
}
