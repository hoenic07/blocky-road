using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedable : MonoBehaviour {

    private float speedUp = 700.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Speed! " + collision.gameObject);
        if (collision.gameObject.GetComponent<Car>()!=null)
        {
            Debug.Log("apply speed!");
            GetComponentInChildren<AudioSource>().Play();
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(-speedUp*Main.globalScale,0 , 0));

        }
    }

}
