﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegate
{
    class Program
    {
        // This generic delegate can represnet any method
        // returning void and taking a single parameter of type T.
        public delegate void MyGenericDelegate<T>(T arg);

        static void Main(string[] args)
        {
            Console.WriteLine("***** Generic Delegates *****\n");
            // Register targets.
            MyGenericDelegate<string> strTarget = new MyGenericDelegate<string>(StringTarget);
            strTarget("some string data");

            MyGenericDelegate<int> intTarget = new MyGenericDelegate<int>(IntTarget);
            intTarget(9);
            Console.ReadLine();
        }
        static void StringTarget(string arg)
        {
            Console.WriteLine("arg in uppercase is: {0}", arg.ToUpper());
        }
        static void IntTarget(int arg)
        {
            Console.WriteLine("++arg is: {0}", ++arg);
        }
    }

       
    
}
