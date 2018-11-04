using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject shotPrefab;
    [SerializeField] AudioClip shotSound;
    [Range(0f, 1f)] [SerializeField] float shotVolume = 1f;
    [SerializeField] float shotSpeed = 2f;
    [SerializeField] float minTimeBetweenBombs = 0.2f;
    [SerializeField] float maxTimeBetweenBombs = 3f;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] AudioClip bombSound;
    [Range(0f, 1f)] [SerializeField] float bombVolume = 1f;
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] AudioClip explosionSound;
    [Range(0f, 1f)] [SerializeField] float explosionVolume = 1f;

    float shotCounter;
    float bombCounter;

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        bombCounter = Random.Range(minTimeBetweenBombs, maxTimeBetweenBombs);
	}
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
        CountDownAndBomb();
	}

    private void CountDownAndBomb()
    {
        bombCounter -= Time.deltaTime;
        if (bombCounter <= 0)
        {
            if (bombPrefab != null)
            {
                FireBomb();
            }
            bombCounter = Random.Range(minTimeBetweenBombs, maxTimeBetweenBombs);
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            if (shotPrefab != null)
            {
                FireLaser();
            }
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void FireBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.Inverse(Quaternion.identity)) as GameObject;
    }

    private void FireLaser()
    {
        GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.Inverse(Quaternion.identity)) as GameObject;
        AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotVolume);
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shotSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionVolume);
            GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);
        }
    }

}
