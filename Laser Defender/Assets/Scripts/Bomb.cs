using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField] float moveSpeed = 2f;

    Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        Player player = null;
        try
        {
            player = FindObjectOfType<Player>();
        }
        catch { }
        if (player != null)
        {
            targetPosition = player.transform.position;
        }
        else
        {
            targetPosition = new Vector3(0, -10, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
}
