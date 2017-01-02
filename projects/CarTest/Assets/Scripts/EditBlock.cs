using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditBlock : MonoBehaviour, IPointerClickHandler
{

    private Main main;

	// Use this for initialization
	void Start () {
        main = FindObjectOfType<Main>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        main.EditBlockSelected(gameObject);
    }
}
