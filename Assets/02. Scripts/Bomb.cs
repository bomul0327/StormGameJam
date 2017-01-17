using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float explosionDelay = 3f;
	public float explosionRate = 1f;
	public float explosionMaxSize;
	public float explosionSpeed = 1f;
	public float currentRadius = 0f;

	private bool isExploded = false;
	private CircleCollider2D explosionRadius;

	void Start () {
		explosionRadius = GetComponent<CircleCollider2D>();
	}
	
	void Update () {
		explosionDelay -= Time.deltaTime;
		if (explosionDelay < 0) {
			isExploded = true;
		}
	}

	void FixedUpdate () {
		if (isExploded) {
			if (currentRadius < explosionMaxSize) {
				currentRadius += explosionRate;
			} else {
				Destroy(gameObject.transform.parent.gameObject);
			}
			explosionRadius.radius = currentRadius;
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (isExploded) {
			Rigidbody2D rbPlayer = coll.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
			if (rbPlayer != null) {
				Vector2 target = coll.gameObject.transform.position;
				Vector2 bomb = gameObject.transform.position;

				Vector2 direction = 8f * (target - bomb).normalized;

				rbPlayer.velocity += direction;
			}
		}
	}
}
