using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacementController : MonoBehaviour {

    public GameObject towerPrefab;

    private bool isFree;

	// Use this for initialization
	void Start () {
        isFree = true;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseUpAsButton() {
        if (isFree)
        {
            var gObject = Instantiate(towerPrefab);
            gObject.transform.position = transform.position + Vector3.up;
            isFree = false;
        }
    }
}
