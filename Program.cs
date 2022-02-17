using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        public static Player curPlayer;
        public static Monster curMonster;
        public static bool playerAttacking, monsterAttacking;
        static void Main(string[] args)
        {
            Run();
        }
        public static void Run()
        {
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

                }
                if(!curMonster.Dead || curMonster != null)
                {
                    playerAttacking = true;
                    GameLoop();
                }
            }
        }
        public static void GameLoop()
        {
            Console.WriteLine("A monster is before you, \n" +
                "Health: {0} \n" + "Damage: {1} \n", curMonster.Health, curMonster.Damage);
            Console.WriteLine("Press A to attack or D to block \n");
            if(playerAttacking)
            {
                if (Console.ReadKey().Key == ConsoleKey.A)
                {
                    float rolledDamage = RollEffects(curPlayer.Damage, curPlayer, true);
                    if (rolledDamage == 0)
                    {
                        Console.WriteLine("Enemy dodged your attack!");
                        Run();
                    }
                    else
                    {
                        Console.WriteLine("\nYou did {0} damage to the enemy! \n" +
                            "Their health is now {1}", rolledDamage, curMonster.Health);
                        Run();
                    }
                    curMonster.Health = (curMonster.Health - rolledDamage);
                }
            } else
            {
                float rolledDamage = RollEffects(curMonster.Damage, curMonster, playerAttacking);
                if(rolledDamage == 0)
                {
                    Console.WriteLine("you dodged the enemies attack!");
                    Run();
                } else
                {
                    Console.WriteLine("\\nThe enemy did {0} damage to you! \n" +
                        "Your health is now {1}", rolledDamage, curPlayer.Health);
                    Run();
                }

            }

            Console.ReadLine();
        }
        #region Attack Rolling
        public static float RollEffects(int baseDamage, Player curPlayer, bool playerAttacking)
        {
            float damage = baseDamage;
            int rollOne, rollTwo;
            int critChance = curPlayer.CritChance;
            int enemyDodgeChance = curMonster.DodgeChance;
            Random rand = new Random();
            rollOne = rand.Next(0, 100);
            rollTwo = rand.Next(0, 100);

            if(rollTwo == enemyDodgeChance)
            {
                return damage = 0;
            }
            if (rollOne == critChance)
                damage = (curPlayer.Damage * curPlayer.CritDamage);
            return damage;
        }
        public static float RollEffects(int baseDamage, Monster curMonster, bool playerAttacking)
        {
            float damage = baseDamage;
            int rollOne, rollTwo;
            int critChance = curPlayer.CritChance;
            int enemyDodgeChance = curMonster.DodgeChance;
            Random rand = new Random();
            rollOne = rand.Next(0, 100);
            rollTwo = rand.Next(0, 100);

            if (rollTwo == enemyDodgeChance)
            {
                return damage = 0;
            }
            if (rollOne == critChance)
                damage = (curMonster.Damage * curMonster.CritDamage);
            return damage;
        }
        #endregion

        #region Spawning
        public static Player SpawnPlayer()
        {
            Player player = new Player
            {
                Health = 100,
                Damage = 5,
                Level = 1,
                BlockChance = 10,
                CritChance = 5,
                CritDamage = 1.2f,
                XP = 0,
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
                Health = 100 * (playerLevel * 0.5f),
                Damage = 5,
                DodgeChance = 5,
                BlockChance = 0,
                CritDamage = 5,
                XPOnKill = (float)RandomExtension.NextDoubleRange(random, 0, 3.5),
                ID = random.Next(0, 999),
                Dead = false,
            };
            return monster;
        }
        #endregion
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
        public float Health;
        public int Damage;
        public float XPOnKill;
        public bool Dead;
        public int DodgeChance;
        public int BlockChance;
        public int CritDamage;
        public int ID;
    }
    public class Player
    {
        public float Health;
        public int Damage;
        public int Level;
        public int BlockChance;
        public int CritChance;
        public float CritDamage;
        public float XPToLevelUp;
        public float XP;
        public bool Dead;
    }
}
