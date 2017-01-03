using Assets.Scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject levelButtonPrefab;


	// Use this for initialization
	void Start () {
        var lvls = LevelLoader.GetAllLevelMeta();
        var parent = GameObject.Find("LevelPanel").transform;
        int i = 0;
        int j = 0;
        foreach (var lvl in lvls)
        {
            Debug.Log("lvl: " + lvl.id);
            var go = Instantiate(levelButtonPrefab, new Vector3(i*240+580, -j*80+550), Quaternion.identity);
            go.GetComponent<LevelButton>().levelMeta = lvl;
            go.GetComponentInChildren<Text>().text = lvl.name;
            go.transform.SetParent(parent, true);
            i++;
            if (i == 3)
            {
                i = 0;
                j++;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
