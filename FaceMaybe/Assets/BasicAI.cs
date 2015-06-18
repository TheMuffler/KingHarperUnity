using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicAI : MonoBehaviour {

	public GameObject targetzone;
	GameObject targetenemy;
	List<GameObject> enemies = new List<GameObject> ();

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
		HandleEnemies ();

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

	void HandleEnemies(){
		GameObject toReturn = null;
		float dist = Mathf.Infinity;
		float temp = 0;

		for (int i = 0; i < enemies.Count;) {
			if(enemies[i] == null){
				enemies.RemoveAt(i);
				continue;
			}
		
			temp = Vector3.SqrMagnitude(enemies[i].transform.position-transform.position);
			if(temp < dist){
				toReturn = enemies[i];
				dist = temp;
			}
			++i;
		}

		targetenemy = toReturn;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Character") && FactionScript.AreEnemies (gameObject, other.gameObject)) {
			enemies.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag("Character") && FactionScript.AreEnemies (gameObject, other.gameObject)) {
			enemies.Remove(other.gameObject);
		}
	}
}
