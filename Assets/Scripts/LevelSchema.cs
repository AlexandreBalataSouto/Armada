using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSchema : MonoBehaviour
{
    private class EnemyName
    {
        public const string Skull = "Skull";
        public const string WeirdSkull = "Weird_Skull";
        public const string Orb = "Orb";
        public const string Wasp = "Wasp";
    }

    public class Enemy {

        public string idEnemy { get; private set; } = "";
        public string nameEnemy { get; private set; } = "";
        public float intervalEnemy  { get; private set; } = 0f;
        public int pointSpawnEnemy { get; private set; } = 0;

        public Enemy(int id, string name, float interval, int pointSapwn)
        {
            idEnemy = name + id.ToString();
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
                foreach (Enemy Enemy in Enemies)
                {
                    NumEnemiesAndBullets++;
                }
            break;
        }
    }
    public int SetIdEnemy(){
        if(Enemies.Count <= 0)
        {
            return 0;
        }else{
            return (Enemies.Count - 1) + 1;
        }
    }
}
