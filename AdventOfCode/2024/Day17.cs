using System.Numerics;
using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day17: IDay
{
    private long registerA;
    private long registerB;
    private long registerC;
    private long[] program;
    private List<long> output = [];
    private Dictionary<long, Action<long>> operations;
    
    public Day17(string[] lines)
    {
        registerA = lines[0].GetNumbers()[0];
        program = lines[4].GetNumbersLong();
        
        operations = new Dictionary<long, Action<long>>
        {
            { 0, (op)=>registerA /= (int)Math.Pow(2, GetValue(op)) },
            { 1, (op)=>registerB ^= op },
            { 2, (op)=>registerB = GetValue(op) % 8 },
            { 3, (_) => { }  },
            { 4, (op)=>registerB ^= registerC} ,
            { 5, (op)=>output.Add(GetValue(op) % 8) } ,
            { 6, (op)=>registerB = registerA / (int)Math.Pow(2, GetValue(op))  },
            { 7, (op)=>registerC = registerA / (int)Math.Pow(2, GetValue(op))  }
        }; 
        long GetValue(long operand)
        {
            return operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => registerA,
                5 => registerB,
                6 => registerC,
                7 => throw new ArgumentException("Invalid operand"),
            };
        }
    }

    public long GetTotalPartA()
    {
        Console.WriteLine(GetOutput(registerA).Item1);
        return 0;
    }

    private (string, List<long> list) GetOutput(long regA)
    {
        long i = 0;
        registerA = regA;
        while (i >= 0 && i < program.Length - 1)
        {
            long opCode = program[i];
            long op = program[i + 1];

            DoOperation(opCode, op);
            i = i + 2;
            if (opCode == 3 && registerA > 0) i = op;
        }

        return (string.Join(',', output), output);

        
    }
    void DoOperation(long code, long operand)
    {
        operations[code](operand);
    }

    public long GetTotalPartB()
    {
        long startA = (long)Math.Pow(8, 15);
        
        while (true)
        {
            int insPointer = 0;
            output = [];
            
            registerA = startA;
            long i = 0;
            while (i >= 0 && i < program.Length - 1)
            {
                long opCode = program[i];
                long op = program[i + 1];

                DoOperation(opCode, op);
                i = i + 2;
                if (opCode == 3 && registerA > 0) i = op;
            }

            bool foundDifference = false;
            for (int j = -1; j >= -16; j--)
            {
                if (output[output.Count + j] != program[program.Length + j])
                {
                    startA += (long)Math.Pow(8, 16 + j);
                    Console.WriteLine($"Diff: {(long)Math.Pow(8, 16 + j)}");
                    Console.WriteLine($"Answer: {startA}");
                    foundDifference = true;
                    break;
                }
            }

            if (output.Count > program.Length)
                break;

            if (!foundDifference && output.SequenceEqual(program.ToList()))
            {
                Console.WriteLine(startA);
                break;
            }

        }

        return 0;
    }
}