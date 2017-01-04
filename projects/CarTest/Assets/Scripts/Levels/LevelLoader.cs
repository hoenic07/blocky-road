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
        private const string META_PATH = "Data/LevelsMeta";
        private const string LEVEL_PATH = "Data/Levels/";
        private const string LEVELS_DONE_KEY = "LevelsDone";


        public static LevelMeta[] GetAllLevelMeta()
        {
            try
            {
                if (!PlayerPrefs.HasKey(LEVELS_DONE_KEY))
                {
                    PlayerPrefs.SetString(LEVELS_DONE_KEY, "");
                    PlayerPrefs.Save();
                }

                var levelsDone = PlayerPrefs.GetString(LEVELS_DONE_KEY)
                    .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(d => int.Parse(d));

                TextAsset bindata = Resources.Load(META_PATH) as TextAsset;
                var json = bindata.text;
                var lvlRoot = JsonUtility.FromJson<LevelRoot>(json);

                foreach (var lvl in lvlRoot.levels)
                {
                    lvl.isDone = levelsDone.Contains(lvl.id);
                }

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
                TextAsset bindata = Resources.Load(LEVEL_PATH + level.id) as TextAsset;
                var json = bindata.text;
                return JsonUtility.FromJson<Level>(json);
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }

        public static void SetAndSaveLevelDone(LevelMeta level)
        {
            if (level.isDone) return;
            level.isDone = true;
            var levelsDone = PlayerPrefs.GetString(LEVELS_DONE_KEY);
            var add = string.IsNullOrEmpty(levelsDone) ? level.id.ToString() : ("," + level.id);
            PlayerPrefs.SetString(LEVELS_DONE_KEY, levelsDone + add);
            PlayerPrefs.Save();
        }
    }
}
