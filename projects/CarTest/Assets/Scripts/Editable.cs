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
    private float snapArea = 1f;
    public BasicStreetBlock editorObject;

    public Main creator;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isEditing)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Left();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Right();
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Up();
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Down();
            }
            if (Input.GetKeyUp(KeyCode.Z))
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
        var editable = this;
        if (creator == null)
        {
            var c = gameObject.transform.parent.gameObject.GetComponent<Editable>();
            creator = c.creator;
            editorObject = c.editorObject;
            editable = c;
        }
        if (creator == null) return;

        if (isEditable && creator.isRemovingBlock)
        {
            creator.RemoveObject(editorObject);
        }
        else if (isEditable)
        {
            if (creator.currentEditing != null) creator.currentEditing.isEditing = false;
            editable.isEditing = true;
            creator.currentEditing = editable;
        }
    }

    public void TrySnap()
    {
        var t = editorObject.GetReferenceForSnapping();
        var leftCur = t.position.x + t.lossyScale.x / 2;
        var rightCur = t.position.x - t.lossyScale.x / 2;
        var y = t.position.y;

        Debug.Log("Try snap");

        foreach (var eo in creator.streetBlocks)
        {
            var otherT = eo.GetReferenceForSnapping();
            if (eo.GameObject == this) continue;

            var rightOther = otherT.position.x - otherT.lossyScale.x / 2;
            var leftOther = otherT.position.x + otherT.lossyScale.x / 2;
            var yOther = otherT.position.y;

            if (Mathf.Abs(rightOther - leftCur) <= snapArea && Mathf.Abs(yOther-y) <= snapArea)
            {
                editorObject.SnapLeft(yOther, rightOther);
                Debug.Log("Snap!");
                return;
            }

            if (Mathf.Abs(leftOther - rightCur) <= snapArea && Mathf.Abs(yOther - y) <= snapArea)
            {
                editorObject.SnapRight(yOther, leftOther);
                Debug.Log("Snap!");
                return;
            }

        }

        Debug.Log("nothing found");
    }

    public void Left()
    {
        transform.position += new Vector3(changePerPress, 0, 0);
    }

    public void Right()
    {
        transform.position += new Vector3(-changePerPress, 0, 0);
    }

    public void Up()
    {
        transform.position += new Vector3(0, changePerPress);
    }

    public void Down()
    {
        transform.position += new Vector3(0, -changePerPress);
    }
}
