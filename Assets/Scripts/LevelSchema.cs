using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSchema : MonoBehaviour
{
    public class Enemy {
        public int amountEnemy { get; private set; } = 0;
        public string nameEnemy { get; private set; } = "";
        
        public Enemy(int amount, string name)
        {
            amountEnemy = amount;
            nameEnemy = name;
        }
    }
    public int NumEnemiesAndBullets { get; private set; } = 0;
    public float EnemyRate { get; private set; } = 0;
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    
    public void SetLevelSchema(string level){
         switch(level){
            case "Level01":
                Enemies.Add(new Enemy(3,"Skull"));
                foreach (Enemy Enemy in Enemies)
                {
                    NumEnemiesAndBullets += Enemy.amountEnemy;
                }

                EnemyRate = 3f;
            break;
        }
    }
}
