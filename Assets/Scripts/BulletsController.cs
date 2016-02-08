using UnityEngine;
using System.Collections;

public class BulletsController : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Bullet's target
    /// </summary>
    public Transform target;

    /// <summary>
    /// Bullet's speed
    /// </summary>
    public float speed = 10;

    /// <summary>
    /// Bullet's damage
    /// </summary>
    public int damage = 1;
    #endregion

    #region Behaviour
    /// <summary>
    /// Update with physics calculations
    /// </summary>
    void FixedUpdate()
    {
        // If bullet has a target
        if (target)
        { 
            // Fly towards it
            var dir = target.position - transform.position;
            GetComponent<Rigidbody>().velocity = dir.normalized * speed;
            // Rotate towards it
            transform.LookAt(target.transform);
        }
        else
        { // Destroy self
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// When reaching the enemy
    /// </summary>
    /// <param name="co">enemy collider</param>
    void OnTriggerEnter(Collider co)
    {
        Debug.Log("Gotcha");
        var enemy = co.gameObject.GetComponent<EnemyController>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    } 
    #endregion
}
