using Assets.Scripts.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public enum BlockType { Invalid=0, Street=1, Jump=2, Speed=3, Finish=4 }
    public class StreetBlockFactory
    {
        public static BasicStreetBlock CreateStreetBlock(BlockType bt, GameObject go=null)
        {
            if (go == null)
            {
                var prefab = LoadPrefabByType(bt);
                go = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            }

            switch (bt)
            {
                case BlockType.Street:
                case BlockType.Speed:
                case BlockType.Finish:
                    return new BasicStreetBlock { GameObject = go, Type = bt };
                case BlockType.Jump:
                    return new JumpStreetBlock { GameObject = go, Type = bt };
                default:
                    return null;
            }
        }

        public static GameObject LoadPrefabByType(BlockType type)
        {
            var path = "Prefabs/" + type;
            return Resources.Load(path) as GameObject;
        }

        public static GameObject LoadEditBlockByType(EditorBlock block)
        {
            var path = "Prefabs/EditBlock";
            var obj = GameObject.Instantiate(Resources.Load(path) as GameObject);
            var ri = obj.GetComponent<RawImage>();
            var tx = Resources.Load("Blocks/" + block.type) as Texture;
            ri.texture = tx;
            block.uiText = obj.GetComponentInChildren<Text>();
            Debug.Log("C: " + obj.gameObject);
            block.gameObject = obj;
            block.ChangeCount(0);
            return obj;
        }
    }
}
