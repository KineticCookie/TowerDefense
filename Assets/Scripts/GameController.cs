using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    #region Behaviour
    /// <summary>
    /// Game start logic
    /// </summary>
    void Start()
    {
        var obj = GameObject.Find("Spawner");
        var con = obj.GetComponent<SpawnerController>();
        con.SpawnWave(1);
    } 
    #endregion
}
