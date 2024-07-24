using System.Globalization;

namespace AdventOfCode._2015;
using AdventOfCode.Extensions;
public class Day22(string[] lines) : IDay
{
    record struct Player(int Hit, int Armor, int Mana);
    record struct Boss(int Hit, int Damage);
    int minMana = int.MaxValue;

    enum Spell
    {
        Undefined,
        MagicMissile,
        Drain,
        Shield,
        Poison,
        Recharge
    }
    public long GetTotalPartA()
    {
        return Calculate(true);
    }
    public static void Method(string arg)
    {
        _ = arg ?? throw new ArgumentNullException(paramName: nameof(arg), message: "arg can't be null");

        // Do work with arg.
    }
    public long GetTotalPartB()
    {
        return Calculate(false);
    }

    private int Calculate(bool partA)
    {
        foreach (var variant in Combinatorics.Variants(12, 5))//1382
        {
            Player player = new Player(50, 0, 500);
            Boss boss = new Boss(55, 8);

            int shieldTimer = 0, poisonTimer = 0, rechargeTimer = 0;
            int mana = 0;
            foreach (int v in variant)
            {
                int magic = v + 1;
                Spell spell = (Spell)magic;
               
                if (poisonTimer > 0 && (Spell)magic == Spell.Poison) continue;

                if (shieldTimer > 0 && (Spell)magic == Spell.Shield) continue;

                if (!partA) player.Hit -= 1;
                if (player.Hit <= 0) break;
                mana+=PlaySet((Spell)magic, ref player, ref boss, ref shieldTimer, ref poisonTimer, ref rechargeTimer);
                if(player.Mana<0) break;
                if (boss.Hit <= 0)
                {
                    if (mana < minMana)
                    {
                        Console.WriteLine("-------");
                        foreach(int i in variant) Console.WriteLine(i);
                        Console.WriteLine($"---{mana}---");
                        minMana = mana;
                    }
                    break;
                }
                else if (player.Hit <= 0) break;

                if (!partA) player.Hit -= 1;
                if (player.Hit <= 0) break;
                PlaySet(Spell.Undefined, ref player, ref boss, ref shieldTimer, ref poisonTimer, ref rechargeTimer);
                if (boss.Hit <= 0)
                {
                    if (mana < minMana)
                    {
                        Console.WriteLine("-------");
                        foreach(int i in variant) Console.WriteLine(i);
                        Console.WriteLine($"---{mana}---");
                        minMana = mana;
                    }

                    break;
                }
                else if (player.Hit <= 0) break;

            }
           
         
        }

        return minMana;
    }
     private int PlaySet(Spell spell, ref Player player, ref Boss boss, ref int shieldTimer, ref int poisonTimer, ref int rechargeTimer)
     {
         int manaSpend = 0;
        if (shieldTimer > 0)
        {
            player.Armor = 7;
            shieldTimer--;
        }
        else player.Armor = 0;
        
        if (poisonTimer > 0)
        {
            poisonTimer--;
            boss.Hit -= 3;
        }

        if (rechargeTimer > 0)
        {
            player.Mana += 101;
            rechargeTimer--;
        }
        
        
        if (spell == Spell.Poison)
        {
            player.Mana -= 173;
            manaSpend += 173;
            poisonTimer = 6;
        }
        else if (spell == Spell.MagicMissile)
        {
            player.Mana -= 53;
            manaSpend += 53;
            boss.Hit -= 4;
        }
        else if (spell == Spell.Drain)
        {
            player.Mana -= 73;
            manaSpend += 73;
            boss.Hit -= 2;
            player.Hit += 2;
        }
        else if (spell == Spell.Shield)
        {
            player.Mana -= 113;
            manaSpend += 113;
            shieldTimer = 5;
            player.Armor = 7;
        }
        else if (spell == Spell.Recharge)
        {
            player.Mana -= 229;
            manaSpend += 229;
            rechargeTimer = 5;
        }
        if(spell==Spell.Undefined)
        {
            player.Hit -= Math.Max(1, (boss.Damage-player.Armor)); 
        }

        return manaSpend;
     }
}