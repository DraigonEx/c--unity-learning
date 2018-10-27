using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    private LevelManager levelManager;
    private int maxHits;
    public AudioClip crack;
    public Sprite[] hitSprites;
    public GameObject smoke;

    private int timesHit;
    public static int breakableCount = 0;
    private bool isBreakable;
    private AudioSource myAudio;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        myAudio = GetComponent<AudioSource>();
        timesHit = 0;
        maxHits = hitSprites.Length + 1;
        isBreakable = (this.tag == "Breakable");
        if (isBreakable)
        {
            breakableCount++;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position);
        if (isBreakable)
        {
            //myAudio.time = 0.5f;
            //myAudio.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isBreakable)
        {
            HandleHits();
        }
    }

    private void HandleHits()
    { 
        timesHit++;
        print(timesHit + " / " + maxHits);
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }

    private void PuffSmoke()
    {
        GameObject puff = Instantiate(smoke, gameObject.transform.position, Quaternion.identity);
        var smokePuff = puff.GetComponent<ParticleSystem>().main;
        smokePuff.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

}
