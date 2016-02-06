using UnityEngine;
using System.Collections;
using System;

public class Damageable : MonoBehaviour
{
    /// <summary>
    /// health points
    /// </summary>
    public int hp;

    /// <summary>
    /// Event is fired when <see cref="Damageable"/> is dead
    /// </summary>
    public event Action<GameObject> Death;

    /// <summary>
    /// Removes health points from this enemy. If no <see cref="hp"/> left, destroys enemy
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        Debug.Log("-" + damage + " hp");
        hp -= damage;
        if (hp <= 0)
        {
            BroadcastDeath();
            Death = null;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Broadcasts death message to subscribers.
    /// </summary>
    private void BroadcastDeath()
    {
        if (Death != null)
            Death(gameObject);
    }
}
