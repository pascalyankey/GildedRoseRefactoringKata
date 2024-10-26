using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void foo()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("foo", Items[0].Name);
    }

    [Fact]
    public void UpdateQuality_ItemNameContainingSulfuras_ShouldNotBeAltered()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(80, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_NormalItemWithPassedSellByDate_DegradesTwice()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = -1, Quality = 5 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(3, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_Item_ShouldNeverBeNegative()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = -1, Quality = 0 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(0, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_AgedBrieItem_Increases()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 20 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(21, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_AgedBrieItemWithPassedSellByDate_IncreasesTwice()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = -1, Quality = 20 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(22, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_AgedBrieItem_IsNeverMoreThanFifty()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 15, Quality = 50 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(50, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_BackstagePassesItemSellInTenDays_IncreasesByTwo()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 40 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(42, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_BackstagePassesItemSellInFiveDays_IncreasesByThree()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 40 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(43, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_BackstagePassesItemWithPassedSellByDate_DropsToZero()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 40 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(0, Items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_ConjuredItemWithPassedSellByDate_DegradesTwice()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = -1, Quality = 30 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(28, Items[0].Quality);
    }
}