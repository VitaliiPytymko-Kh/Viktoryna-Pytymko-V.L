using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Viktoryna_Pytymko_V.L

{ 
      
         class Program
        {
           static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
           
            Viktoryna viktoryna = new Viktoryna();
            viktoryna.Run();

        }

    }
}

