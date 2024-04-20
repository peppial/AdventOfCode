using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day15(string[] lines):IDay
{
    private Dictionary<string, int[]> ingredients = [];
    private Dictionary<int, int> spoons = [];
    long grandtotal = 100;
    public long GetTotalPartA()
    {
        return GetMax(false);

    }

    private long GetMax(bool partB)
    {
        foreach (string line in lines)
        {
            int[] numbers = line.GetNumbers();
            int col = line.IndexOf(':');
            string name = line.Substring(0, col);
            ingredients.Add(name,numbers);
        }

       
        var index = 0;
        
        var numIngredients = ingredients.Count;
        var total = 100;
        int[] baseList = Enumerable.Range(0, numIngredients).ToArray();
        var currValue = baseList.Select(_ => 0).ToArray();
        currValue[0] = -1;
        
        
        bool GetNext()
        {
            var i = 0;
            do
            {
                if (++currValue[i] == 101)
                {
                    currValue[i] = 0;
                    i++;
                }
                else
                {
                    break;
                }

                if (i == numIngredients - 1)
                    return false;
            } 
            while (true);

            currValue[numIngredients - 1] = 0;
            currValue[numIngredients - 1] = total - currValue.Sum();

            return true;
        }

        long totaltotal = 0;
        
        long capacity = 0;
        long durability = 0;
        long flavor = 0;
        long mixture = 0;
        long calories = 0;
        while (GetNext())
        {
            index = 0;
            capacity = durability = flavor = mixture = calories= 0;
            foreach (var ingredient in ingredients)
            {
                capacity += currValue[index] * ingredient.Value[0];
                durability += currValue[index] * ingredient.Value[1];
                flavor += currValue[index] * ingredient.Value[2];
                mixture += currValue[index] * ingredient.Value[3];
                calories+=currValue[index] * ingredient.Value[4];
                index++;
            }

            var t = (capacity>0?capacity:0) * (durability>0?durability:0) * (flavor>0?flavor:0) * (mixture>0?mixture:0);
            if (t > totaltotal && calories==500) totaltotal = t;
        }

        return totaltotal;
    }


    public long GetTotalPartB()
    {
        return GetMax(true);

    }
    
}