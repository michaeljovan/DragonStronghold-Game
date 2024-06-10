using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerHp playerHp;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
        playerHp = FindObjectOfType<PlayerHp>();
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                if (playerHp != null)
                {
                    PlayerHpPoint.startHP -= 2;
                    playerHp.PlayerBar(PlayerHpPoint.startHP); 

                    if (PlayerHpPoint.startHP <= 0)
                    {
                        LevelManager.main.GameOver();
                        Time.timeScale = 0; 
                    }
                }
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized; 

        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    } 

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

}
