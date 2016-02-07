using UnityEngine;
using System.Collections;
using System;

public class Damageable : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Initial amount of health points
    /// </summary>
    public int healthStart;

    /// <summary>
    /// current health points
    /// </summary>
    public int healthCurrent { get; private set; }

    /// <summary>
    /// Event is fired when <see cref="Damageable"/> is dead
    /// </summary>
    public event Action<GameObject> Death;
    #endregion

    #region Behaviour
    void Awake()
    {
        healthCurrent = healthStart;
        OnAwake();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Removes health points from this enemy. If no <see cref="healthCurrent"/> left, destroys enemy
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        Debug.Log(string.Format("-{0} hp", damage));
        healthCurrent -= damage;
        if (healthCurrent <= 0)
        {
            KillSelf();
        }
    }

    public void KillSelf()
    {
        BroadcastDeath();
        Death = null;
        Destroy(gameObject);
    }

    /// <summary>
    /// Broadcasts death message to subscribers.
    /// </summary>
    private void BroadcastDeath()
    {
        if (Death != null)
            Death(gameObject);
    }

    /// <summary>
    /// Override this to use Unity Awake method.
    /// </summary>
    protected virtual void OnAwake() { } 
    #endregion
}
