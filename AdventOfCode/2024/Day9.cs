namespace AdventOfCode._2024;

public class Day9(string[] lines): IDay
{
    string line = lines[0];
    public long GetTotalPartA()
    {
        var (chars, blankBlocks, fullBlocks) = GetChars(line);

        var empties = chars.Select((c, i) => c==-1 ? i : -1).Where(i => i != -1).ToList();
        var fulls = chars.Select((c, i) => c!=-1 ? i : -1).Where(i => i != -1).ToList();
        
        
        int lastIndex = fulls.Count - 1;
        
        for (int i = 0; i < empties.Count; i++)
        {
            chars[empties[i]]=chars[fulls[lastIndex]];
            chars[fulls[lastIndex]] = -1;
            if (chars.IndexOf(-1) == fulls[lastIndex]) break;
            lastIndex--;
        }

        return CalculateSum(chars);
    }

    

    public long GetTotalPartB()
    {
        var (chars, emptyBlocks, fullBlocks) = GetChars(line);

        
        var fIndex = fullBlocks.Count - 1;
        var emIndex = 0;
        long sum = 0;
        while (fIndex > 0)
        {
            int tempLen = 0;
            for (var i = 0; i < chars.Count; i++)
            {
                int count = 0;
                if (chars[i] == -1)
                {
                    count = 1;
                    while ((i + count) < chars.Count && chars[i + count] == -1)
                    {
                        count++;
                    }

                }

                if (fullBlocks[fIndex].pos < i) break;
                if (count >= fullBlocks[fIndex].length)
                {
                    for (int j = 0; j < fullBlocks[fIndex].length; j++)
                    {
                        chars[i + j] = chars[fullBlocks[fIndex].pos + j];
                        chars[fullBlocks[fIndex].pos + j] = -1;
                    }

                    break;
                }
            }

            fIndex--;
        }

        return CalculateSum(chars);
        
    }
    private static long CalculateSum(List<int> chars)
    {
        long sum = 0;
        for (int i = 0; i < chars.Count; i++)
        {
            if(chars[i]==-1) continue;
            sum += chars[i] * i;
        }

        return sum;
    }
    private static (List<int>, List<(int ch,int length,int pos)>, List<(int ch,int length,int pos)>) GetChars(string line)
    {
        List<int> chars = [];
        List<(int, int,int)> blanks = [];
        List<(int, int,int)> fulls = [];
        
        bool space = false;
        int blockCount = 0;
        int pos = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (space)
            {
                chars.AddRange(Enumerable.Range(1,line[i]-'0').Select(n =>-1));
                blanks.Add((i,line[i]-'0', pos));
            }
            else
            {
                chars.AddRange(Enumerable.Range(1,line[i]-'0').Select(n => blockCount));
                fulls.Add((i,line[i]-'0',pos));
                blockCount++;
            }

            pos += line[i] - '0';
            space = !space;
        }

        return (chars, blanks, fulls);
    }
}