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

        public string idEnemy { get; private set; } = "";
        public string nameEnemy { get; private set; } = "";
        public float intervalEnemy  { get; private set; } = 0f;
        public int pointSpawnEnemy { get; private set; } = 0;

        public Enemy(string id, string name, float interval, int pointSapwn)
        {
            idEnemy = name + id;
            nameEnemy = name;
            intervalEnemy = interval;
            pointSpawnEnemy = pointSapwn;
        }
    }
    public int NumEnemiesAndBullets { get; private set; } = 0;
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    
    public void SetLevelSchema(string level){
         switch(level){
            case "Level01":
                //TODO -> SET ID IN A BETTER WAY
                //TODO -> GET POSITION OF POINTLIST
                Enemies.Add(new Enemy("0",EnemyName.Skull,1f, 0));
                Enemies.Add(new Enemy("1",EnemyName.Skull,1f, 1));
                Enemies.Add(new Enemy("2",EnemyName.Skull,1f, 2));

                foreach (Enemy Enemy in Enemies)
                {
                    NumEnemiesAndBullets++;
                }
            break;
        }
    }
}
