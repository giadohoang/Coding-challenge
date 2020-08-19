using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GR.Tests
{
    public class TestAssemblyTests
    {
        private readonly Program _app;

        public TestAssemblyTests()
        {
            _app = new Program
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 1},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item
                    {
                        Name = "Backstage passes to a D498FJ9FJ2N concert",
                        SellIn = 10,
                        Quality = 30
                    },
                    new Item
                    {
                        Name = "Backstage passes to a FH38F39DJ39 concert",
                        SellIn = 5,
                        Quality = 33
                    },
                    new Item
                    {
                        Name = "Backstage passes to a I293JD92J44 concert",
                        SellIn = 6,
                        Quality = 27
                    },
                    new Item
                    {
                        Name = "Backstage passes to a O2848394820 concert",
                        SellIn = 1,
                        Quality = 13
                    },
                    new Item
                    {
                        Name = "Backstage passes to a DEEEADMEEET concert",
                        SellIn = 0,
                        Quality = 25
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            _app.UpdateInventory();
        }

        [Fact]
        public void DexterityVest_SellIn_ShouldDecreaseByOne()
        {
            Assert.Equal(9, _app.Items.First(x => x.Name == "+5 Dexterity Vest").SellIn);
        }

        [Fact]
        public void DexterityVest_Quality_ShouldDecreaseByOne()
        {
            Assert.Equal(19, _app.Items.First(x => x.Name == "+5 Dexterity Vest").Quality);
        }
    }
}