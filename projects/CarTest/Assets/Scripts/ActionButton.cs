using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    private bool isPressing = false;
	// Use this for initialization
	void Start () {;
        //this.GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    ProcessAction(this.GetComponentInChildren<Text>().text);
        //});
    }
	
	// Update is called once per frame
	void Update () {
        if (isPressing)
        {
            ProcessAction(this.GetComponentInChildren<Text>().text);
        }
	}

    private void ProcessAction(string action)
    {
        var ed = FindObjectOfType<Main>().currentEditing;
        if (ed == null) return;
        switch (action)
        {
            case "<":
                ed.Left();
                break;
            case ">":
                ed.Right();
                break;
            case "+":
                ed.Up();
                break;
            case "-":
                ed.Down();
                break;
            case "snap":
                ed.TrySnap();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
    }
}
