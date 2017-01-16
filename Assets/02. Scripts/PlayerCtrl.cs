using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed = 3;

	private float horInput;
	private float verInput;

	private Vector2 moveDir;

	//For flipping sprite 
	private float scale;

	//Initialize Components
	private Animator anim;
	private Rigidbody2D rb2d;
	
	void Awake(){
		scale = transform.localScale.x;
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		horInput = Input.GetAxis("Horizontal");
		verInput = Input.GetAxis("Vertical");

		moveDir = new Vector2(horInput, 0);
		
		FlipCharacter();
		Attack();
		Jump();
		Crouch();

		rb2d.velocity = moveDir * moveSpeed + new Vector2(0, rb2d.velocity.y);
		
		UpdateAnimation();
	}

	//Update Animation
	void UpdateAnimation(){

		anim.SetFloat("Speed", Mathf.Abs(horInput));
		anim.SetFloat("FallSpeed", rb2d.velocity.y);

	}

	void FlipCharacter(){
		if(horInput < 0){
			transform.localScale = new Vector3(-scale, scale, 0);
		}
		else if(horInput > 0){
			transform.localScale = new Vector3(scale, scale, 0);
		}
	}

	void Jump(){
		if(Input.GetButtonDown("Jump")){
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
		}
	}

	void Attack(){
		if(Input.GetButtonDown("Fire1")){
			anim.SetTrigger("Attack1");
		}
		if(Input.GetButtonDown("Fire2")){
			anim.SetTrigger("Attack2");
		}
		if(Input.GetButtonDown("Fire3")){
			anim.SetTrigger("Attack3");
		}
	}

	void Crouch(){
		if(verInput < 0){
			anim.SetBool("IsCrouch", true);
		}
		else{
			anim.SetBool("IsCrouch", false);
		}
	}

	void GetDamage(){
		anim.SetTrigger("Damage");
	}
}
