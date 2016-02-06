using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacementController : MonoBehaviour {

    #region Fields
    /// <summary>
    /// Tower prefab to place
    /// </summary>
    public GameObject towerPrefab;

    /// <summary>
    /// Is this place free to place the tower?
    /// </summary>
    private bool isFree;
    #endregion

    #region Behaviour
    void Start()
    {
        isFree = true;
    }

    // TODO: make a list of placeable towersPrefabs maybe
    void OnMouseUpAsButton()
    {
        if (isFree)
        {
            var gameController = Camera.main.GetComponent<GameController>();
            var towerController = towerPrefab.GetComponent<TowerController>();
            if (gameController.money >= towerController.price)
            {
                var gObject = Instantiate(towerPrefab);
                gObject.transform.position = transform.position + Vector3.up;
                isFree = false;
                gameController.RemoveMoney(towerController.price);
            }
        }
    } 
    #endregion
}
