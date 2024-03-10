using AdventOfCode.Extensions;

namespace AdventOfCode._2022;

public class Day7(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        const int max = 100000;
        int grandTotal = 0;
        int total = 0;
        bool started = false;
        string name = "";
        Dictionary<string, int> dict = [];
        Dictionary<string, string> containedDirs = [];
        for(int i=0;i<lines.Length;i++)
        {
            string line = lines[i];

           /* if (line=="$ ls")
            {
                name = lines[i - 1].Split(' ')[1];
                started = true;
                if(!dict.ContainsKey(name)) dict[name] = 0;
                continue;
            }*/

            if (line.Contains("$ cd "))
            {
                name = line.Split(' ')[2];
                started = true;
                if(!dict.ContainsKey(name)) dict[name] = 0;
                continue;
            }

            if (line.StartsWith("dir "))
            {
                string dir = line.Split(' ')[1];
                containedDirs[name] = dir;
                continue;
            }
            if (char.IsDigit(line[0]))
            {
                var nums = line.GetNumbers();
                int num = nums.Length>0?nums[0]:0;
                dict[name] += num;
            }
            else
            {
                started = false;
            }
            
     }
        foreach(var key in dict.Keys)
        {   
            if(dict[key]<max) grandTotal += dict[key];
            if (containedDirs.ContainsKey(key) && dict[containedDirs[key]]<max) grandTotal += dict[containedDirs[key]];
            Console.WriteLine("grandtotal " + dict[key]);
        }
        return grandTotal;
    }

    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
}