using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private bool isStarted = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyStartForce()
    {
        if (isStarted) return;
        FindObjectOfType<AudioSource>().Play();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Debug.Log("Force!");
        rb.AddForce(new Vector3(-300, 0, 0));
        isStarted = true;
    }
}
