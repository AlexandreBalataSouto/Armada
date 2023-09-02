using System.Numerics;

public class Constants
{
    public class Common{
        public const string MAIN_CAMERA = "MainCamera";
        public const string PLAYER = "Player";
        public const string SPIDER = "Spider";
        public const string ORB = "Orb";
        public const string WASP = "Wasp";
        public const string LASER_ENEMY = "LaserEnemy";
        public const string FLAME_KRAKEN = "Flame_Kraken";
        public const string LIMIT = "Limit";
        public const string LIMIT_LASER = "LimitLaser";
        public const string LIMIT_FLAME = "Limit_Flame";
        public const string ENEMY = "Enemy";
        public const string ENEMY_KRAKEN = "Enemy_Kraken";
         public const string BULLET = "Bullet";
        public const string BULLET_ENEMY = "BulletEnemy";
        public const string STOP_POINTS= "StopPoints";
        public const string STOP_POINT_KNIGHT= "StopPoint_Knight";
        public const string STOP_POINT_ORB = "StopPoint_Orb";
        public const string STOP_POINT_WARLOCK = "StopPoint_Warlock";
        public const string SPEAR = "Spear";
        public const string SHIELD = "Shield";
        public const int OBJECT_AMOUNT = 100;
    }
    
    public class Player{
        public const float SPEED = 8f;
        public const float FIRE_RATE = 0.5f;
        public const float RELOAD_TIME = 3f;
    }

    public class Bullet{
        public const float KRAKEN_FLAME_SPEED = 10f;
        public const float LASER_ENEMY_SPEED = 8f;
        public const float ORB_SHOOT_SPEED = 6f;
        public const float PLAYER_SPEED = 14f;
        public const float SPIDER_SPEED = 8f;
    }

    public class Shield{
        public const float SPEED = 90f;
    }

    public class Skull{
        public const float SPEED = 5f;
        public const float AMPLITUDE = 1f;
        public const float FREQUENCY = 1f;
    }

    public class Spider{
        public const float SPEED = 2.5f;
        public const float ANGLE = 1f;
        public const float RADIUS = 3f;
        public const float FIRE_RATE = 1f;
    }

    public class Kraken{
        public const float SPEED = 2f;
    }

    public class Kraken_Flame{
        public const float NEXT_FIRE = 10f;
        public const float FIRE_RATE = 10f;
    }

    public class Knight{
        public const float FIRE_RATE = 6f;
        public const float SPEED_ATTACK = 15f;
        public const float SPEAR_ROTATION_AUX = 90f;
        public const float SPEAR_AUX = 10f;
        public static readonly float[] RANGE_FIRE_RATE = {2f, 6f};
        public static readonly int[] RANGE_INDEX_PATTERN = {1, 3};
        public static readonly Vector3 SPEAR_LOCAL_POSITION = new Vector3(1f, 0.5f, 1f);
        public class Pattern{
            public class Bretzel{
                public const float DISTANCE_X = 4f;
                public const float DISTANCE_Y = 2f;
                public const float MOVE_FREQ_X = 3f;
                public const float MOVE_FREQ_Y = 4f;
            }
            public class Pottery{
                public const float DISTANCE_X = 3f;
                public const float DISTANCE_Y = 3f;
                public const float MOVE_FREQ_X = 4f;
                public const float MOVE_FREQ_Y = 1f;
            }
            public class Attom{
                public const float DISTANCE_X = 5f;
                public const float DISTANCE_Y = 3f;
                public const float MOVE_FREQ_X = 2.5f;
                public const float MOVE_FREQ_Y = 2f;
            }
        }
    }

    public class Orb{
        public const float SPEED = 6f;
        public const float NEXT_FIRE = 0f;
        public const float FIRE_RATE = 0.25f;
        public static readonly  string[] ORB_SHOOT = {
          "Orb_shoot",
          "Orb_shoot_1",
          "Orb_shoot_2",
          "Orb_shoot_3",
          "Orb_shoot_4",
          "Orb_shoot_5",
          "Orb_shoot_6",
          "Orb_shoot_7"
        };
         public static readonly  string[] ORB_SHIELD = {
          "Shield",
          "Shield_1",
          "Shield_2",
          "Shield_3",
          "Shield_4",
          "Shield_5",
          "Shield_6",
          "Shield_7"
        };
    }

    public class Warlock{
        public const float SPEED = 1.5f;
        public const float AUX = 0.5f;
    }

    public class WarlockShield{
        public string SHIELD_NAME;
        public float JOURNEY_TIME;
        public float SPEED;
        public float SHIELD_X;
        public float SHIELD_Y;

        public WarlockShield(string sn, float sp, float sx, float sy)
        {
            JOURNEY_TIME = 1f;
            SHIELD_NAME = sn;
            SPEED = sp;
            SHIELD_X = sx;
            SHIELD_Y = sy;
        }
    }

    //Default value TEST
    //Shield 01 -> _speed = 0.8f / _shieldX = 5.5f / _shieldY = 4f
    //Shield 02 -> _speed = 0.75f / _shieldX = 4.5f / _shieldY = 3.5f
    //Shield 03 -> _speed = 0.5f / _shieldX = 3.5f / _shieldY = 3f
    //Shield 04 -> _speed = 0.25f / _shieldX = 3f / _shieldY = 2f
    public static readonly  WarlockShield[] WarlockShieldList = {
        new WarlockShield("WarlockShield", 0.8f, 5.5f, 4f),
        new WarlockShield("WarlockShield_1", 0.75f, 4.5f, 3.5f),
        new WarlockShield("WarlockShield_2", 0.5f, 3.5f, 3f),
        new WarlockShield("WarlockShield_3", 0.25f, 3, 2f),
    };

}