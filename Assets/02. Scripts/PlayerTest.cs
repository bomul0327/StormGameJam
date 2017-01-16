using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerTest : NetworkBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
			return;

		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(hor, 0, ver) * 10f * Time.deltaTime);
	}
}
