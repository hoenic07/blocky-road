using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    

    public class BasicStreetBlock
    {
        public GameObject GameObject { get; set; }
        public BlockType Type { get; set; }

        public virtual Transform GetReferenceForSnapping()
        {
            return GameObject.transform;
        }

        public virtual void SnapLeft(float yOther, float rightOther)
        {
            var x = rightOther - (Editable.blockSize / 2) * Main.globalScale;
            GameObject.transform.position = new Vector3(x, yOther - 0.00001f, GameObject.transform.position.z);
        }

        public virtual void SnapRight(float yOther, float leftOther)
        {
            var x = leftOther + (Editable.blockSize / 2)* Main.globalScale;
            GameObject.transform.position = new Vector3(x, yOther-0.00001f, GameObject.transform.position.z);
        }

    }

    public class JumpStreetBlock : BasicStreetBlock
    {
        public override Transform GetReferenceForSnapping()
        {
            return GameObject.transform.GetChild(0);
        }

        public override void SnapLeft(float yOther, float rightOther)
        {
            var x = rightOther - (Editable.blockSize / 2) * Main.globalScale;
            GameObject.transform.position = new Vector3(x, yOther - 0.00001f, GameObject.transform.position.z);
        }

        public override void SnapRight(float yOther, float leftOther)
        {
            var x = leftOther + (Editable.blockSize / 2) * Main.globalScale;
            GameObject.transform.position = new Vector3(x, yOther - 0.00001f, GameObject.transform.position.z);
        }
    }
}
