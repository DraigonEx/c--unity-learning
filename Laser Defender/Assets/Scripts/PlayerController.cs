using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject basicLaser;
    public bool autoPlay = false;
    public float moveSpeed;
    public float padding = 0.5f;

    //private float mousePosInBlocks;
    private float minX, maxX, minY, maxY;

    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;
        minY = bottomMost.y + padding;
        maxY = topMost.y - padding;
    }

    // Update is called once per frame
    void Update () {
        if (!autoPlay)
        {
            MoveWithKeys();
            CheckForLaser();
        }
        else
        {
            AutoPlay();
        }
    }

    private void AutoPlay()
    {
        // TODO : track closest enemy and line up with them

        //Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
        //var ballPos = ball.transform.position;
        //paddlePos.x = ballPos.x;
        //this.transform.position = paddlePos;
    }

    private void MoveWithKeys()
    {
        Vector3 translate = new Vector3
            (
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0
            );
        transform.Translate(translate * moveSpeed * Time.deltaTime, Space.World);
        float shipPosX = Mathf.Clamp(transform.position.x, minX, maxX);
        float shipPosY = Mathf.Clamp(transform.position.y, minY, maxY);
        
        transform.position = new Vector3(shipPosX, shipPosY, transform.position.z);
    }

    private void CheckForLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject laser = Instantiate(basicLaser, transform.position, Quaternion.identity);
        }
    }

}
