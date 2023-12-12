using System.Text.RegularExpressions;

namespace Advent2023;

public class Day12(string[] lines) : IDay
{
	
	public long GetTotalPartA()
	{

		List<string[]> input = lines.Select(l => l.Split(' ')).ToList();
		int result = 0;

		foreach (string[] line in input)
		{
			List<int> commands = line[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
			result += GenerateCombinationsFor(line[0]).Count(comb => MatchesCommand(comb, commands));
		}

		return result;
	}

	private bool MatchesCommand(string input, List<int> commands)
	{
		string[] groupsOfHashtags = input.Split(".", StringSplitOptions.RemoveEmptyEntries);
		if (groupsOfHashtags.Length != commands.Count) return false;
		for (int i = 0; i < groupsOfHashtags.Length; i++)
		{
			if (groupsOfHashtags[i].Length != commands[i]) return false;
		}

		return true;
	}

	private List<string> GenerateCombinationsFor(string input)
	{
		List<string> result = [];
		result.Add(input);

		while (result[0].Contains('?'))
		{
			List<string> combinations = [];
			for (int i = 0; i < result.Count; i++)
			{
				int index = result[i].IndexOf('?');
				combinations.Add(string.Concat(result[i].AsSpan(0, index), ".", result[i].AsSpan(index + 1)));
				combinations.Add(string.Concat(result[i].AsSpan(0, index), "#", result[i].AsSpan(index + 1)));
			}

			result = combinations;
		}

		return result;
	}

	private static Dictionary<string, long> scorePerSubInput { get; } = new();

	public long GetTotalPartB()
	{
		var inputLines = ParseInput(lines);
		var unfoldedInputLines = inputLines.Select(t => UnfoldInputLine(t, 5));
		var sum = unfoldedInputLines.Select(t => CountPossibilities(t.springStates, t.commands))
			.Aggregate((long)0, (current, count) => current + count);
		return sum;
		
	}
	
	private static (string springStates, int[] commands) UnfoldInputLine((string springStates, int[] commands) inputLine, int unfoldingLength)
	{
		var sprintStates = string.Join("?", Enumerable.Repeat(inputLine.springStates, unfoldingLength));
		return (sprintStates, Enumerable.Repeat(inputLine.commands, unfoldingLength).SelectMany( i => i ).ToArray());
	}

	private static IEnumerable<(string springStates, int[] commands)> ParseInput(IEnumerable<string> inputLines) =>
		inputLines.Where(t => !string.IsNullOrEmpty(t))
			.Select(t => (t.Split(' ')[0], t.Split(' ')[1].Split(',')
				.Select(int.Parse).ToArray())).ToArray();

	private static long CountPossibilities(string springStates, IReadOnlyList<int> commands) =>
		CountPossibilities(springStates, 0, commands, 0, commands.Sum(t => t + 1) - 1);

	private static long CountPossibilities(string springStates, int springStateStartIndex,
		IReadOnlyList<int> commands, int commandSizeStartIndex, int minSpringStateLength)
	{
		var subInput = springStates.Substring(springStateStartIndex) + " " +
		               string.Join(",", commands.Skip(commandSizeStartIndex));
		if (scorePerSubInput.ContainsKey(subInput)) return scorePerSubInput[subInput];
		long result = 0;
		if (springStates.Length - springStateStartIndex < minSpringStateLength) result = 0;
		else if (commandSizeStartIndex >= commands.Count)
			result = springStates.LastIndexOf('#') >= springStateStartIndex ? 0 : 1;
		else
		{
			var currentChunkSize = commands[commandSizeStartIndex];
			if (springStateStartIndex >= springStates.Length) result = 0;
			else if (springStates.Length - springStateStartIndex < currentChunkSize) result = 0;
			else if (springStates[springStateStartIndex] == '.')
				result = CountPossibilities(springStates, springStateStartIndex + 1, commands,
					commandSizeStartIndex, minSpringStateLength);
			else if (springStates[springStateStartIndex] == '#')
			{
				result = CountPossibilitiesConsideringFirstAsDamaged(springStates, springStateStartIndex,
					commands, commandSizeStartIndex, minSpringStateLength);
			}
			else if (springStates[springStateStartIndex] == '?')
			{
				result = CountPossibilities(springStates, springStateStartIndex + 1, commands,
					         commandSizeStartIndex, minSpringStateLength)
				         + CountPossibilitiesConsideringFirstAsDamaged(springStates, springStateStartIndex,
					         commands, commandSizeStartIndex, minSpringStateLength);
			}
		}

		scorePerSubInput[subInput] = result;
		return result;
	}

	private static long CountPossibilitiesConsideringFirstAsDamaged(string springStates, int springStateStartIndex,
		IReadOnlyList<int> commands, int commandSizeStartIndex,
		int minSpringStateLength)
	{
		var currentChunkSize = commands[commandSizeStartIndex];
		for (var i = 1; i < currentChunkSize; ++i)
		{
			if (springStates[springStateStartIndex + i] == '.') return 0;
		}

		if (springStates.Length - springStateStartIndex == currentChunkSize)
			return commandSizeStartIndex == commands.Count - 1 ? 1 : 0;
		if (springStates[springStateStartIndex + currentChunkSize] == '#') return 0;
		return CountPossibilities(springStates, springStateStartIndex + currentChunkSize + 1, commands,
			commandSizeStartIndex + 1, minSpringStateLength - currentChunkSize - 1);
	}
}