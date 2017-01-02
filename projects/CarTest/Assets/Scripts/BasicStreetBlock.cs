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

        public virtual void SnapLeft(Transform refObj)
        {

        }

        public virtual void SnapRight(Transform refObj)
        {

        }

    }

    public class JumpStreetBlock : BasicStreetBlock
    {
        public override Transform GetReferenceForSnapping()
        {
            return GameObject.transform.GetChild(0);
        }

        public override void SnapLeft(Transform refObj)
        {

        }

        public override void SnapRight(Transform refObj)
        {
            base.SnapRight(refObj);
        }
    }
}
