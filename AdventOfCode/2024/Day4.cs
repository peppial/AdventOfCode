namespace AdventOfCode._2024;

public class Day4(string[] lines): IDay
{
    int rows= lines.Length;
    int columns = lines[0].Length;
 
    string XMAS = "XMAS";
    
    public long GetTotalPartA()
    {   
        int count = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                    if (j + 3 < columns  && $"{lines[i][j]}{lines[i][j+1]}{lines[i][j+2]}{lines[i][j+3]}" == "XMAS") count++;
                    if (j - 3 >= 0 && $"{lines[i][j]}{lines[i][j-1]}{lines[i][j-2]}{lines[i][j-3]}" == "XMAS") count++;
                    if (i + 3 < rows && $"{lines[i][j]}{lines[i+1][j]}{lines[i+2][j]}{lines[i+3][j]}" == XMAS) count++;
                    if (i - 3 >= 0 && $"{lines[i][j]}{lines[i-1][j]}{lines[i-2][j]}{lines[i-3][j]}" == XMAS) count++;
                    if (j + 3 < columns && i - 3 >= 0 && $"{lines[i][j]}{lines[i-1][j+1]}{lines[i-2][j+2]}{lines[i-3][j+3]}" == XMAS) count++;
                    if (j + 3 < columns && i + 3 < rows && $"{lines[i][j]}{lines[i+1][j+1]}{lines[i+2][j+2]}{lines[i+3][j+3]}" == XMAS) count++;
                    if (j - 3 >= 0 && i - 3 >= 0 && $"{lines[i][j]}{lines[i-1][j-1]}{lines[i-2][j-2]}{lines[i-3][j-3]}" == XMAS) count++;
                    if (j - 3 >= 0 && i + 3 < rows && $"{lines[i][j]}{lines[i+1][j-1]}{lines[i+2][j-2]}{lines[i+3][j-3]}" == XMAS) count++;

            }

        }
        return count;

    }

    public long GetTotalPartB()
    {
        int count = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {

                if (j - 1 >= 0 && j + 1 < columns && i - 1 >= 0 && i + 1 < rows &&
                    ($"{lines[i - 1][j - 1]}{lines[i][j]}{lines[i + 1][j + 1]}" == "MAS"
                     || $"{lines[i - 1][j - 1]}{lines[i][j]}{lines[i + 1][j + 1]}" == "SAM") &&
                    ($"{lines[i - 1][j + 1]}{lines[i][j]}{lines[i + 1][j - 1]}" == "MAS"
                     || $"{lines[i - 1][j + 1]}{lines[i][j]}{lines[i + 1][j - 1]}" == "SAM"))
                    count++;


            }


        }
        return count;    
    }
}