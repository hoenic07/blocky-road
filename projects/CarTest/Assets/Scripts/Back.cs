﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Back : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Menu");
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}