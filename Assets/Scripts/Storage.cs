using System;
using UnityEngine;

namespace DefaultNamespace
{

    public class Storage
    {
        public static void SaveCoin(int coin)
        {
            PlayerPrefs.SetInt("coin", coin);
        }

        public static void SaveHighScore(int highScore)
        {
            PlayerPrefs.SetInt("high_score", highScore);
        }

        public static int GetHighScore()
        {
            return PlayerPrefs.GetInt("high_score", 0);
        }
        
        public static int GetCoin()
        {
            return PlayerPrefs.GetInt("coin", 0);
        }
        
        public static void Save(int coin, int highScore)
        {
            SaveCoin(coin);
            SaveCoin(highScore);
        }
    }
}