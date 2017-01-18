using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float time;
	public float force;
	public GameObject explosionEffect;

	private float dirFactor = 1;
	private Rigidbody2D rb2d;

	void Awake(){
		rb2d = GetComponent<Rigidbody2D>();
	}
	void Start(){
		dirFactor = rb2d.velocity.x / Mathf.Abs(rb2d.velocity.x);
		StartCoroutine(DestroyInTime(time));
	}
	void OnCollisionEnter2D(Collision2D coll){
		if(coll.transform.CompareTag("Player")){
			Rigidbody2D collRb2d = coll.transform.GetComponent<Rigidbody2D>();
			collRb2d.AddForce(force * Vector2.right * dirFactor);
		}
		Quaternion effectRot = Quaternion.identity;
		effectRot.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
		GameObject explosionEffectInstance = Instantiate(explosionEffect, transform.position, effectRot);
		Destroy(explosionEffectInstance, 1f);
		Destroy(this.gameObject);
	}

	IEnumerator DestroyInTime(float sec){
		yield return new WaitForSeconds(sec);

		Destroy(this.gameObject);
	}
}
