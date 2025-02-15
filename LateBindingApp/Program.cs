﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace LateBindingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Late Binding *****");
            // Try to load a local copy of CarLibrary.
            Assembly a = null;
            try
            {
                a = Assembly.Load("CarLibrary");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            if (a != null)
                CreateUsingLateBinding(a);
            Console.ReadLine();
        }
        static void CreateUsingLateBinding(Assembly asm)
        {
            try
            {
                // Get metadata for the Minivan type.
                Type miniVan = asm.GetType("CarLibrary.MiniVan");
                // Create a Minivan instance on the fly.
                object obj = Activator.CreateInstance(miniVan);
                Console.WriteLine("Created a {0} using late binding!", obj);
                // Get info for TurboBoost.
                MethodInfo mi = miniVan.GetMethod("TurboBoost");
                // Invoke method ('null' for no parameters).
                mi.Invoke(obj, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void InvokeMethodWithArgsUsingLateBinding(Assembly asm)
        {
            try
            {
                // First, get a metadata description of the sports car.
                Type sport = asm.GetType("CarLibrary.SportsCar");
                // Now, create the sports car.
                object obj = Activator.CreateInstance(sport);
                // Invoke TurnOnRadio() with arguments.
                MethodInfo mi = sport.GetMethod("TurnOnRadio");
                mi.Invoke(obj, new object[] { true, 2 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

