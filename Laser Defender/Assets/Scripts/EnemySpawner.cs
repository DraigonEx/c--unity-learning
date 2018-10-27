using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject basicEnemy;
    public enum formationType { staticFormation, pathFormation }
    public formationType thisFormationType;
    public float width = 10f;
    public float height = 5f;
    public float padding = 0.5f;
    public float speed = 1f;

    private float minX, maxX, minY, maxY;
    private bool movingRight = true;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftMost.x + (padding + width/2);
        maxX = rightMost.x - (padding + width/2);

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(basicEnemy, child.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float newX;
        if (movingRight)
        {
            newX = transform.position.x + 0.01f*speed*Time.deltaTime;
        }
        else
        {
            newX = transform.position.x - 0.01f*speed*Time.deltaTime;
        }
        newX = Mathf.Clamp(newX, minX, maxX);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        if(transform.position.x <= minX)
        {
            movingRight = true;
        }
        if(transform.position.x >= maxX)
        {
            movingRight = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}
