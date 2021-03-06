﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour
{
    [Header("Boss Stats")]
    [SerializeField] float health = 5000;
    [SerializeField] int scoreVaule = 1500;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] int numberOfProjectiles = 20;
    [SerializeField] int numberOfProjectileWaves = 1;
    [SerializeField] float shootAngle = 10f;
    [SerializeField] float timeBetweenShootingWaves = 0.5f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    string sceneName = "";

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            StartCoroutine(BossShots(numberOfProjectiles, numberOfProjectileWaves));
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    IEnumerator BossShots(int numberOfProjectiles, int waves)
    {
        for (int i = 0; i < waves; i++)
        {
            Fire(numberOfProjectiles, i * shootAngle);
            yield return new WaitForSeconds(timeBetweenShootingWaves);
        }
    }

    private void Fire(int numberOfProjectiles, float angle)
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float shootAngle = i * (360f / numberOfProjectiles);
            Quaternion rot = Quaternion.AngleAxis(shootAngle + angle, Vector3.forward);
            GameObject laser = Instantiate(projectile, transform.position, rot) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = laser.transform.up * projectileSpeed;
        }
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreVaule);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        sceneName = SceneManager.GetActiveScene().name;
        FindObjectOfType<Level>().LoadWinGame(sceneName);
    }
}
