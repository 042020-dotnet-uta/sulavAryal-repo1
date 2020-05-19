using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace GettingStartedLib
{
    
   
    public class FridgeService : IFridge
    {
        public List<string> Fridge { get; set; } = new List<string>{ "Apple", "Banana", "Orange"};

        public int GetFruitTotal()
        {
            int count = Fridge.Count;
            return count;
        }

        public int AddFruit(string fruit) 
        {
            Fridge.Add(fruit);
            return Fridge.Count;
        }

        public int TakeFruit(string fruit) 
        {
            if (Fridge.Contains(fruit)) 
            {
                Fridge.Remove(fruit);
                return Fridge.Count;
            }
            return 0;
        }

        public List<string> FridgeContents() 
        {
            var fridgeContent = Fridge;
            return fridgeContent;
        }


       
    }
}