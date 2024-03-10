namespace AdventOfCode._2022;

public class Day8(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int total = lines[0].Length * 2;
        for(int i=1;i<lines.Length-1;i++)
        {
            string line = lines[i];
            total += 2;
            for (int j = 1; j < line.Length-1; j++)
            {
                if (IsVisible(i, j))
                {
                    total++;
                    int target = lines[i][j] - '0';

                    Console.WriteLine("target "  + target+ "-- "+ i+" , "+j);
                }
            }
        }

        return total;
    }

    private bool IsVisible(int row, int col)
    {
        int target = lines[row][col] - '0';
        bool isVisible = true;
        
        for (int i = 0; i < lines[row].Length; i++)
        {
            if(i==col)
            {
                if (isVisible) return true;
                isVisible = true;
                continue;
            }
            int anotherTree = lines[row][i] - '0';
            if (anotherTree >= target) 
            {
                isVisible = false;
            }
        }
        if (isVisible) return true;
        isVisible = true;
        for (int j = 0; j < lines.Length; j++)
        {
            if(j==row) 
            {
                if (isVisible) return true;
                isVisible = true;
                continue;
            }
            int anotherTree = lines[j][col] - '0';
            if (anotherTree >= target) 
            {
                isVisible = false;
            }
        }
        return isVisible;

    }
    public long GetTotalPartB()
    {
        int maxScore = 0;
        for(int i=1;i<lines.Length-1;i++)
        {
            string line = lines[i];
            for (int j = 1; j < line.Length-1; j++)
            {
                int maxC = GetScore(i, j);
                
                int target = lines[i][j] - '0';

                
                Console.WriteLine("target "  + target+ "-- "+ i+" , "+j);
                Console.WriteLine(maxC);
                if (maxC > maxScore) maxScore = maxC;
            }
        }

        return maxScore;
        
    }

    private int GetScore(int row, int col)
    {
        int target = lines[row][col] - '0';

        int scoreLeft = 1;
        bool blocked = false;
        for (int i = col - 1; i >= 0; i--)
        {
            if ((lines[row][i]- '0') < target) scoreLeft++;
            else 
            {
                blocked = true;
                break;
            }
        }

        if (!blocked) scoreLeft --;


        int scoreRight = 1;blocked = false;
        for (int i = col+1; i < lines[0].Length; i++)
        {
            if ((lines[row][i]- '0') < target) scoreRight++;
            else 
            {
                blocked = true;
                break;
            }
            
        }

        if (!blocked) scoreRight --;

        int scoreTop = 1;blocked = false;
        for (int i = row-1; i >=0; i--)
        {
            if ((lines[i][col]- '0') < target) scoreTop++;
            else 
            {
                blocked = true;
                break;
            }
            
        }
        if (!blocked) scoreTop --;

        int scoreDown = 1;blocked = false;
        for (int i = row+1; i <lines.Length; i++)
        {
            if ((lines[i][col]- '0') < target) scoreDown++;
            else 
            {
                blocked = true;
                break;
            }        
        }

        if (!blocked) scoreDown --;

        return scoreLeft*scoreRight*scoreTop*scoreDown;

    }
}