using System.Text;

namespace AdventOfCode._2016;

public class Day5(string[] lines):IDay
{
    private System.Security.Cryptography.MD5 md5;
    public long GetTotalPartA()
    {
        string door = "uqwqemis";
        int index = 0;
        md5 = System.Security.Cryptography.MD5.Create();
        string pass = "";
        while (true)
        {
            string door1 = $"{door}{index}";

            string hash = generateHash(door1);
            if (hash.Substring(0,7) == "00-00-0")
            {
                pass+=hash.ToString()[7];
                if (pass.Length == 8)
                {
                    Console.WriteLine(pass.ToLower());
                    break;
                }
            }
            index++;
        }

        return 0;
    }
    
    
  
    public long GetTotalPartB()
    {
        string door = "uqwqemis";
        int index = 0;
        md5 = System.Security.Cryptography.MD5.Create();
        char[] pass = new char[8];
        int count = 0;
        while (true)
        {
            string door1 = $"{door}{index}";

            string hash = generateHash(door1);
            if (hash.Substring(0,7) == "00-00-0")
            {
                char pos = hash.ToString()[7];
                int position = 0;
                if (!int.TryParse(pos.ToString(), out position)){ index++;continue;}
                if (position >= 0 && position <= 7 && pass[position] =='\0')
                {
                    pass[position] = hash.ToString()[9];
                    count++;
                    
                    if (count == 8)
                    {
                        Console.WriteLine(new string(pass).ToLower());
                        break;
                    }
                    
                }
            }
            index++;
        }

        return 0;
    }
    
    string generateHash(string input)
    {
        byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
        return BitConverter.ToString(data);
    }
}