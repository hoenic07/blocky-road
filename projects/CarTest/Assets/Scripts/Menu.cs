using Assets.Scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Menu : MonoBehaviour {

    public GameObject levelButtonPrefab;


	// Use this for initialization
	void Start () {
        Main.globalScale = 1;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        var lvls = LevelLoader.GetAllLevelMeta();
        var parent = GameObject.Find("LevelPanel").transform;
        int i = 0;
        int j = 0;
        foreach (var lvl in lvls)
        {
            Debug.Log("lvl: " + lvl.id);
            var go = Instantiate(levelButtonPrefab, new Vector3(i*180-200, -j*60+130), Quaternion.identity);
            go.GetComponent<LevelButton>().levelMeta = lvl;
            var txts = go.GetComponentsInChildren <Text>();
            txts.First(d => d.name == "TextLevel").text = lvl.name;
            if (!lvl.isDone)
            {
                Destroy(txts.First(d => d.name == "TextCheck"));
            }
           
            go.transform.SetParent(parent, false);
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
