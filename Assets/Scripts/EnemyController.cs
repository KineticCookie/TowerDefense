using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int hp;
    public float speed;

	// Use this for initialization
	void Start () {
        var castle = GameObject.Find("Castle");
        if (castle)
            GetComponent<NavMeshAgent>().destination = castle.transform.position;
	}

    void OnTriggerEnter(Collider co) {
        if (co.name == "Castle") {
            // TODO: deal damage to castle
            Destroy(gameObject);
        }
    }
}
