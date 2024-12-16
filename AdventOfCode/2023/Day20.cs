using static AdventOfCode.Utils.MathUtils;

using AdventOfCode.Extensions;
namespace Advent2023;
public class Day20(string[] lines):IDay 
{
    public long GetTotalPartA()
    {
        var modules = GetModules();

        var high = 0;
        var low = 0;

        var queue = new Queue<(string Label, bool IsHigh, string Source)>();

        for (var i = 0; i < 1000; i++)
        {
            queue.Enqueue(("broadcaster", false, "button"));
            while (queue.Count > 0)
            {
                var (label, isHigh, source) = queue.Dequeue();
                if (isHigh)
                {
                    high++;
                }
                else
                {
                    low++;
                }

                if (!modules.TryGetValue(label, out var module)) continue;
                if (module is FlipModule flipModule)
                {
                    if (isHigh) continue;

                    flipModule.Flip();
                    isHigh = flipModule.IsOn;
                }
                else if (module is ConjunctionModule conjunctionModule)
                {
                    conjunctionModule.SetInput(source, isHigh);
                    isHigh = !conjunctionModule.IsHigh;
                }

                foreach (var destination in module.Destinations)
                {
                    queue.Enqueue((destination, isHigh, label));
                }
            }

            if (modules.All(x => x.Value.IsDefault()))
            {
                var cycle = i + 1;
                var totalCycles = 1000 / cycle;

                high *= totalCycles;
                low *= totalCycles;
                i = totalCycles * cycle - 1;
            }
        }

        return high * low;
    }
 
    public long GetTotalPartB()
    {
        var modules = GetModules();
        var queue = new Queue<(string Label, bool IsHigh, string Source)>();

        var rxSource = modules.FirstOrDefault(x => x.Value.Destinations.Contains("rx")).Key;
        var sources = modules.Where(x => x.Value.Destinations.Contains(rxSource)).Select(x => x.Key).ToArray();
        var sourceIntervals = sources.Select(x => new List<long>()).ToArray();
        for (var i = 0;; i++)
        {
            queue.Enqueue(("broadcaster", false, "button"));
            while (queue.Count > 0)
            {
                var (label, isHigh, source) = queue.Dequeue();

                if (!modules.TryGetValue(label, out var module)) continue;
                if (module is FlipModule flipModule)
                {
                    if (isHigh) continue;

                    flipModule.Flip();
                    isHigh = flipModule.IsOn;
                }
                else if (module is ConjunctionModule conjunctionModule)
                {
                    conjunctionModule.SetInput(source, isHigh);
                    isHigh = !conjunctionModule.IsHigh;
                }

                foreach (var destination in module.Destinations)
                {
                    var sourceIndex = Array.IndexOf(sources, label);
                    if (sourceIndex >= 0 && isHigh)
                    {
                        sourceIntervals[sourceIndex].Add(i + 1);
                    }

                    queue.Enqueue((destination, isHigh, label));
                }
            }

            if (sourceIntervals.All(x => x.Count >= 2)) break;
        }

        var intervals = sourceIntervals.Select(x => x[1] - x[0]).ToArray();
        return LCM(intervals);
    }
    
    private Dictionary<string, IModule> GetModules()
    {
        var modules = new Dictionary<string, IModule>();
        foreach (var line in lines)
        {
            var parts = line.SplitDefault(" -> ");
            var connections = parts[1].SplitDefault(",");
            
            var label = line[..line.IndexOf(' ')];
            IModule module = label[0] switch
            {
                '%' => new FlipModule(label[1..], connections),
                '&' => new ConjunctionModule(label[1..], connections),
                _ => new Module(label, connections)
            };
            modules.Add(module.Label, module);
        }

        foreach (var module in modules)
        {
            foreach (var destination in module.Value.Destinations)
            {
                if (!modules.TryGetValue(destination, out var destModule)) continue;
                if (destModule is ConjunctionModule conjunctionModule)
                {
                    conjunctionModule.AddSource(module.Key);
                }
            }
        }

        return modules;
    }


    private interface IModule
    {
        public string Label { get; }
        public string[] Destinations { get; }

        public bool IsDefault();
    }

    private record Module(string Label, string[] Destinations) : IModule
    {
        public bool IsDefault() => true;
    }
    private record FlipModule(string Label, string[] Destinations) : IModule
    {
        public bool IsOn { get; private set; }
        public void Flip() => IsOn = !IsOn;

        public bool IsDefault() => !IsOn;
    }
    private record ConjunctionModule(string Label, string[] Destinations) : IModule
    {
        private readonly Dictionary<string, bool> inputs = new();
        public void AddSource(string label)
        {
            inputs.Add(label, false);
        }

        public void SetInput(string source, bool isHigh) => inputs[source] = isHigh;
        public bool IsHigh => inputs.Values.All(x => x);
        public bool IsDefault() => inputs.Values.All(x => !x);
    }
}