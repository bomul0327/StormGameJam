using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour {

	public float stormSpeed = 1f;
	public bool toLeft;

	private float distanceFromCamera = 10f;
	private float direction;

	void Start () {
		Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
		direction = transform.position.x - centerPos.x;
		direction /= direction;
		if (toLeft) {
			direction = -direction;
		}
	}
	
	void Update () {
		transform.Translate(direction * stormSpeed * Time.deltaTime, 0, 0);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			if(coll.gameObject.GetComponent<PlayerCtrl>().IsDead == false){
				Animator anim = coll.gameObject.GetComponent(typeof(Animator)) as Animator;
				coll.gameObject.GetComponent<PlayerCtrl>().IsDead = true;
				StartCoroutine(GameManager.Instance.GameOver(coll.transform.GetComponent<PlayerCtrl>().playerIdx == 2 ? 1 : 2));
				anim.SetTrigger("Damage");
				anim.SetBool("IsDead", true);
			}
		}
	}
}
