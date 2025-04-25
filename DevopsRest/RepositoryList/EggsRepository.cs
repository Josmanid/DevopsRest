using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevopsRest.Models;

namespace DevopsRest.RepositoryList
{
    public class EggsRepository
    {
        //declaring variable named eggs type list
        private List<Egg> eggs;
        private int _nextId = 0;
        public EggsRepository() {
            //instatiating/ creating a empty list of Egg object and assigning to the eggs variable
            eggs = new List<Egg>();
            //then we add some eggs
            eggs.Add(new Egg(_nextId++, "Marabou", 34.32));
            eggs.Add(new Egg(_nextId++, "Arla", 20.99));
            eggs.Add(new Egg(_nextId++, "Kinder", 23.99));
        }
        //Return type Egg? nullable because their is a chance the egg dosent exist
        public Egg? GetById(int id) {
            Egg eggResult = eggs.Find(egg => egg.Id == id);
            return eggResult;
        }

        public List<Egg>? GetAllEggs(int? LowPrice = null,
            int? HighPrice = null,
            string? name = null,
            string? orderBy = null,
            int? amount = null) {
            //gets copy of my list not the list
            List<Egg> eggList = new List<Egg>(eggs);
            if (LowPrice != null)
            {
                eggList = eggList.FindAll(m => m.Price < LowPrice);

            }
            if (HighPrice != null)
            {
                eggList = eggList.Where(m => m.Price > HighPrice).ToList();
            }
            if (name != null)
            {
                eggList = eggList.FindAll(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            }
            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "nameAsc":
                        eggList = eggList.OrderBy(m => m.Name.ToLower()).ToList();
                        break;
                    case "nameDesc":
                        eggList = eggList.OrderByDescending(m => m.Name.ToLower()).ToList();
                        break;
                    case "priceAsc":
                        eggList = eggList.OrderBy(m => m.Price).ToList();
                        break;
                    case "priceDesc":
                        eggList = eggList.OrderByDescending(m => m.Price).ToList();
                        break;
                }


            }
            if (amount.HasValue && amount.Value > 0)
            {
                //Take() is a LINQ method that returns the first N elements from a collection (like a list or array).
                eggList = eggList.Take(amount.Value).ToList();
            }
            return eggList;
        }

        public Egg AddEgg(Egg egg) {
            egg.Id = _nextId++;
            eggs.Add(egg);
            return egg;
        }

        public Egg? RemoveEgg(int id) {
            Egg incomingEgg = GetById(id);
            if (incomingEgg == null)
            {
                throw new ArgumentNullException("The egg is not in the list for removal :(");
            }
            eggs.Remove(incomingEgg);

            return incomingEgg;
        }

        public Egg? UpdateEgg(int id, Egg egg) {
            Egg existingEgg = GetById(id);
            if (existingEgg == null)
            {
                throw new ArgumentNullException("The egg is not there for update :(");
            }
            existingEgg.Name = egg.Name;
            existingEgg.Price = egg.Price;
            return existingEgg;




        }
    }
}
