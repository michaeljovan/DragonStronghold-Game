using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpPoint : MonoBehaviour
{
    public float HealthPoint;
    public static float startHP;
    EnemySpawner enemyPrefab;
    void Start()
    {
        HealthPoint = 10;
        startHP = HealthPoint;
    }

    void Update()
    {
        
    }

}
