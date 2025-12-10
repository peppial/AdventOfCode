using System.Text.RegularExpressions;
using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day10(string[] lines) : IDay
{
    public long GetTotalPartA() => Calculate(false);

    public long GetTotalPartB() => Calculate(true);


    private long Calculate(bool partB)
    {
        int ret = 0;
        foreach (var line in lines)
        {
            var schema = Regex.Matches(line, @"\[([.#]+)\]");
            var buttons = Regex.Matches(line, @"\(([^)]*)\)");

            string target = schema[0].Groups[1].Value.Replace('#', '1').Replace('.', '0');
            int len = target.Length;
            List<int> buttonValues = [];
            foreach (Match button in buttons)
            {
                var nums = button.Groups[1].Value.GetNumbers();
                string str = "".PadLeft(len, '0');

                for (int i = 0; i < nums.Length; i++)
                {
                    str = str[..nums[i]] + '1' + str.Substring(nums[i] + 1);
                }
                buttonValues.Add(Convert.ToInt32(str, 2));
            }
            int targetValue = Convert.ToInt32(target, 2);
            int minCount = int.MaxValue;
            Iterate(targetValue, buttonValues, 0);
            ret += minCount + 1;

            void Iterate(int target, List<int> buttons, int count)
            {
                if (count >= minCount) return;

                for (int idx = 0; idx < buttons.Count; idx++)
                {
                    var first = buttons[idx];
                    if (first == target)
                    {
                        minCount = count;
                        return;
                    }
                    var rest = buttons.GetRange(idx + 1, buttons.Count - (idx + 1));
                    Iterate(target ^ first, rest, count + 1);
                }
            }
        }
        return ret;
    }

}
