using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day5(string[] lines): IDay
{
    public long GetTotalPartA()
    {
        return Sum(true);
    }
    public long GetTotalPartB()
    {
        return Sum(false);
    }
    private long Sum(bool partA)
    {
        List<string> order = [];
        List<string> pages = [];
        bool isOrder = true;
        foreach (var line in lines)
        {
            if (line == "") {isOrder = false;continue;}
            if(isOrder) order.Add(line);
            else 
                pages.Add(line);
            
        }
        List<string> okpages = [];
        List<string> failpages = [];
        foreach (var page in pages)
        {
            var numbers = page.GetNumbers();
            
            bool ok = true;
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i+1; j < numbers.Length; j++)
                {
                    if (!order.Contains($"{numbers[i]}|{numbers[j]}"))
                    {
                        ok = false;
                        if(!failpages.Contains(page)) failpages.Add(page);
                    }
                }
            }
            if(ok) okpages.Add(page);
        }

        long sum = 0;
        if (partA)
        {
            foreach (var page in okpages)
            {
                var numbers = page.GetNumbers();
                sum += numbers[(numbers.Length + 1) / 2 - 1];
            }
        }
        else
        {
            for(int p = 0; p < failpages.Count; p++)
            {
                var page = failpages[p];
                var numbers = page.GetNumbers();
                bool repeat = true;
                while (repeat)
                {
                    bool ok = true;
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        for (int j = i + 1; j < numbers.Length; j++)
                        {
                            if (!order.Contains($"{numbers[i]}|{numbers[j]}"))
                            {
                                (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
                                page = failpages[p] = string.Join(',', numbers);
                                ok = false;

                            }
                        }
                    }
                    repeat = !ok;
                }
            }
            foreach (var page in failpages)
            {
                var numbers = page.GetNumbers();
                sum += numbers[(numbers.Length + 1) / 2 - 1];
            }
        }

        return sum;
    }

   
}