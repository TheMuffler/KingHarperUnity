using UnityEngine;
using System.Collections;

public class CManagerTest : MonoBehaviour {


	void OnEnable(){
		ComponentManager<CManagerTest>.instance.Register (gameObject, this);
	}
	
	void OnDisable(){
		ComponentManager<CManagerTest>.instance.UnRegister (gameObject);
	}


	public void doit(){
		transform.Translate (Vector3.up * 10);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
