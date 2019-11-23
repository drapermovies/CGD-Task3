using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpObject : MonoBehaviour
{
    [SerializeField] private float bump_range = 0.5f;

    private List<EnemyAI> enemies;

    private float new_bump = 0;

    private void Awake()
    {
        enemies = new List<EnemyAI>();
        //Add all enemies to an array
        foreach(EnemyAI enemy in FindObjectsOfType<EnemyAI>())
        {
            enemies.Add(enemy);
        }
        ChangeDistance();
    }

    private void OnValidate()
    {
        ChangeDistance();
    }

    void Update()
    {
        foreach(EnemyAI enemy in enemies)
        {
            if(Vector3.Distance(enemy.transform.position, this.transform.position) <= new_bump)
            {
                enemy.Bump();
            }
            else
            {
                enemy.has_bumped = false;
            }
        }
    }

    void ChangeDistance()
    {
        float largest_value = 0;
        if(transform.localScale.x > largest_value)
        {
            largest_value = transform.localScale.x;
        }
        if (transform.localScale.y > largest_value)
        {
            largest_value = transform.localScale.y;
        }
        if (transform.localScale.z > largest_value)
        {
            largest_value = transform.localScale.z;
        }

        new_bump = bump_range + largest_value;
    }
}
