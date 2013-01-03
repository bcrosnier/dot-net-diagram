using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClassLibrary
{
    /// <summary>
    /// Test library. Contains dummy properties and methods.
    /// </summary>
    public class MyClass
        : MyClassInterface
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MyClass()
        {
            privateProperty = 42d;
        }

        private double privateProperty;

        private string PublicProperty
        {
            get { return privateProperty.ToString(); }
            set { Double.TryParse( value, out privateProperty ); }
        }

        private int MyPrivateMethod( int a, int b )
        {
            return 42 + a + b;
        }

        public int MyPublicMethod( int a, int b )
        {
            return 72 + a + b;
        }
    }

    /// <summary>
    /// Test child class.
    /// </summary>
    public class MyChildClass
        : MyClass
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MyChildClass()
            : base()
        {
            Console.WriteLine( "dummy" );
        }

        private string PublicProperty
        {
            get { return "lol"; }
            set { return; }
        }
    }

    /// <summary>
    /// Test child class.
    /// </summary>
    public class MyChildClassTwo
        : MyClass
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MyChildClassTwo()
            : base()
        {
            Console.WriteLine( "dummy2" );
        }

        private string PublicProperty
        {
            get { return "lol2"; }
            set { return; }
        }

        public class MyPublicNestedClass
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public MyPublicNestedClass()
            {
                Console.WriteLine( "dummy3" );
            }
        }
    }

    /// <summary>
    /// Test interface.
    /// </summary>
    public interface MyClassInterface
    {
        int MyPublicMethod( int a, int b );
    }
}
