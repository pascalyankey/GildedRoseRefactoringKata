using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        const int maxQuality = 50;
        const int minQuality = 0;

        foreach (var item in Items) 
        {
            var isSulfuras = item.Name == "Sulfuras, Hand of Ragnaros";
            var isAgedBrie = item.Name == "Aged Brie";
            var isBackstagePasses = item.Name == "Backstage passes to a TAFKAL80ETC concert";

            if (!isSulfuras)
            {
                item.SellIn--;

                var isMaxQuality = item.Quality == maxQuality;
                var isMinQuality = item.Quality == minQuality;
                var isPassedSellDate = item.SellIn < 0;
                var isTenDaysSell = item.SellIn < 10 && item.SellIn > 4;
                var isFiveDaysSell = item.SellIn < 5;
                var exceedsTenDaysSellQualityLimit = item.Quality + 2 > 50;
                var exceedsFiveDaysSellQualityLimit = item.Quality + 3 > 50;
                var exceedsPassedBySellDateQualityMin = isPassedSellDate && item.Quality - 2 < minQuality;
                var exceedsPassedBySellDateQualityMax = isPassedSellDate && item.Quality + 2 > maxQuality;

                if (isAgedBrie) // Case Aged Brie
                {
                    if (!isMaxQuality)
                    {
                        if (isPassedSellDate)
                        {
                            if (exceedsPassedBySellDateQualityMax)
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
                else if (isBackstagePasses) // Case Backstage passes to a TAFKAL80ETC concert
                {
                    if (isPassedSellDate)
                    {
                        item.Quality = 0;
                    }
                    else
                    {
                        if (!isMaxQuality)
                        {
                            if (isTenDaysSell)
                            {
                                if (exceedsTenDaysSellQualityLimit)
                                {
                                    item.Quality = maxQuality;
                                } 
                                else
                                {
                                    item.Quality += 2;
                                }
                            }
                            else if (isFiveDaysSell)
                            {
                                if (exceedsFiveDaysSellQualityLimit)
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
                else // Case normal items
                {
                    if (!isMinQuality)
                    {
                        if (isPassedSellDate)
                        {
                            if (exceedsPassedBySellDateQualityMin)
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
            }
        }
    }
}