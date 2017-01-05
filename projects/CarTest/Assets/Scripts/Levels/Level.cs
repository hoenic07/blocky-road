using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Levels
{
    [Serializable]
    public class Level
    {
        public FixedBlock[] staticBlocks;
        public EditorBlock[] editorBlocks;
    }

    [Serializable]
    public class FixedBlock
    {
        public BlockType type;
        public int x;
        public int y;
        public int rotation;
        public bool isStart;
    }

    [Serializable]
    public class EditorBlock
    {
        public BlockType type;
        public int count;

        public Text uiText;
        public GameObject gameObject;

        public void ChangeCount(int change)
        {
            count += change;
            uiText.text = count + "x";
        }

    }
}
