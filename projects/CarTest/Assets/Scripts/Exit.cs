﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
