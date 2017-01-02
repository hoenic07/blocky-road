using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Editable : MonoBehaviour
{

    public bool isEditable = true;
    public bool isEditing = false;
    public float changePerPress = 0.2f;
    private float snapArea = 0.6f;
    public BasicStreetBlock editorObject;

    public Main creator;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isEditing)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(changePerPress, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(-changePerPress, 0,0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, changePerPress);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -changePerPress);
            }
            if (Input.GetKey(KeyCode.Z))
            {
                TrySnap();
            }            
        }
	}

    public void OnMouseDown()
    {
        Debug.Log("remove!");

        var creator = this.creator;
        var editorObject = this.editorObject;
        if (creator == null)
        {
            var c = gameObject.transform.parent.gameObject.GetComponent<Editable>();
            creator = c.creator;
            editorObject = c.editorObject;
        }

        if (isEditable && creator !=null && creator.isRemovingBlock)
        {
            creator.RemoveObject(editorObject);
        }
    }

    void TrySnap()
    {
        var t = editorObject.GetReferenceForSnapping();
        var leftCur = t.position.x + t.localScale.x / 2;
        var rightCur = t.position.x - t.localScale.x / 2;
        var y = t.position.y;

        Debug.Log("Try snap");

        foreach (var eo in creator.createdStreets)
        {
            var otherT = eo.GetReferenceForSnapping();
            if (eo.GameObject == this) continue;

            var rightOther = otherT.position.x - otherT.localScale.x / 2;
            var leftOther = otherT.position.x + otherT.localScale.x / 2;
            var yOther = otherT.position.y;

            if (Mathf.Abs(rightOther - leftCur) <= snapArea && Mathf.Abs(yOther-y) <= snapArea)
            {
                var x = rightOther - transform.localScale.x / 2;
                transform.position = new Vector3(x, yOther, transform.position.z);
                return;
            }

            if (Mathf.Abs(leftOther - rightCur) <= snapArea && Mathf.Abs(yOther - y) <= snapArea)
            {
                var x = leftOther + transform.localScale.x / 2;
                transform.position = new Vector3(x, yOther, transform.position.z);
                Debug.Log("Snap!");
                return;
            }

        }

        Debug.Log("nothing found");
    }
}
