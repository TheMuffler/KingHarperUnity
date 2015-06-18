using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	void OnEnable(){
		ComponentManager<Stats>.instance.Register (gameObject, this);
	}
	
	void OnDisable(){
		ComponentManager<Stats>.instance.UnRegister (gameObject);
	}


	private float _hp;
	public float hp{
		get{
			return _hp;
		}
		private set{
			_hp = value;
		}
	}

	// Use this for initialization
	void Start () {
		hp = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(float val){
		hp -= val;
		if (hp <= 0)
			Destroy (gameObject);
	}
}
