namespace Advent2023;

readonly record struct Instruction(string Left, string Right);
public class Day8(string[] lines)
{
    
    private static Instruction Parse(string line)
    {
        var parts = line.Split(" =,()");
        return new Instruction(parts[1], parts[2]);
    }

    public long Test()
    {
        var a = lines[2..].ParseInputLines(Parse);
        return a.ToList().Count();
    }
    public long GetTotalPartA()
    {
        string nextInstruction = "AAA";
        string instructions = lines[0];
        Dictionary<string, Instruction> dict = CreateInstructions();

        int instructionCount = 0;
        int stepCount = 0;
        while (nextInstruction != "ZZZ")
        {
            var step = dict[nextInstruction];
            nextInstruction = instructions[instructionCount] switch
            {
                'L' => dict[nextInstruction].Left,
                'R' => dict[nextInstruction].Right
            };
            
            instructionCount++;
            stepCount++;
            if (instructionCount == instructions.Length) instructionCount = 0;
        }
       
        return stepCount;
    }

    private Dictionary<string, Instruction> CreateInstructions()
    {
        Dictionary<string, Instruction> dict = new();
        for (int i = 2; i < lines.Length; i++)
        {
            string[] parts = lines[i].SplitDefault(" ()=,");
            dict.Add(parts[0], new Instruction(parts[1],parts[2]));
        }

        return dict;
    }
 
    public long GetTotalPartB()
    {
        string instructions = lines[0];
       
        Dictionary<string, Instruction> dict = CreateInstructions();

        IEnumerable<string> nextInstructions = dict.Where(item=>item.Key.EndsWith("A"))
            .Select(item=>item.Key);
        
        List<long> lcm = new();
        foreach (var startInstruction in nextInstructions)
        {
            int instructionCount = 0;
            int stepCount = 0;
            var nextInstruction = startInstruction;
            while (!nextInstruction.EndsWith("Z"))
            {
                nextInstruction = instructions[instructionCount] switch
                {
                    'L' => dict[nextInstruction].Left,
                    'R' => dict[nextInstruction].Right
                };
   
                stepCount++;
                instructionCount++;
                if (instructionCount == instructions.Length) instructionCount = 0;
            }
            lcm.Add(stepCount);

        }

        return MathUtils.LCM(lcm.ToArray());
    }
}