using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day13(string[] lines): IDay
{
    public long GetTotalPartA()
    {
        return GetTotal(true);
    }
    public long GetTotalPartB()
    {
        return GetTotal(false);
    }
    
    long GetTotal(bool partA)
    {
        long total = 0;
        
        for (int i = 0; i < lines.Length; i += 4)
        {
            var an = lines[i].GetNumbersLong();
            var bn = lines[i+1].GetNumbersLong();
            var price = lines[i+2].GetNumbersLong();
            long ax = an[0];
            long ay = an[1];
            
            long bx = bn[0];
            long by = bn[1];

            long pricex = price[0];
            long pricey = price[1];
            long maxx = 0;
            if (partA) maxx = 100;
            else
            {
                pricex += 10000000000000;
                pricey += 10000000000000;
                maxx = pricex / Math.Min(ax, bx);
            }

            long resA = -1, resB = -1;

            long crosssum = ax * by - bx * ay;
            
            if ((pricex * by - pricey * bx) % crosssum == 0)
            {
                resA = (pricex * by - pricey * bx) / crosssum;
            }

            if ((pricey * ax - pricex * ay) % crosssum == 0)
            {
                resB = (pricey * ax - pricex * ay) / crosssum;
            }
            
            if(resA != -1 && resB != -1)
                total+= resA * 3 + resB;
        }
        return total;
    }
   
}