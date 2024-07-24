namespace AdventOfCode._2015;

public class Day20(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        
        for (int i = 1; i < 1_000_000; i++)
        {
            var divisors = GetDivisors(i);
            var total = divisors.Select(x => x * 10).Sum();
            
            if(total%1_000_000==0) Console.WriteLine(total);
            if (total >= 33_100_000) return i;
        }

        return 0;
    }

    public long GetTotalPartB()
    {
        Dictionary<int, int> count = [];
        for (int i = 1; i < 1_000_000; i++)
        {
            var divisors = GetDivisors(i).ToList();
            divisors.Add(1);
            count.TryAdd(i, 0);
            divisors = divisors.Where(x => count[x] <= 50).ToList();
            var total = divisors.Select(x => x * 11).Sum();

            if (i % 10_000 == 0)
            {
                Console.WriteLine(i);
                Console.WriteLine(total);

            }
            
            foreach (int divisor in divisors)
            {
                count[divisor]+=1;
            }
            if (total >= 33_100_000) return i;
        }

        return 0;
    }
    
    private IEnumerable<int> GetDivisors(int n)
    {
        return Enumerable.Range(2, n).Where(a => n % a == 0);
    }
}