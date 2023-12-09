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
            if (instructions[instructionCount] == 'L')
                nextInstruction = step.Left;
            else
                nextInstruction = step.Right;
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
            string label = parts[0];
            string left = parts[1];
            string right = parts[2];
            dict.Add(label, new Instruction(left,right));
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
                if (instructions[instructionCount] == 'L')
                    nextInstruction = dict[nextInstruction].Left;
                else
                    nextInstruction = dict[nextInstruction].Right;
                stepCount++;
                instructionCount++;
                if (instructionCount == instructions.Length) instructionCount = 0;
            }
            lcm.Add(stepCount);

        }

        return MathUtils.LCM(lcm.ToArray());
    }
}