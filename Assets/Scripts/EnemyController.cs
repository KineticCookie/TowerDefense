using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : Damageable
{
    #region Fields
    /// <summary>
    /// How much damage does enemy to castle
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Reward for enemy's death
    /// </summary>
    public int reward = 100;
    #endregion

    #region Behaviour
    void Start()
    {
        var castle = GameObject.Find("Castle");
        if (castle)
            GetComponent<NavMeshAgent>().destination = castle.transform.position;
        Death += GetReward;
    }

    private void GetReward(GameObject obj)
    {
        var gameController = Camera.main.GetComponent<GameController>();
        gameController.AddMoney(reward);
    }

    void OnTriggerEnter(Collider co)
    {
        var castle = co.GetComponent<CastleController>();
        if (castle)
        {
            castle.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    #endregion
}
