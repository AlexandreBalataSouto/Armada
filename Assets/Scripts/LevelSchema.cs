using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSchema : MonoBehaviour
{
    private class EnemyName
    {
        public const string Skull = "Skull";
        public const string WeirdSkull = "Weird_Skull";
    }

    public class Enemy {
        // public int amountEnemy { get; private set; } = 0;
        public string nameEnemy { get; private set; } = "";
        public float rateEnemy { get; private set; } = 0;
        
        public Enemy(string name, float rate)
        {
            nameEnemy = name;
            rateEnemy = rate;
        }
    }
    public int NumEnemiesAndBullets { get; private set; } = 0;
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    
    public void SetLevelSchema(string level){
         switch(level){
            case "Level01":
                Enemies.Add(new Enemy(EnemyName.Skull,1f));
                Enemies.Add(new Enemy(EnemyName.Skull,1f));
                Enemies.Add(new Enemy(EnemyName.Skull,1f));
                Enemies.Add(new Enemy(EnemyName.WeirdSkull,2f));
                Enemies.Add(new Enemy(EnemyName.WeirdSkull,0.5f));
                Enemies.Add(new Enemy(EnemyName.WeirdSkull,0.5f));
                Enemies.Add(new Enemy(EnemyName.Skull,3f));

                foreach (Enemy Enemy in Enemies)
                {
                    NumEnemiesAndBullets++;
                }
            break;
        }
    }
}
