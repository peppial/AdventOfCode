using System.ComponentModel.DataAnnotations;

namespace Advent2;

record Instruction(string left, string right);
public class Day8(string[] lines)
{
    private Dictionary<string, Instruction> dict = new();
    public long GetTotalPartA()
    {
        string nextInstruction = "AAA";
        string instructions = lines[0];
        for (int i = 2; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(new char[]{' ','(',')','=',','}, StringSplitOptions.RemoveEmptyEntries);
            string label = parts[0];
            string left = parts[1];
            string right = parts[2];
            dict.Add(label, new Instruction(left,right));
        }

        int instructionCount = 0;
        int stepCount = 0;
        while (nextInstruction != "ZZZ")
        {
            var step = dict[nextInstruction];
            if (instructions[instructionCount] == 'L')
                nextInstruction = step.left;
            else
                nextInstruction = step.right;
            instructionCount++;
            stepCount++;
            if (instructionCount == instructions.Length) instructionCount = 0;
        }
       
        return stepCount;
    }

    public long GetTotal()
    {
        string instructions = lines[0];
        List<string> nextInstructions = new();
        for (int i = 2; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(new char[] { ' ', '(', ')', '=', ',' },
                StringSplitOptions.RemoveEmptyEntries);
            string label = parts[0];
            string left = parts[1];
            string right = parts[2];
            dict.Add(label, new Instruction(left, right));
            if (label.EndsWith("A")) nextInstructions.Add(label);
        }

       
        List<long> lcm = new();
        foreach (var startInstruction in nextInstructions)
        {
            int instructionCount = 0;
            int stepCount = 0;
            var nextInstruction = startInstruction;
            while (!nextInstruction.EndsWith("Z"))
            {
                if (instructions[instructionCount] == 'L')
                    nextInstruction = dict[nextInstruction].left;
                else
                    nextInstruction = dict[nextInstruction].right;
                stepCount++;
                instructionCount++;
                if (instructionCount == instructions.Length) instructionCount = 0;
            }
            lcm.Add(stepCount);

        }

        return Utils.LCM(lcm.ToArray());
    }
}