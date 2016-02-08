using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Start amount of money, player has got
    /// </summary>
    public float moneyStart;

    /// <summary>
    /// Current amoun of money
    /// </summary>
    public float moneyCurrent { get; private set; }

    /// <summary>
    /// List of enemy quantity per wave.
    /// </summary>
    public List<uint> wavesConfig;

    /// <summary>
    /// Interval between spawning enemies
    /// </summary>
    public float spawnInterval = 3;

    /// <summary>
    /// Current wave
    /// </summary>
    public int waveCurrent { get; private set; }

    /// <summary>
    /// Amount of waves
    /// </summary>
    public int waveCount { get; private set; }

    /// <summary>
    /// Spawner's controller for actually spawning
    /// </summary>
    private SpawnerController spawnerController;

    /// <summary>
    /// List of spawned enemies on current wave.
    /// </summary>
    private List<GameObject> enemies = new List<GameObject>(); // TODO: more customisation
    #endregion

    #region Behaviour
    /// <summary>
    /// Game start logic
    /// </summary>
    void Start()
    {
        waveCurrent = 0;
        if (wavesConfig.Count == 0)
            wavesConfig = new List<uint>
            {
                1,2,3,4,5
            };
        waveCount = wavesConfig.Count;

        moneyCurrent = moneyStart;

        var castle = GameObject.Find(Constants.GameObjects.Castle);
        var castleController = castle.GetComponent<CastleController>();
        castleController.Death += GameOver; // Subscribe on castle death.

        var spawner = GameObject.Find(Constants.GameObjects.Spawner);
        spawnerController = spawner.GetComponent<SpawnerController>();
    }

    void Update()
    {
        if (enemies.Count == 0) // If wave is ended
        {
            if (waveCurrent < waveCount) // And there is more waves
            {
                StartCoroutine(SpawnWave(wavesConfig[waveCurrent])); // spawn more enemies
                waveCurrent++;
            }
            else // And there is no more waves left
            {
                SceneManager.LoadScene(Constants.Scenes.Win); // WIN
                Debug.Log("You survived");
            }
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Coroutine, which actually spawns enemies with time interval (<see cref="spawnInterval"/>) between them
    /// </summary>
    /// <param name="waveSize">size of the wave</param>
    /// <returns></returns>
    private IEnumerator SpawnWave(uint waveSize)
    {
        Debug.Log("Entered spawn loop with wave size: " + waveSize);
        for (int i = 0; i < waveSize; i++)
        {
            Debug.Log(string.Format("Spawn enemy {0}/{1}", i + 1, waveSize));
            var enemy = spawnerController.Spawn();
            enemies.Add(enemy);
            enemy.GetComponent<EnemyController>().Death += (obj) => enemies.Remove(obj);
            yield return new WaitForSeconds(spawnInterval);
        }
        Debug.Log("End of the wave");
    }

    /// <summary>
    /// Adds money to bank
    /// </summary>
    /// <param name="quantity">how much</param>
    public void AddMoney(float quantity)
    {
        moneyCurrent += quantity;
    }

    /// <summary>
    /// Removes money from bank
    /// </summary>
    /// <param name="quantity">how much</param>
    public void RemoveMoney(float quantity)
    {
        moneyCurrent -= quantity;
    }

    /// <summary>
    /// Game over event subscriber. Shot when castle is ded
    /// </summary>
    /// <param name="obj"></param>
    private void GameOver(GameObject obj)
    {
        SceneManager.LoadScene(Constants.Scenes.GameOver);
        Debug.Log("Castle has been destroyed");
    } 
    #endregion
}
