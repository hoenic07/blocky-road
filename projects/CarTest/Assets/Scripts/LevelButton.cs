using Assets.Scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    public LevelMeta levelMeta;
    private static bool isLevelLoading = false;

	// Use this for initialization
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isLevelLoading) return;
            isLevelLoading = true;
            Main.levelMeta = levelMeta;
            FindObjectOfType<Car>().ApplyStartForce();
            StartCoroutine(LoadLevel());
            
        });
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1f);
        isLevelLoading = false;
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
