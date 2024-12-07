using System.Diagnostics.SymbolStore;
using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day7(string[] lines): IDay
{
    public long GetTotalPartA()
    {
        long total = 0;
        foreach (var line in lines)
        {
            var numbers = line.GetNumbersLong();
            
            string[] operators = Combinatorics.GenerateAllBinaryStrings(numbers.Length-2);
           
            foreach (string oper in operators)
            {
                long sum = numbers[1];
                int cop = 0;
                for (int i = 2; i < numbers.Length; i++)
                {
                    if (oper[cop++] == '0') sum += numbers[i];
                    else sum *= numbers[i];
                }

                if (sum == numbers[0])
                {
                    total += sum;
                    break;
                }
            }
            
        }
        return total;
    }

    public long GetTotalPartB()
    {
        long total = 0;
        foreach (var line in lines)
        {
            var numbers = line.GetNumbersLong();
            
            foreach (var oper in Combinatorics.Variants(numbers.Length-1, 3)) 
            {
                long sum = numbers[1];
                int cop = 0;
                for (int i = 2; i < numbers.Length; i++)
                {
                    if (oper[cop] == 0) sum += numbers[i];
                    else if (oper[cop] == 1) sum *= numbers[i];
                    else sum = long.Parse($"{sum}{numbers[i]}");
                    cop++;
                }

                if (sum == numbers[0])
                {
                    total += sum;
                    break;
                }
            }
            
        }
        return total;
    }
    
}