using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace GR
{
    public class Program
    {
        public IList<Item> Items;

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            //app.UpdateInventory();
            app.UpdateInventoryNew();
            var filename = $"inventory_{DateTime.Now:yyyyddMM-HHmmss}.txt";
            var inventoryOutput = JsonConvert.SerializeObject(app.Items, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename), inventoryOutput);

            Console.ReadKey();
        }

        public void UpdateInventory()
        {
            Console.WriteLine("Updating inventory");
            foreach (var item in Items)
            {

                Console.WriteLine(" - Item: {0}", item.Name);
                if (item.Name != "Aged Brie" && !item.Name.Contains("Backstage passes"))
                {
                    if (item.Quality > 0)
                    {
                        //For Conjured item
                        if (item.Name != null && item.Name.Contains("Conjured"))
                        {
                            var qLeft = item.Quality - 2;
                            item.Quality = qLeft > 0 ? qLeft : 0;
                        }
                        else if (item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            item.Quality = item.Quality - 1;
                        }
                    }
                }
                //Brie, Backstage
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;

                        if (item.Name.Contains("Backstage passes"))
                        {
                            if (item.SellIn < 11)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality = item.Quality + 1;
                                }
                            }
                        }
                    }
                }
                //Update SellIn
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn = item.SellIn - 1;
                }

                //if (item.SellIn >= 0) continue;

                //if (item.Name != "Aged Brie")
                //{
                //    if (item.Name.Contains("Backstage passes"))
                //    {
                //        if (item.Quality <= 0) continue;

                //        if (item.Name != "Sulfuras, Hand of Ragnaros")
                //        {
                //            item.Quality = item.Quality - 1;
                //        }
                //    }
                //    else
                //    {
                //        item.Quality = item.Quality - item.Quality;
                //    }
                //}
                //else
                //{
                //    if (item.Quality < 50)
                //    {
                //        item.Quality = item.Quality + 1;
                //    }
                //}
            }
            Console.WriteLine("Inventory update complete");
        }
        /// <summary>
        /// Assumption: item.SellIn == 0 -> today, still able to sell at current item.Quality
        ///             item.SellIn < 0 -> expired
        /// </summary>
        public void UpdateInventoryNew()
        {
            Console.WriteLine("Updating inventory");
            try {
                foreach (var item in Items)
                {
                    Console.WriteLine(" - Item: {0}", item.Name);
                    //Update Quality by Name
                    switch (item)
                    {
                        case Item it when it.Name == "Aged Brie":
                            //if passes expiration date??
                            it.Quality += 1;
                            break;
                        case Item it when it.Name.Contains("Backstage passes"):
                            if (it.SellIn < 0)
                            {
                                it.Quality = 0;
                            }
                            else if (it.SellIn <= 5)
                            {
                                it.Quality += 3;
                            }
                            else if (item.SellIn <= 10)
                            {
                                it.Quality += 2;
                            }
                            else
                            {
                                it.Quality += 1;
                            }
                            break;
                        case Item it when it.Name.Contains("Conjured"):
                            if(it.SellIn < 0)
                            {
                                it.Quality -= 4;
                            }
                            else
                            {
                                it.Quality -= 2;
                            }
                            break;
                        case Item it when it.Name == "Sulfuras, Hand of Ragnaros":
                            //quality wont change
                            break;
                        default:
                            if(item.SellIn < 0)
                            {
                                item.Quality -= 2;
                            }
                            else
                            {
                                item.Quality -= 1;
                            }
                            break;
                    }
                    //update 0 to 50 threshold
                    if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        if (item.Quality > 50)
                        {
                            item.Quality = 50;
                        }
                        else if (item.Quality < 0)
                        {
                            item.Quality = 0;
                        }
                    }
                    //decrement sellIn
                    item.SellIn -= 1;
                }
            }
            catch(Exception e)
            {
#if DEBUG
                Console.WriteLine("There is an error: "+ e.Message);
#else
                Console.WriteLine("There is an error, please try again later");
#endif
            }
            Console.WriteLine("Inventory update complete");
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}