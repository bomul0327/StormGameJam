﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

#region Public Variables
	public bool IsDead{
		get{
			return isDead;
		}
		set{
			isDead = value;
		}
	}
	public float moveSpeed;
	public float jumpSpeed = 3;
	public float bulletSpeed;
	public float bombSpeed;
	public GameObject bullet;
	public GameObject bomb;
#endregion

#region Private Serialized Variables
	//Check whether is on ground or not
	[SerializeField]
	private LayerMask groundLayer;

	[SerializeField]
	private Transform LeftCheckGroundTransform;
	[SerializeField]
	private Transform RightCheckGroundTransform;

	[SerializeField]
	private Transform UpCheckFrontTransform;
	[SerializeField]
	private Transform MiddleCheckFrontTransform;
	[SerializeField]
	private Transform DownCheckFrontTransform;
#endregion

#region Private Variables
	private bool ignoreCrouch;
	private bool isGround;
	private bool isFrontClear;
	private bool isDead;
	private bool specialAtk;
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
		Debug.Log(col2d.bounds.size.x);
	}

	// Update is called once per frame
	void Update () {
		if(isDead)
		return;

		horInput = Input.GetAxis("Horizontal");
		verInput = Input.GetAxis("Vertical");

		CheckGround();
		CheckFrontClear();

		FlipCharacter();
		if (!specialAtk) {
			Jump();
		}
		Attack();
		if (!ignoreCrouch) {
			Crouch();
		}
		if(!isFrontClear){
			horInput = 0;

		}

		moveDir = new Vector2(horInput, 0);
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
			specialAtk = true;
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
		bool checkGround1 = Physics2D.Raycast(LeftCheckGroundTransform.position, Vector2.down, 0.1f, groundLayer);
		bool checkGround2 = Physics2D.Raycast(RightCheckGroundTransform.position, Vector2.down, 0.1f, groundLayer);

		if(checkGround1 || checkGround2){
			isGround = true;
			ignoreCrouch = false;
			anim.SetFloat("GroundDistance", 0);
		}
		else{
			isGround = false;
			anim.SetFloat("GroundDistance", 99);
			anim.ResetTrigger("Attack3");
		}
	}

	void CheckFrontClear(){
		bool checkFront1 = Physics2D.Raycast(UpCheckFrontTransform.position, Vector2.right * transform.localScale.x / Mathf.Abs(scale), 0.1f, groundLayer);
		bool checkFront2 = Physics2D.Raycast(MiddleCheckFrontTransform.position, Vector2.right * transform.localScale.x / Mathf.Abs(scale), 0.1f, groundLayer);
		bool checkFront3 = Physics2D.Raycast(DownCheckFrontTransform.position, Vector2.right * transform.localScale.x / Mathf.Abs(scale), 0.1f, groundLayer);
		

		if(checkFront1 || checkFront2 || checkFront3){
			isFrontClear = false;
		}
		else{
			isFrontClear = true;
		}

	}

	void resetSpecialAtk() {
		specialAtk = false;
	}

	void BasicAttack() {
		GameObject gobullet = Instantiate<GameObject>(bullet, transform.position, Quaternion.identity);
		Rigidbody2D rbbullet = gobullet.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
		if (transform.localScale.x * bulletSpeed < 0) {
			bulletSpeed = -bulletSpeed;
		}
		rbbullet.velocity = new Vector2(bulletSpeed, 0);
	}

	void ThrowBomb() {
		GameObject goBomb = Instantiate<GameObject>(bomb, transform.position, Quaternion.identity);
		Rigidbody2D rbBomb = goBomb.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
		float bombY = bombSpeed < 0 ? -bombSpeed : bombSpeed;
		if (transform.localScale.x * bombSpeed < 0) {
			bombSpeed = -bombSpeed;
		}
		rbBomb.velocity = (new Vector2(bombSpeed, bombY)).normalized * bombY;
	}

}
