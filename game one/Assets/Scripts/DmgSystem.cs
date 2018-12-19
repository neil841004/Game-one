using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgSystem : MonoBehaviour {

	// Use this for initialization
	private void OnTriggerEnter(Collider co) {
		if (co.gameObject.tag == "Monster")
		{
			co.gameObject.SendMessage("Back") ;
		}
	}
	
}
