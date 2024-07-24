using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day21(string[] lines) : IDay
{
    public record Item(string Title, int Cost, int Damage, int Armor);

    private int bossHitPoints = 104;
    private int bossDamage = 8;
    int bossArmor = 1;
    private int myHitPoints = 100;

    public long GetTotalPartA()
    {
        
        return GetOptimum(true);
    }
    public long GetTotalPartB()
    {
        
        return GetOptimum(false);    }
    private void Increase(Item item, ref int damage, ref int arm, ref int cost)
    {
        damage += item.Damage;
        arm += item.Armor;
        cost += item.Cost;
    }

    private void Decrease(Item item, ref int damage, ref int arm, ref int cost)
    {
        damage -= item.Damage;
        arm -= item.Armor;
        cost -= item.Cost;
    }

    private bool DoIWin(int myDamage, int myArmor)
    {
        int myHit = myHitPoints;
        int bossHit = bossHitPoints;
        bool player = true;
        while (true)
        {
            if (player)
            {
                player = false;
                bossHit -= Math.Max(1,(myDamage - bossArmor));
            }
            else
            {
                player = true;
                myHit -=Math.Max(1, (bossDamage - myArmor));
            }

            if (bossHit <= 0) return true;
            if (myHit <= 0) return false;
        }
    }

    private int GetOptimum(bool partOne)
    {
        bool weapon = true, armor = false, rings = false;
        
        List<Item> items = [];

        List<int> weaponsInts = [];
        List<int> armorInts = [-1];
        List<int> ringsInts = [-1];
        int index = 0;
        foreach (var line in lines)
        {
            if (line.Trim() == "" && !armor)
            {
                armor = true;
                weapon = false;
                continue;
            }

            if (line.Trim() == "" && !rings)
            {
                rings = true;
                armor = false;
                continue;
            }

            int[] numbers = line.GetNumbers();
            if (numbers.Length == 0) continue;
            if (weapon) weaponsInts.Add(index++);
            if (armor) armorInts.Add(index++);
            if (rings) ringsInts.Add(index++);

            string title = weapon ? "Weapon" : armor ? "Armor" : "Rings";
            items.Add(new Item(title, numbers[0], numbers[1], numbers[2]));
        }

        int minCost = int.MaxValue;
        int maxCost = 0;
        
        foreach (int w in weaponsInts)
        {
            int cost = 0, damage = 0, arm = 0;
            damage += items[w].Damage;
            arm += items[w].Armor;
            cost += items[w].Cost;

            foreach (int r1 in ringsInts)
            {
                if (r1 > -1) Increase(items[r1], ref damage, ref arm, ref cost);
                var rings2 = ringsInts.Where(x => x!= r1).ToList();
                rings2.Add(-1);
                foreach (int r2 in rings2)
                {
                    if (r2 > -1) Increase(items[r2], ref damage, ref arm, ref cost);
                    foreach (int a in armorInts)
                    {
                        if (a > -1)
                        {
                            Increase(items[a], ref damage, ref arm, ref cost);

                            bool doIWin = DoIWin(damage, arm);
                            if (doIWin && cost < minCost) minCost = cost;
                            if (!doIWin&& cost > maxCost) maxCost = cost;
                            
                            Decrease(items[a], ref damage, ref arm, ref cost);
                        }
                    }

                    if (r2 > -1) Decrease(items[r2], ref damage, ref arm, ref cost);
                }

                if (r1 > -1) Decrease(items[r1], ref damage, ref arm, ref cost);
            }
        }

        return partOne?minCost:maxCost;
    }
    
}