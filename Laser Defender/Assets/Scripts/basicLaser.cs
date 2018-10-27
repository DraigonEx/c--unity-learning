using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicLaser : MonoBehaviour {

    public float speed = 5;
    public float maxY;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        maxY = topMost.y + 1;
    }

    // Update is called once per frame
    void Update () {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if(transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
	}
}
