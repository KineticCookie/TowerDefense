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

    // TODO: make a list of placeable towersPrefabs. menu maybe
    void OnMouseUpAsButton()
    {
        if (isFree)
        {
            var gObject = Instantiate(towerPrefab);
            gObject.transform.position = transform.position + Vector3.up;
            isFree = false;
        }
    } 
    #endregion
}
