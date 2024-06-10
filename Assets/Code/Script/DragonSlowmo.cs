using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DragonSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private Transform dragonRotationPoint;
    [SerializeField] private float aps = 4f; //bullet per second (speed)  
    [SerializeField] private float freezeTime = 1f; //bullet per second (speed)   
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float rotationSpeed = 5f;
    private Transform target;
    [SerializeField] private Transform firingPoint;

    private float timeUntilFire;

    private void Update()
    {
        timeUntilFire += Time.deltaTime; 
        
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                Shoot();
                FreezeEnemies();
                timeUntilFire = 0f;
            }
        }

    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        dragonRotationPoint.rotation = Quaternion.RotateTowards(dragonRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask); 

        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];  

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f); 

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    } 

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }



    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefabs, firingPoint.position, Quaternion.identity);
        Fire fireScript = bulletObj.GetComponent<Fire>();
        fireScript.SetTarget(target);
    }
} 


