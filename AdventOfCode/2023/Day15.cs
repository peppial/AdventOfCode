namespace Advent2023;

public class Day15(string lines):IDay 
{
    private string[] steps = lines.Split(',');
    public long GetTotalPartA()
    {   
        int sum = 0;
        
        foreach (var step in steps)
        {
            int result = GetHash(step);
            Console.WriteLine($"{step} hash: {result}.");
            sum += result;
        }

        return sum;
    }
    

    public long GetTotalPartB()
    {
        Dictionary<string, int> lengths = new ();
        List<List<string>> boxes = new (256);

        foreach(int i in 255) boxes.Add(new ());
        
        int result = 0;
        string[] steps = lines.Split(',');

        foreach (string step in steps)
        {
            if (step.Contains("="))
            {
                string[] parts = step.Split('=');
                string label = parts[0].Trim();
                int length = int.Parse(parts[1].Trim());
                int hash = GetHash(label);

                if (!boxes[hash].Contains(label))
                {
                    boxes[hash].Add(label);
                }

                lengths[label] = length;
            }
            else if (step.Contains("-"))
            {
                string label = step.Split('-')[0].Trim();
                int hash = GetHash(label);

                if (boxes[hash].Contains(label))
                {
                    boxes[hash].Remove(label);
                }
            }
        }

        for (int bi = 0; bi < boxes.Count; bi++)
        {
            for (int li = 0; li < boxes[bi].Count; li++)
            {
                string label = boxes[bi][li];
                int power = (bi + 1) * (li + 1) * lengths[label];
                Console.WriteLine($"{label} -> {power}");
                result += power;
            }
        }

        return result;
    }
    private int GetHash(string input)
    {
        return input.Select(character => (int)character)
            .Aggregate(0, (acc, asciiCode) => (acc + asciiCode) * 17 % 256);
    }
    
}