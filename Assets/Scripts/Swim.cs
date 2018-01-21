using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour {

	// Use this for initialization
	CharacterController charControl;
	CharacterMotor charMotor;
	ParticleSystem bubbles ;

	void Start () {
		//RenderSettings.fog = false;
		RenderSettings.fogColor = new Color (0.22f, 0.65f, 0.77f, 0.5f);
		RenderSettings.fogDensity = 0.01f;
		charControl = gameObject.GetComponent<CharacterController> ();
		charMotor = gameObject.GetComponent<CharacterMotor> ();
		bubbles.Stop ();
		GameObject.Find("Blob Light Projector").GetComponent<Projector>().enabled = false;
	}
	bool isUnderwater()
	{
		return gameObject.transform.position.y < 100;
	}
	
	// Update is called once per frame
	void Update () 
	{
		RenderSettings.fog = isUnderwater();
		charMotor.jumping.enabled = !isUnderwater ();
		if (isUnderwater ()) {
			charMotor.movement.maxForwardSpeed = 2;
			charMotor.movement.maxSidewaysSpeed = 2;
			charMotor.movement.maxFallSpeed = 2;
			charMotor.movement.maxBackwardsSpeed = 2;
			charMotor.movement.gravity = 2;
			bubbles.Play ();
			GameObject.Find("Blob Light Projector").GetComponent<Projector>().enabled = true;
		} else {
			charMotor.movement.maxForwardSpeed = 12;
			charMotor.movement.maxSidewaysSpeed = 9;
			charMotor.movement.maxFallSpeed = 20;
			charMotor.movement.maxBackwardsSpeed = 9;
		}

	}
}
