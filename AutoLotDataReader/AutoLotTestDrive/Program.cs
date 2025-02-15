﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.EF;
using AutoLotDAL.Models;


using System.Data.Entity;
using AutoLotDAL.Repos;
using System.Data.Entity.Infrastructure;

namespace AutoLotTestDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            // Database.SetInitializer(new MyDataIntializer());
            //Console.WriteLine("***** Fun with ADO.NET EF Code First *****\n");
            //using (var context = new AutoLotEntities())
            //{
            //    foreach (Inventory c in context.Inventory)
            //    {
            //        Console.WriteLine(c);
            //    }
            //}
            Console.WriteLine("***** Using a Repository *****\n");
            using (var repo = new InventoryRepo())
            {
                foreach (Inventory c in repo.GetAll())
                {
                    Console.WriteLine(c);
                }
            }
            Console.ReadLine();
        }
        private static void AddNewRecord(Inventory car)
        {
            // Add record to the Inventory table of the AutoLot
            // database.
            using (var repo = new InventoryRepo())
            {
                repo.Add(car);
            }
        }
        private static void UpdateRecord(int carId)
        {
            using (var repo = new InventoryRepo())
            {
                // Grab the car, change it, save!
                var carToUpdate = repo.GetOne(carId);
                if (carToUpdate == null) return;
                carToUpdate.Color = "Blue";
                repo.Save(carToUpdate);
            }
        }
        private static void RemoveRecordByCar(Inventory carToDelete)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Delete(carToDelete);
            }
        }
        private static void RemoveRecordById(int carId, byte[] timeStamp)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Delete(carId, timeStamp);
            }
        }
        private static void TestConcurrency()
        {
            var repo1 = new InventoryRepo();
            //Use a second repo to make sure using a different context
            var repo2 = new InventoryRepo();
            var car1 = repo1.GetOne(1);
            var car2 = repo2.GetOne(1);
            car1.PetName = "NewName";
            repo1.Save(car1);
            car2.PetName = "OtherName";
            try
            {
                repo2.Save(car2);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;
                var dbValues = entry.GetDatabaseValues();
                Console.WriteLine(" ******** Concurrency ************");
                Console.WriteLine("Type\tPetName");
                Console.WriteLine($"Current:\t{currentValues[nameof(Inventory.PetName)]}");
                Console.WriteLine($"Orig:\t{originalValues[nameof(Inventory.PetName)]}");
                Console.WriteLine($"db:\t{dbValues[nameof(Inventory.PetName)]}");
            }
        }
        
    }
}