﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEnumerator
{
    class Garage : IEnumerable
    {
        private Car[] carArray = new Car[4];
        public Garage()
        {
            carArray[0] = new Car("Rusty", 30);
            carArray[1] = new Car("Clunker", 55);
            carArray[2] = new Car("Zippy", 30);
            carArray[3] = new Car("Fred", 30);

        }
        public IEnumerator GetEnumerator()
        {
            return actualImplementation();
            //This will get thrown immediately
            throw new Exception("This will get called");

            /* yield return carArray[0];
             yield return carArray[1];
             yield return carArray[2];
             yield return carArray[3]; */
           
            //this is the private function
            IEnumerator actualImplementation()
            {
                foreach (Car c in carArray)
                {
                    yield return c;
                }
            }
        }
        public IEnumerable GetTheCars(bool returnReversed)
        {
            //do some error checking here
            return actualImplementation();
            IEnumerable actualImplementation()
            {
                // Return the items in reverse.
                if (returnReversed)
                {
                    for (int i = carArray.Length; i != 0; i--)
                    {
                        yield return carArray[i - 1];
                    }
                }
                else
                {
                    // Return the items as placed in the array.
                    foreach (Car c in carArray)
                    {
                        yield return c;
                    }
                }
            }
        }
    }
}
