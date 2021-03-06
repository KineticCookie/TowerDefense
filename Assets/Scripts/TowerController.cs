﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Bullets for turret
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Pause between shots. Measured in seconds
    /// </summary>
    public float shotsPause = 1;

    /// <summary>
    /// Tower's price
    /// </summary>
    public float price;

    /// <summary>
    /// Queue of spotted targets. Tower will fire to the first enemy in queue.
    /// </summary>
    private List<GameObject> targets = new List<GameObject>();
    #endregion

    #region Behaviour
    void Start()
    {
        InvokeRepeating("SearchAndDestroy", 0, shotsPause);
    }

    /// <summary>
    /// When collided, add enemy on the tracking list
    /// </summary>
    /// <param name="co"></param>
    void OnTriggerEnter(Collider co)
    {
        var enemyController = co.GetComponent<EnemyController>();
        if(enemyController)
        {
            Debug.Log("I see the enemy");
            enemyController.Death += OnEnemyDeath;
            targets.Add(co.gameObject);
        }
    }

    /// <summary>
    /// When collider quit, remove enemy off the tracking list
    /// </summary>
    /// <param name="co"></param>
    void OnTriggerExit(Collider co)
    {
        var enemyController = co.GetComponent<EnemyController>();
        if (enemyController)
        {
            Debug.Log("I lost the enemy");
            enemyController.Death -= OnEnemyDeath;
            targets.Remove(co.gameObject);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// If has enemies on the tracklist, attack first.
    /// </summary>
    private void SearchAndDestroy()
    {
        targets.RemoveAll(x => x == null);
        if (targets.Count != 0) // if targets on the list
        {
            // shoot the bullet
            Debug.Log("Shooting!");
            var currentTarget = targets[0];
            if (currentTarget)
            {
                var bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<BulletsController>().target = currentTarget.transform;
            }
        }
    }

    /// <summary>
    /// Remove enemy from the tracklist when he is dead
    /// </summary>
    /// <param name="enemy"></param>
    public void OnEnemyDeath(GameObject enemy)
    {
        if (enemy.GetComponent<EnemyController>())
        {
            Debug.Log("Enemy dead");
            targets.Remove(enemy);
        }
    } 
    #endregion
}
