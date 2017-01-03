using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedable : MonoBehaviour {

    public float speedUp = 1000.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Speed!" + collision.gameObject.name);
        if (collision.gameObject.GetComponent<Car>()!=null)
        {
            Debug.Log("apply speed!");
            GetComponentInChildren<AudioSource>().Play();
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(-speedUp,0 , 0));

        }
    }

}
