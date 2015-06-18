using UnityEngine;
using System.Collections;

public class FactionScript : MonoBehaviour {

	void OnEnable(){
		ComponentManager<FactionScript>.instance.Register (gameObject, this);
	}
	
	void OnDisable(){
		ComponentManager<FactionScript>.instance.UnRegister (gameObject);
	}

	public enum Faction{PLAYER_1,PLAYER_2,NEUTRAL, NONE};
	public Faction faction = Faction.NONE;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isEnemy(GameObject other){
		FactionScript fs = ComponentManager<FactionScript>.instance.Get (other);
		if (fs == null)
			return false;
		return this.faction != Faction.NONE && fs.faction != Faction.NONE && fs.faction != this.faction;
	}

	public bool isEnemy(Faction f){
		return this.faction != Faction.NONE && f != Faction.NONE && this.faction != f;
	}

	public static bool AreEnemies(GameObject a, GameObject b){
		FactionScript fs = ComponentManager<FactionScript>.instance.Get (a);
		if (fs == null)
			return false;
		return fs.isEnemy(b);
	}

	public static bool AreEnemies(GameObject a, Faction f){
		FactionScript fs = ComponentManager<FactionScript>.instance.Get (a);
		if (fs == null)
			return false;
		return fs.isEnemy(f);
	}
}
