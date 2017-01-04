using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Levels
{
    [Serializable]
    public class LevelRoot
    {
        public LevelMeta[] levels;
    }

    [Serializable]
    public class LevelMeta
    {
        public int id;
        public string name;
        public bool isDone; 
    }
}
