using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

	public GameObject targetzone;
	GameObject targetenemy;

	NavMeshAgent nav;
	float attacktime = 0;
	float attackdelay = 1f;

	public GameObject spark;

	void OnEnable(){
		ComponentManager<BasicAI>.instance.Register (gameObject, this);
	}

	void OnDisable(){
		ComponentManager<BasicAI>.instance.UnRegister (gameObject);
	}



	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
	}

	// Update is called once per frame
	void Update () {

		if (targetenemy == null) {
			nav.destination = targetzone.transform.position;
		} else {
			Vector3 delta = transform.position - targetenemy.transform.position;
			delta.y = 0;
			delta.Normalize();
			nav.destination = targetenemy.transform.position + delta;
			if (Vector3.Distance(transform.position,targetenemy.transform.position) <= 1.5f){
				if(Time.time > attacktime){
					ComponentManager<Stats>.instance.Get(targetenemy).Damage(4);
					Destroy(Instantiate(spark,targetenemy.transform.position,Quaternion.identity),4);
					attacktime = Time.time + attackdelay;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Character") && FactionScript.AreEnemies (gameObject, other.gameObject)) {
			targetenemy = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == targetenemy)
			targetenemy = null;
	}
}
