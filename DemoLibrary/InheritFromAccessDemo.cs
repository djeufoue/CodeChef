using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class InheritFromAccessDemo: AccessDemo
    {
        public void Test()
        {
            ProtectedDemo();
            InternalDemo();
            PrivateProtectedDemo();
            ProtectedInternalDemo();
        }
    }
}
