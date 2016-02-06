using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour {

    #region Fields
    /// <summary>
    /// Enemy prefab to spawn
    /// </summary>
    public GameObject enemyPrefab;

    /// <summary>
    /// Seconds between enemy spawns
    /// </summary>
    public float spawnInterval = 3;
    #endregion

    #region Methods
    /// <summary>
    /// Spawn the wave of enemies. Delegates spawning to Spawn coroutine
    /// </summary>
    /// <param name="waveSize"></param>
    public void SpawnWave(uint waveSize)
    {
        StartCoroutine(Spawn(5));
    }

    /// <summary>
    /// Coroutine, which actually spawns enemies with time interval (<see cref="spawnInterval"/>) between them
    /// </summary>
    /// <param name="waveSize">size of the wave</param>
    /// <returns></returns>
    IEnumerator Spawn(uint waveSize)
    {
        Debug.Log("Entered spawn loop with wave size: " + waveSize);

        for (int i = 0; i < waveSize; i++)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }

        Debug.Log("End of the wave");
    }

    /// <summary>
    /// Spawn a single enemy in the spawner position
    /// </summary>
    void Spawn()
    {
        Debug.Log("Spawning enemy on position: " + transform.position);
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    } 
    #endregion
}
