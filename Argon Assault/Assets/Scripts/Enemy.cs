using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] int scoreAmount = 15;
    [SerializeField] private int lifes = 6;
    [SerializeField] private GameObject hitVFX;
    private ScoreBoard scoreBoard;
    private bool isDead = false;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        lifes--;
        if (lifes >= 0)
        {
            Instantiate(hitVFX, gameObject.transform.position, Quaternion.identity);
            scoreBoard.IncreaseScore(scoreAmount / 3);
            print("only hit");
        }
        else
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            scoreBoard.IncreaseScore(scoreAmount);

            print("dead");
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Instantiate(deathVFX, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
