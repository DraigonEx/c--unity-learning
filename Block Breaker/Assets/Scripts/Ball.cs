using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Paddle paddle;
    private bool hasStarted = false;
    private Vector3 paddleToBallVector;
    private Rigidbody2D rigidBody2D;

	// Use this for initialization
	void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(!hasStarted)
            this.transform.position = paddle.transform.position + paddleToBallVector;

        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rigidBody2D.velocity = new Vector2(2f, 10f);
        }
	}
}
