using System.Text.RegularExpressions;

namespace AdventOfCode._2016;

public class Day7(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        int count = 0;
        foreach (string line in lines)
        {
            bool inBracket = false;
            bool abbaFound = false;
            bool wrongAbbaFound = false;
            string stemp = "";
            foreach (char c in line)
            {
                if (c == '[') 
                {
                    if(ContainsAbba(stemp)) abbaFound = true;
                    inBracket=true;
                    stemp = "";
                }
                else if (c == ']')
                {
                    if (ContainsAbba(stemp)) {wrongAbbaFound=true;};
                    inBracket=false;
                    stemp = "";
                }
                else
                stemp += c;
            }

            if (!inBracket && ContainsAbba(stemp)) abbaFound = true;
            if (abbaFound && !wrongAbbaFound) count++;
        }

        return count;
    }

    public long GetTotalPartB()
    {
        int count = 0;
        foreach (string line in lines)
        {
            if(SupportsSSL(line)) count++;
            
        }

        return count;
    }
    private bool SupportsSSL(string input)
    {
        string[] ipv7 = Regex.Split(input, @"\[[^\]]*\]");
        foreach (string ip in ipv7)
        {
            List<string> aba = CheckAba(ip);
            foreach (var val in aba)
            {
                string bab = val[1].ToString() + val[0].ToString() + val[1].ToString();
                foreach (Match m in Regex.Matches(input, @"\[(\w*)\]"))
                {
                    if (m.Value.Contains(bab))
                        return true;
                }

            }
        }
        return false;
    }
    private bool ContainsAbba(string text)
    { 
        for (int i = 0; i < text.Length - 3; i++)
        {
            if(text[i]==text[i+3] && text[i+1]==text[i+2] && text[i]!=text[i+1]) return true;
        }

        return false;
    }

    private List<string> CheckAba(string text)
    {
        List<string> aba = new List<string>();
        for (int i = 0; i < text.Length - 2; i++)
        {
            if (text[i] == text[i + 2] && text[i] != text[i + 1])
            {
                aba.Add(text.Substring(i, 3));
            }
        }

        return aba;
    }
}