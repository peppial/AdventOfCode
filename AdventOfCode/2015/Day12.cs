using System.Net.NetworkInformation;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015;

public class Day12(string[] lines):IDay
{
    private int sum = 0;
    public long GetTotalPartA()
    {
        int sum = 0;
        foreach (Match match in Regex.Matches(lines[0], @"-?\d+"))
        {
            sum += int.Parse(match.Value);
        }

        return sum;
    }

    public long GetTotalPartB()
    {
      
        JsonNode? value = JsonValue.Parse(lines[0]);

        Iterate(value);

        return sum;
    }

    private void Iterate(JsonNode node)
    {
        JsonValueKind kind = node.GetValueKind();
        if (kind == JsonValueKind.Array)
        {
            foreach (JsonNode j in node.AsArray()) Iterate(j);
        }
        else if (kind == JsonValueKind.Number) sum += int.Parse(node.AsValue().ToJsonString());
        else if (kind == JsonValueKind.Object)
        {
            foreach (KeyValuePair<string, JsonNode?> pair in node.AsObject())
            {
                if (pair.Value.ToString() == "red") return;
            }

            foreach (KeyValuePair<string, JsonNode?> pair in node.AsObject())
            {
                JsonValueKind kind0 = pair.Value.GetValueKind();
                if (kind0 == JsonValueKind.Array)
                {
                    foreach (JsonNode j in pair.Value.AsArray()) Iterate(j);
                }
                else if (kind0 == JsonValueKind.Number) sum += int.Parse(pair.Value.AsValue().ToJsonString());
                else if (kind0 == JsonValueKind.Object)
                {
                    Iterate(pair.Value.AsObject());
                }
            }
        }
        
    }

    
}