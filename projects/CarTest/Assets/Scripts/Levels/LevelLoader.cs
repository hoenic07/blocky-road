using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Levels
{
    public class LevelLoader
    {
        private const string META_PATH = "Assets/Data/Levels.json";
        private const string LEVEL_PATH = "Assets/Data/Levels/";


        public static LevelMeta[] GetAllLevelMeta()
        {
            try
            {
                var json = File.ReadAllText(META_PATH);
                var lvlRoot = JsonUtility.FromJson<LevelRoot>(json);
                return lvlRoot.levels;
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
                return new LevelMeta[0];
            }
        }

        public static Level GetLevel(LevelMeta level)
        {
            try
            {
                var json = File.ReadAllText(LEVEL_PATH + level.id + ".json");
                return JsonUtility.FromJson<Level>(json);
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }

    }
}
