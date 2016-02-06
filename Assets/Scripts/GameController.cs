using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    //TODO: shit ton of logic and fields. Refactor
    #region Fields
    /// <summary>
    /// Amount of money, player has got
    /// </summary>
    public float money;

    /// <summary>
    /// List of enemy quantity per wave.
    /// </summary>
    public List<uint> wavesConfig;

    public float spawnInterval = 3;

    private int currentWave = 0;

    /// <summary>
    /// UI Text object for money output
    /// </summary>
    private GameObject uiMoneyText;

    /// <summary>
    /// UI Text object for health output
    /// </summary>
    private GameObject uiHealthText;

    /// <summary>
    /// UI Text object for waves output
    /// </summary>
    private GameObject uiWaveText;

    private GameObject spawner;
    private SpawnerController spawnerController;

    /// <summary>
    /// List of spawned enemies on current wave.
    /// </summary>
    private List<GameObject> enemies = new List<GameObject>();

    private GameObject castle;
    private CastleController castleController;
    #endregion

    #region Behaviour
    /// <summary>
    /// Game start logic
    /// </summary>
    void Start()
    {
        if (wavesConfig.Count == 0)
            wavesConfig = new List<uint>
            {
                1,2,3,4,5
            };

        castle = GameObject.Find("Castle");
        castleController = castle.GetComponent<CastleController>();
        castleController.Death += GameOver;

        spawner = GameObject.Find("Spawner");
        spawnerController = spawner.GetComponent<SpawnerController>();

        uiMoneyText = GameObject.Find("MoneyText");
        uiHealthText = GameObject.Find("HealthText");
        uiWaveText = GameObject.Find("WaveText");
    }

    void Update()
    {
        UpdateUI();

        if (enemies.Count == 0) // If wave is ended
        {
            if (currentWave < wavesConfig.Count) // And there is more waves
            {
                StartCoroutine(SpawnWave(wavesConfig[currentWave])); // spawn more enemies
                currentWave++;
            }
            else // And there is no more waves left
            {
                SceneManager.LoadScene("Winner"); // WIN
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
    /// Update UI information
    /// </summary>
    private void UpdateUI()
    {
        uiHealthText.GetComponent<Text>().text = string.Format(Constants.HealthText_1, castleController.hp);
        uiMoneyText.GetComponent<Text>().text = string.Format(Constants.MoneyText_1, money);
        uiWaveText.GetComponent<Text>().text = string.Format(Constants.WaveText_2, currentWave, wavesConfig.Count);
    }

    /// <summary>
    /// Adds money to bank
    /// </summary>
    /// <param name="quantity">how much</param>
    public void AddMoney(float quantity) //TODO: srsly?
    {
        money += quantity;
    }

    /// <summary>
    /// Removes money from bank
    /// </summary>
    /// <param name="quantity">how much</param>
    public void RemoveMoney(float quantity) //TODO: srsly?
    {
        money -= quantity;
    }

    /// <summary>
    /// Game over event subscriber. Shot when castle is ded
    /// </summary>
    /// <param name="obj"></param>
    private void GameOver(GameObject obj)
    {
        SceneManager.LoadScene("GameOver");
        Debug.Log("Castle has been destroyed");
    } 
    #endregion
}
