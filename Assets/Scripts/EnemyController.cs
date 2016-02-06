using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Enemy's health points
    /// </summary>
    public int hp;

    /// <summary>
    /// List of towers, that targeted this enemy
    /// </summary>
    private List<GameObject> towers = new List<GameObject>();
    #endregion

    #region Behaviour
    void Start()
    {
        var castle = GameObject.Find("Castle");
        if (castle)
            GetComponent<NavMeshAgent>().destination = castle.transform.position;
    }

    void OnTriggerEnter(Collider co)
    {
        if (co.name == "Castle")
        {
            // TODO: deal damage to castle
            Destroy(gameObject);
        }

        var towerControl = co.GetComponent<TowerController>();
        if (towerControl)
        {
            towerControl.AddEnemy(gameObject);
            towers.Add(co.gameObject);
        }
    }

    void OnTriggerExit(Collider co)
    {
        var towerControl = co.GetComponent<TowerController>();
        if (towerControl)
        {
            towerControl.RemoveEnemy(gameObject);
            towers.Remove(co.gameObject);
        }
    }
    #endregion

    #region Methods
    public void TakeDamage(int damage)
    {
        Debug.Log("-" + damage + " hp");
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log("I'm ded");
            BroadcastDeath();
            Destroy(gameObject);
        }
    }

    private void BroadcastDeath()
    {
        foreach (var towerObj in towers)
        {
            var towerControl = towerObj.GetComponent<TowerController>();
            towerControl.RemoveEnemy(gameObject);
        }
        towers.Clear();
    } 
    #endregion
}
