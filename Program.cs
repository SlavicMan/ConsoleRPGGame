using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Run();
        }
        public static void Run()
        {
            Player curPlayer = null;
            Monster curMonster = null;
            bool playerAttacking, monsterAttacking;
            
            if (curPlayer == null || curPlayer.Dead)
            {
                curPlayer = SpawnPlayer();
                curPlayer.XPToLevelUp = curPlayer.Level * 1.5f;
            }
            if (!curPlayer.Dead)
            {
                if (curMonster == null || curMonster.Dead)
                {
                    curMonster = SpawnMonster(curPlayer.Level);
                    Console.WriteLine(curMonster.ID);
                    Console.WriteLine("A monster is before you, \n" +
                        "Health: {0} \n" + "Damage: {1} \n", curMonster.Health, curMonster.Damage);
                    Console.ReadLine();
                }
            }
        }
        public static Player SpawnPlayer()
        {
            Player player = new Player
            {
                Health = 100,
                Damaage = 5,
                Level = 1,
                Dead = false,
            };
            // Creates player obviously
            return player;
        }
        public static Monster SpawnMonster(int playerLevel)
        {
            Random random = new Random();
            Monster monster = new Monster
            {
                Health = 100 * (playerLevel * 0.5),
                Damage = 5,
                XPOnKill = (float)RandomExtension.NextDoubleRange(random, 0, 3.5),
                ID = random.Next(0, 999),
                Dead = false
            };
            return monster;
        }
    }
    public static class RandomExtension
    {
        public static double NextDoubleRange(this Random random, double minNum, double maxNum)
        {
            return random.NextDouble() * (maxNum - minNum) + minNum;
        }
    }
    public class Monster
    {
        public double Health;
        public float Damage;
        public float XPOnKill;
        public bool Dead;
        public int ID;
    }
    public class Player
    {
        public int Health;
        public int Damaage;
        public int Level;
        public float XPToLevelUp;
        public bool Dead;
    }
}
