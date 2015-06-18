using UnityEngine;
using System.Collections;

public class Navig : MonoBehaviour {

	NavMeshAgent nav;
	GameObject target;
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		target = GameObject.FindWithTag ("Player");
		//ComponentManager<CManagerTest>.instance.Get(target).doit();
	}
	
	// Update is called once per frame
	void Update () {
		nav.SetDestination (target.transform.position);

	}
}
