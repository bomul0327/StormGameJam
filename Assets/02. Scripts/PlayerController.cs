using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int moveSpeed;
	public int rotateSpeed;

	void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		// var rotate = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
		// transform.Rotate(0, rotate, 0);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		if (hit.collider.gameObject.tag == "Terrain") {
			Vector3 target = hit.point;
			transform.LookAt(target);
		}

		transform.Translate(x, 0, z, Space.World);
	}
}
