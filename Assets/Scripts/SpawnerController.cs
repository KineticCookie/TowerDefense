using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour {

    #region Fields
    /// <summary>
    /// Enemy prefab to spawn
    /// </summary>
    public GameObject enemyPrefab;

    #endregion

    #region Methods
    /// <summary>
    /// Spawn a single enemy in the spawner position
    /// </summary>
    public GameObject Spawn()
    {
        return (GameObject)Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
    #endregion

}
