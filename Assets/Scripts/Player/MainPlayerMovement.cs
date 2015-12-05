﻿using UnityEngine;
using System.Collections;

public class MainPlayerMovement : MonoBehaviour {

	public float TranslateSpeed = 6.0f;
	public float RotateSpeed = 5.0f;

	float JumpDistance;


	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;

	void Awake(){
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent<Rigidbody>();
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() //Called and fires every physics update
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		


		Move (h, v);
		Animating (h, v);


		if (h != 0.0f || v != 0.0f) 
		{
			Turn (h, v);
		}
		
	}

	void Move(float h, float v)
	{
		//Jumping
		if (Input.GetKey (KeyCode.Space)) 
		{
			JumpDistance = 2.5f;
			//JumpTimer =  Time.time;
			//Debug.Log("JumpTimer:" + JumpTimer);
		} 
		else
		{
			JumpDistance = 0;
		}
		
		/*if(Time.time - JumpTimer >= 3.of)
			movement.Set (h + Jump)  */
		
		movement.Set (h, JumpDistance, v);
		
		movement = movement.normalized * TranslateSpeed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position + movement);
		
		//JumpDistance = 0f; // Walking
		
	}

	void Turn(float horizontal, float vertical)
	{
		//transform.Rotate (0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
		
		
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, RotateSpeed * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);

	}

	void Animating(float h, float v)
	{
		/*
		* Mecanim animator setup
		*
		*
		*
		*/

			int landMotionType;

			if (h != 0f || v != 0f) 
			{
				//walking animation
				landMotionType = 1;
			} 
			else if(h != 0f || v != 0f && Input.GetKey(KeyCode.C))
			{
				//running animation
				landMotionType = 2;
			}
			else if(Input.GetKey (KeyCode.Space))
			{
				//jumping animation
				landMotionType = 3;
			}
			else
			{
				landMotionType = 0;
			}


		anim.SetInteger ("LandMotionType", landMotionType);
	}

}
