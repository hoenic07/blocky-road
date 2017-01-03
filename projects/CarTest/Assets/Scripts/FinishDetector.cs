using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger!" + collision.gameObject.name);
        if (collision.gameObject.GetComponent<Car>() != null)
        {
            Debug.Log("finished");
            FindObjectOfType<Main>().ShowFinished();
            GetComponentInChildren<AudioSource>().Play();
        }
    }
}
