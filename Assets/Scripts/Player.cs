using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    //CONGIFGURATION PARAMETERS --------------------------------------------------

    [Header("Stats")]
    [SerializeField] int health = 1;

    [Header("Movement")]
    [SerializeField] float yIncrement = 5f;
    [SerializeField] float moveSpeed = 100;

    [Header("Effects")]
    [SerializeField] GameObject moveEffect = null;
    [SerializeField] AudioClip[] soundEffects;

    [Header("Points")]
    [SerializeField] int points = 0;
    [SerializeField] int pointsPerSecond = 10;

    //STORED VALUES ---------------------------------------------------------------

    private Shake shake;
    private Vector2 targetPos;
    private GameObject pointsText;
    private float addPointsDelay = 2f;
    private int vertical = 0;
    private bool isAxisInUse = false;

    //Initialise Player and Parameters
    void Start()
    {
        shake = FindObjectOfType<Shake>();
        GameManager.Instance.UpdateCurrentScore(0);
    }

    // Update Score and move player;
    void Update()
    {
        MovePlayer();
        UpdateScore();

        if (health <= 0)
        {
            Die();
        }
    }

    //add points to score every second, change ui text, update current game score
    private void UpdateScore()
    {
        if (addPointsDelay <= 0)
        {
            AddPoints(pointsPerSecond);
            GameCanvas.Instance.UpdatePointsText(points);
            GameManager.Instance.UpdateCurrentScore(points);
            addPointsDelay = 1f;
        }
        else
        {
            addPointsDelay -= 1f * Time.deltaTime;
        }
    }

    //create death particles, wait until they are gone, destroy the player and restart the game
    private void Die()
    {
        GameOverMenu gameOverMenu = FindObjectOfType<GameOverMenu>();
        shake.CamShake();
        StartCoroutine(CreateParticles());
        GameManager.Instance.SetGameOver(true);
        gameOverMenu.SetGameOverStats();
        gameOverMenu.DisplayGameOverScreen();
        Destroy(gameObject);
    }

    //MOBILE BUTTON FUNCTIONS

    public void SetVertical(int direction)
    {
        vertical = direction;
    }

    //use the up and down arrow keys to move the player, shake cam on movement, create move particles
    private void MovePlayer()
    {

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR || UNITY_WEBGL


        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (isAxisInUse == false)
            {
                vertical = (int)Input.GetAxisRaw("Vertical");
                isAxisInUse = true;
            }
        }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            isAxisInUse = false;
        }

#endif

        if (vertical == 1 && transform.position.y < yIncrement)
        {
            vertical = 0;
            targetPos = new Vector2(transform.position.x, transform.position.y + yIncrement);
            shake.CamShake();
            StartCoroutine(CreateParticles());
        }
        else if (vertical == -1 && transform.position.y > -yIncrement)
        {
            vertical = 0;
            targetPos = new Vector2(transform.position.x, transform.position.y - yIncrement);
            shake.CamShake();
            StartCoroutine(CreateParticles());
        }

        //for smooth movement to position
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    //update health upon taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    //add points
    public void AddPoints(int p)
    {
        points += p;
    }

    //Instantiate particles for player
    private IEnumerator CreateParticles()
    {
        AudioSource.PlayClipAtPoint(soundEffects[Random.Range(0, soundEffects.Length)], Camera.main.transform.position);
        GameObject particles = Instantiate(moveEffect, transform.position, Quaternion.identity);
        float particleDuration = particles.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        yield return new WaitForSeconds(particleDuration);
        Destroy(particles);
    }
}
