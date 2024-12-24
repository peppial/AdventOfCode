namespace AdventOfCode._2024;

public class Day24 : IDay
{
    Dictionary<string, int> gates = [];

    enum Operation
    {
        AND,
        OR,
        XOR
    }

    Dictionary<string, (string left, string right, Operation operation)> operations = [];

    public Day24(string[] lines)
    {
        bool isGates = true;
        Queue<string> notSolved = [];
        foreach (var line in lines)
        {

            if (line.Trim() == "")
            {
                isGates = false;
                continue;
            }

            if (isGates)
            {
                gates.Add(line[0..3], int.Parse(line[5..]));
                continue;
            }

            notSolved.Enqueue(line);
        }

        while (notSolved.Count > 0)
        {
            var line = notSolved.Dequeue();
            Operation operation = Operation.AND;
            if (line.Contains("XOR")) operation = Operation.XOR;
            else if (line.Contains("OR")) operation = Operation.OR;

            var parts = line.Split(' ');
            var o = (parts[0], parts[2], operation);

            if (!gates.ContainsKey(parts[0]) || !gates.ContainsKey(parts[2]))
            {
                notSolved.Enqueue(line);
                continue;
            }

            operations.Add(parts[4], o);
            var val = operation switch
            {
                Operation.AND => gates[parts[0]] & gates[parts[2]],
                Operation.OR => gates[parts[0]] | gates[parts[2]],
                Operation.XOR => gates[parts[0]] ^ gates[parts[2]]
            };
            gates.Add(parts[4], val);
        }
    }

    public long GetTotalPartA()
    {
        var bin = string.Join("", gates.Where(g => g.Key.StartsWith('z'))
            .OrderByDescending(x => x.Key).Select(x => x.Value));
        Console.WriteLine(bin);
        return Convert.ToInt64(bin, 2);
    }

    public long GetTotalPartB()
    {
        var wrong = operations.Where(g => g.Key[0] == 'z' && g.Key != "z45" && g.Value.operation != Operation.XOR)
            .Select(g => g.Key).ToHashSet();

        wrong = wrong.Union(operations.Where(g => g.Key[0] != 'z'
                                                  && g.Value.left[0] is not ('x' or 'y')
                                                  && g.Value.right[0] is not ('x' or 'y')
                                                  && g.Value.operation == Operation.XOR)
            .Select(g => g.Key)).ToHashSet();

        foreach (var left in operations
                     .Where(g => g.Value.operation == Operation.AND && g.Value.left != "x00" && g.Value.right != "x00"))
        {
            foreach (var right in operations)
            {
                if ((left.Key == right.Value.left || left.Key == right.Value.right) &&
                    right.Value.operation != Operation.OR)
                {
                    wrong.Add(left.Key);
                }
            }
        }

        foreach (var left in operations.Where(g => g.Value.operation == Operation.XOR))
        {
            foreach (var right in operations)
            {
                if ((left.Key == right.Value.left || left.Key == right.Value.right) &&
                    right.Value.operation == Operation.OR)
                {
                    wrong.Add(left.Key);
                }
            }
        }

        Console.WriteLine(string.Join(',', wrong.Order()));

        return 0;
    }
}