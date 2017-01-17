using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour {

	public float stormSpeed = 1f;
	public bool toLeft;

	private float distanceFromCamera = 10f;
	private float direction;
	private Collider2D storm;

	void Start () {
		Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
		direction = transform.position.x - centerPos.x;
		direction /= direction;
		if (toLeft) {
			direction = -direction;
		}
		storm = GetComponent<Collider2D>();
	}
	
	void Update () {
		storm.transform.Translate(direction * stormSpeed / 1000, 0, 0);
	}
}
