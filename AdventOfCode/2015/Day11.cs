namespace AdventOfCode._2015;

public class Day11(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        string line=lines[0];
        while (true)
        {
            
            int count = line.Length - 1;
            bool over = false;
            while (count >= 0)
            {
                char c = line[count];
                if (count == line.Length - 1)
                {
                    (c, over) = GetReplacement(c);
                }
                else if(over)
                {
                    over = false;
                    (c, over) = GetReplacement(c);
                }

                line = line.Substring(0, count) + c + line.Substring(count + 1);
                count--;
                
            }
            
            if (IsValid(line)) {Console.WriteLine(line);break;}
        } 
        
        return 0;
        
        bool IsValid(string line)
        {
            if (line.Contains('i') || line.Contains('o') | line.Contains('l')) return false;
            bool contains3 = false, contains22=false;
            int contains21=-1;
            for (int i = 0; i < line.Length; i++)
            {
                if (i < line.Length - 1 && line[i] == line[i + 1] && contains21==-1) 
                {
                    contains21 = i;
                    continue;
                }
                if (i < line.Length - 1 && line[i] == line[i + 1] && contains21>-1 && i>contains21+1) contains22 = true;

                if (i < line.Length - 2 && ((char)(line[i]+1)) == line[i + 1] && ((char)(line[i]+2)) == line[i + 2]) contains3 = true;

            }

            if (!contains3 || !contains22) return false;
            return true;
        }
    }

    private (char,bool) GetReplacement(char c)
    {
        if (c == 'z') return ('a', true);
        return ((char)((byte)(c) + 1), false);
    }

    public long GetTotalPartB()
    {
        return GetTotalPartA();//diffrent input
    }
}