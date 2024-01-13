using Advent2023;
using AdventOfCode.Extensions;
namespace AdventOfCode._2022;

public class Day2(string[] lines):IDay
{
    public enum Round
    {
        Rock=1,
        Paper=2,
        Scissors=3
    }
    public enum GameEnd
    {
        Lose=1,
        Draw=2,
        Win=3
    }

    public List<(Round player1, Round player2)> games = [];
    public long GetTotalPartA()
    {
        InitA();

        return games.Aggregate(0,(newvalue, game)=>newvalue+RoundOutcome(game.player1, game.player2)+(int)game.player2);
    }

    private void InitA()
    {
        foreach (string line in lines)
        {
            var parts = line.SplitDefault(" ");
            Round player1 = parts[0] switch
            {
                "A" => Round.Rock,
                "B" => Round.Paper,
                "C" => Round.Scissors
            };
            
            Round player2 = parts[1] switch
            {
                "X" => Round.Rock,
                "Y" => Round.Paper,
                "Z" => Round.Scissors
            };
            games.Add((player1,player2));
        }
    }

    public long GetTotalPartB()
    {
         List<(Round player1, GameEnd player2)> gamesB = [];
         
         foreach (string line in lines)
         {
             var parts = line.SplitDefault(" ");
             Round player1 = parts[0] switch
             {
                 "A" => Round.Rock,
                 "B" => Round.Paper,
                 "C" => Round.Scissors
             };
            
             GameEnd gameEnd = parts[1] switch
             {
                 "X" => GameEnd.Lose,
                 "Y" => GameEnd.Draw,
                 "Z" => GameEnd.Win
             };
             gamesB.Add((player1,gameEnd));
         }
         return gamesB.Aggregate(0,(newvalue, game)=>newvalue+RoundOutcomeB(game.player1, game.player2));

         
    }

    private int RoundOutcome(Round player1, Round player2)
    {
        if (player1 == player2) return 3;
        if (player1 == Round.Paper && player2 == Round.Scissors) return 6;
        if (player1 == Round.Paper && player2 == Round.Rock) return 0;
        
        if (player1 == Round.Rock && player2 == Round.Paper) return 6;
        if (player1 == Round.Rock && player2 == Round.Scissors) return 0;
        
        if (player1 == Round.Scissors && player2 == Round.Rock) return 6;
        if (player1 == Round.Scissors && player2 == Round.Paper) return 0;

        throw new Exception("error");
    }
    
    private int RoundOutcomeB(Round player1, GameEnd gameEnd)
    {
        var score= gameEnd switch
        {
            GameEnd.Win => 6,
            GameEnd.Draw => 3,
            GameEnd.Lose => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(gameEnd), gameEnd, null)
        };
        if (player1 == Round.Rock && gameEnd == GameEnd.Win) score += (int)Round.Paper;
        if (player1 == Round.Paper && gameEnd == GameEnd.Win) score += (int)Round.Scissors;
        if (player1 == Round.Scissors && gameEnd == GameEnd.Win) score += (int)Round.Rock;

        if (player1 == Round.Rock && gameEnd == GameEnd.Lose) score += (int)Round.Scissors;
        if (player1 == Round.Paper && gameEnd == GameEnd.Lose) score += (int)Round.Rock;
        if (player1 == Round.Scissors && gameEnd == GameEnd.Lose) score += (int)Round.Paper;
        
        
        if ( gameEnd == GameEnd.Draw) score += (int)player1;
        return score;

    }
}