using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class CameraKeyboardController : Controller<TileApp> {

	// Use this for initialization
	void Start () {
		
	}

    float moveSpeed = 20f;
	
	// Update is called once per frame
	void Update () {

        Vector3 translate = new Vector3
            (
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            );
        this.transform.Translate(translate * moveSpeed * Time.deltaTime, Space.World);
	}
}
