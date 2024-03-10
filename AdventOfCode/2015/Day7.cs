namespace AdventOfCode._2015;

public class Day7(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        return GetSignals();
    }
    public long GetTotalPartB()
    {
        return GetSignals(true);
    }
    private long GetSignals(bool overrideA = false)
    {
        Dictionary<string, ushort> values = new();
        Dictionary<string, string[]> instructions = new();

        foreach (string line in lines)
        {
            var parts = line.Split("->", StringSplitOptions.TrimEntries);
            var firstParts = parts[0].Split(' ', StringSplitOptions.TrimEntries);
            if (firstParts.Length == 1 &&  ushort.TryParse(firstParts[0], out _))
            {
                values.Add(parts[1],ushort.Parse(firstParts[0]));
            }
            else instructions.Add(parts[1],firstParts);

        }

        if (overrideA)
        {

            var value = (ushort) GetSignals();
            values["b"] = value;
        }
        while (instructions.Count > 0)
        {
            Dictionary<string, string[]> tempInstructions = new();

            foreach (string key in instructions.Keys)
            {
                var instr = instructions[key];
                if (instr.Length == 1  && values.ContainsKey(instr[0]))
                {
                    values.Add(key,values[instr[0]]);
                    continue;
                }

                if (instr.Length == 2 && values.ContainsKey(instr[1]) && instr[0] =="NOT")
                {
                    values.Add(key,(ushort)(~values[instr[1]]));
                    continue;
                }
                if (instr.Length == 3 && values.ContainsKey(instr[0]) && instr[1] =="LSHIFT")
                {
                    values.Add(key,(ushort)(values[instr[0]]<<short.Parse(instr[2])));
                    continue;
                }
                if (instr.Length == 3 && values.ContainsKey(instr[0]) && instr[1] =="RSHIFT")
                {
                    values.Add(key,(ushort)(values[instr[0]]>>short.Parse(instr[2])));
                    continue;
                }
                if (instr.Length == 3 && values.ContainsKey(instr[0]) && values.ContainsKey(instr[2]) && instr[1] =="AND")
                {
                    values.Add(key,(ushort)(values[instr[0]] & values[instr[2]]));
                    continue;
                }
                if (instr.Length == 3 && values.ContainsKey(instr[0]) && values.ContainsKey(instr[2]) && instr[1] =="OR")
                {
                    values.Add(key,(ushort)(values[instr[0]] | values[instr[2]]));
                    continue;
                }
                if (instr.Length == 3 && ushort.TryParse(instr[0], out _) && values.ContainsKey(instr[2]) && instr[1] =="AND")
                {
                    values.Add(key,(ushort)(ushort.Parse(instr[0]) & values[instr[2]]));
                    continue;
                }
                if (instr.Length == 3 && ushort.TryParse(instr[0], out _) && values.ContainsKey(instr[2]) && instr[1] =="OR")
                {
                    values.Add(key,(ushort)(ushort.Parse(instr[0]) | values[instr[2]]));
                    continue;
                }
                
                if (instr.Length == 3 && ushort.TryParse(instr[2], out _) && values.ContainsKey(instr[0]) && instr[1] =="AND")
                {
                    values.Add(key,(ushort)(ushort.Parse(instr[2]) & values[instr[0]]));
                    continue;
                }
                if (instr.Length == 3 && ushort.TryParse(instr[2], out _) && values.ContainsKey(instr[0]) && instr[1] =="OR")
                {
                    values.Add(key,(ushort)(ushort.Parse(instr[2]) | values[instr[0]]));
                    continue;
                }
                
                tempInstructions.Add(key,instr);
            }

            instructions = tempInstructions;
        }
        return values["a"];
    }

  
}