using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] int damage = 1;
    [SerializeField] float speed = 5;

    [SerializeField] GameObject effect = null;

    private void Update()
    {
        if (transform.position.x < -Camera.main.orthographicSize * Camera.main.aspect - 2.5)
        {
            Destroy(this.gameObject);
        }
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
