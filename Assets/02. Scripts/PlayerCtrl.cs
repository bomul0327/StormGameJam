﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

#region Public Variables
	public float moveSpeed;
	public float jumpSpeed = 3;
#endregion

#region Private Serialized Variables
	//Check whether is on ground or not
	[SerializeField]
	private LayerMask groundLayer;
#endregion

#region Private Variables
	private bool ignoreCrouch;
	private bool isGround;
	private float horInput;
	private float verInput;

	private Vector2 moveDir;

	//For flipping sprite 
	private float scale;

	//Initialize Components
	private Animator anim;
	private Rigidbody2D rb2d;
	private Collider2D col2d;
#endregion

	void Awake(){
		scale = transform.localScale.x;
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		col2d = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update () {
		horInput = Input.GetAxis("Horizontal");
		verInput = Input.GetAxis("Vertical");

		moveDir = new Vector2(horInput, 0);
		
		FlipCharacter();
		Jump();
		Attack();
		if (!ignoreCrouch) {
			Crouch();
		}

		rb2d.velocity = moveDir * moveSpeed + new Vector2(0, rb2d.velocity.y);
		
		UpdateAnimation();
	}

	void FixedUpdate(){
		CheckGround();
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
		if(Input.GetButtonDown("Jump") && isGround){
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
			isGround = false;
			ignoreCrouch = true;
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
		if(verInput < 0 && isGround){
			anim.SetBool("IsCrouch", true);
		}
		else{
			anim.SetBool("IsCrouch", false);
		}
	}

	void GetDamage(){
		anim.SetTrigger("Damage");
	}

	void CheckGround(){
		Vector3 origin = transform.position + (Vector3)col2d.offset - new Vector3(0, col2d.bounds.size.y * 0.5f, 0);
		Debug.DrawRay(transform.position + (Vector3)col2d.offset, Vector3.down, Color.red, col2d.bounds.size.y * 0.5f);
		if(Physics2D.Raycast(transform.position, Vector2.down, col2d.bounds.size.y * 0.6f, groundLayer)){
			isGround = true;
			ignoreCrouch = false;
			anim.SetFloat("GroundDistance", 0);
		}
		else{
			isGround = false;
			anim.SetFloat("GroundDistance", 99);
		}
	}
}
