using System.Security.Cryptography;

namespace AdventOfCode._2015;

public class Day4(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int i = 0;
        using (MD5 md5 = MD5.Create())
        {
            while (true)
            {

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{lines[0]}{i.ToString()}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                if (Convert.ToHexString(hashBytes).StartsWith("00000")) return i;

                i++;

            }
        }
    }

    public long GetTotalPartB()
    {
        int i = 0;
        using (MD5 md5 = MD5.Create())
        {
            while (true)
            {

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{lines[0]}{i.ToString()}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                if (Convert.ToHexString(hashBytes).StartsWith("000000")) return i;

                i++;

            }
        }    }
}