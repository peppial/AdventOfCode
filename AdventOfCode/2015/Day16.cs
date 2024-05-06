using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day16(string[] lines):IDay
{
   

    record Gift(string SueNumber, string First, int FirstNum, string Second, int SecondNum, string Third, int ThirdNum);
    public long GetTotalPartA()
    {

        Dictionary<string, int> dict = Init();
        foreach (var line in lines)
        {
            var parts = line.Split(new char[]{' ',',',':'},StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Gift gift = new Gift(parts[1], parts[2], int.Parse(parts[3]), parts[4], int.Parse(parts[5]), parts[6],
                int.Parse(parts[7]));

            if (gift.FirstNum == dict[gift.First]
                && gift.SecondNum == dict[gift.Second]
                && gift.ThirdNum == dict[gift.Third]
               )
                return int.Parse(gift.SueNumber);
            
            
        }

        return 0;
    }

    private Dictionary<string, int> Init()
    {
        Dictionary<string, int> dict = [];
        dict.Add("children",3);
        dict.Add("cats",7);
        dict.Add("samoyeds",2);
        dict.Add("pomeranians",3);
        dict.Add("akitas",0);
        dict.Add("vizslas",0);
        dict.Add("goldfish",5);
        dict.Add("trees",3);
        dict.Add("cars",2);
        dict.Add("perfumes",1);

        return dict;
    }

    public long GetTotalPartB()
    {
        Dictionary<string, int> dict = Init();

        
        foreach (var line in lines)
        {
            var parts = line.Split(new char[]{' ',',',':'},StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Gift gift = new Gift(parts[1], parts[2], int.Parse(parts[3]), parts[4], int.Parse(parts[5]), parts[6],
                int.Parse(parts[7]));

            if ( IsMatch(dict, gift.FirstNum,gift.First)
                && IsMatch(dict, gift.SecondNum,gift.Second)
                && IsMatch(dict, gift.ThirdNum,gift.Third)
               )
                return int.Parse(gift.SueNumber);
            
            
        }

        return 0;
        
    }

    private static bool IsMatch(Dictionary<string, int> dict, int number, string name)
    {
        if ((name == "cats" || name == "trees") && number > dict[name]) return true;
        if ((name == "pomeranians" || name == "goldfish") && number < dict[name]) return true;
        if(name != "cats" && name != "trees" && name != "pomeranians" && name != "goldfish")
        return number == dict[name];
        return false;
    }
}