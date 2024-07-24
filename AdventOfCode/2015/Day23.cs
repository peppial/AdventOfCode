using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day23(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        long a = 0,b=0;
        b = GetFinalRegisterValue(a, b);

        return b;
    }
    public long GetTotalPartB()
    {
        long a = 1,b=0;
        b = GetFinalRegisterValue(a, b);

        return b;
    }
    private long GetFinalRegisterValue(long a, long b)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            var instr = lines[i];
            string[] parts = instr.Split(new char[] { ' ', ',' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts[0] == "jmp")
            {
                int offset = int.Parse(parts[1]);
                i += offset-1;
                continue;
            }
            if (parts[0] == "jio")
            {
                if(parts[1]=="a" && a==1 || parts[1]=="b" && b==1)
                {
                    int offset = int.Parse(parts[2]);
                    i += offset-1;
                }
                continue;
            }
            if (parts[0] == "jie")
            {
                if(parts[1]=="a" && a%2==0 || parts[1]=="b" && b%2==0)
                {
                    int offset = int.Parse(parts[2]);
                    i += offset-1;
                }
                continue;
            }
            if (parts[0] == "inc")
            {
                if (parts[1] == "a") a++;
                
                if (parts[1] == "b") b++;
                continue;
            }
            if (parts[0] == "hlf")
            {
                if (parts[1] == "a") a=a/2;
                
                if (parts[1] == "b") b=b/2;
                continue;
            }
            if (parts[0] == "tpl")
            {
                if (parts[1] == "a") a*=3;
                
                if (parts[1] == "b") b*=3;
                continue;
            }
        }

        return b;
    }

  
}