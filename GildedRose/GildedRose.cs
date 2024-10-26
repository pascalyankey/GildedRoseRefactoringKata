using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    const int maxQuality = 50;
    const int minQuality = 0;

    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items) 
        {
            if (!IsSulfuras(item))
            {
                item.SellIn--;

                // Case Aged Brie
                if (IsAgedBrie(item))
                {
                    UpdateAgedBrie(item);
                }
                else if (IsBackstagePasses(item)) // Case Backstage passes to a TAFKAL80ETC concert
                {
                    UpdateBackStagePasses(item);
                }
                else // Case normal items
                {
                    UpdateNormalItem(item);
                }
            }
        }
    }

    private static void UpdateAgedBrie(Item item)
    {
        if (!IsMaxQuality(item))
        {
            if (IsPassedSellDate(item))
            {
                if (ExceedsPassedBySellDateQualityMax(item))
                {
                    item.Quality = maxQuality;
                }
                else
                {
                    item.Quality += 2;
                }
            }
            else
            {
                item.Quality++;
            }
        }
    }

    private static void UpdateBackStagePasses(Item item)
    {
        if (IsPassedSellDate(item))
        {
            item.Quality = minQuality;
        }
        else
        {
            if (!IsMaxQuality(item))
            {
                if (IsTenDaysSell(item))
                {
                    if (ExceedsTenDaysSellQualityLimit(item))
                    {
                        item.Quality = maxQuality;
                    }
                    else
                    {
                        item.Quality += 2;
                    }
                }
                else if (IsFiveDaysSell(item))
                {
                    if (ExceedsFiveDaysSellQualityLimit(item))
                    {
                        item.Quality = maxQuality;
                    }
                    else
                    {
                        item.Quality += 3;
                    }
                }
                else
                {
                    item.Quality++;
                }
            }
        }
    }

    private static void UpdateNormalItem(Item item)
    {
        if (!IsMinQuality(item))
        {
            if (IsPassedSellDate(item))
            {
                if (ExceedsPassedBySellDateQualityMin(item))
                {
                    item.Quality = minQuality;
                }
                else
                {
                    item.Quality -= 2;
                }
            }
            else
            {
                item.Quality--;
            }
        }
    }

    private static bool IsFiveDaysSell(Item item)
    {
        return item.SellIn < 5;
    }

    private static bool ExceedsFiveDaysSellQualityLimit(Item item)
    {
        return item.Quality + 3 > maxQuality;
    }

    private static bool ExceedsTenDaysSellQualityLimit(Item item)
    {
        return item.Quality + 2 > maxQuality;
    }
    private static bool IsTenDaysSell(Item item)
    {
        return item.SellIn < 10 && item.SellIn > 4;
    }

    private static bool IsAgedBrie(Item item)
    {
        return item.Name == "Aged Brie";
    }

    private static bool IsSulfuras(Item item)
    {
        return item.Name == "Sulfuras, Hand of Ragnaros";
    }

    private static bool IsBackstagePasses(Item item)
    {
        return item.Name == "Backstage passes to a TAFKAL80ETC concert";
    }

    private static bool IsMaxQuality(Item item)
    {
        return item.Quality == maxQuality;
    }

    private static bool IsMinQuality(Item item)
    {
        return item.Quality == minQuality;
    }

    private static bool IsPassedSellDate(Item item)
    {
        return item.SellIn < 0;
    }

    private static bool ExceedsPassedBySellDateQualityMax(Item item)
    {
        return IsPassedSellDate(item) && item.Quality + 2 > maxQuality;
    }

    private static bool ExceedsPassedBySellDateQualityMin(Item item)
    {
        return IsPassedSellDate(item) && item.Quality - 2 < minQuality;
    }
}