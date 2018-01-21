using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************
/* Author: The Messy Coder
/* Date: September 2017
/* Video Tutorial: http://bit.ly/aiplaylist
/* Please support the channel, facebook and on twitter
/* YouTube:  www.YouTube.com/TheMessyCoder
/* Facebook: www.Facebook.com/TheMessyCoder
/* Twitter:  www.Twitter.com/TheMessyCoder
*******************/


public class AIMove : MonoBehaviour {

	//declare variable for AISpawner manager script
	private AISpawner m_AIManager;

	//declare variables for moving and turning
	private bool m_hasTarget = false;
	private bool m_isTurning;

	//variable for the current waypoint
	private Vector3 m_wayPoint;
	private Vector3 m_lastWaypoint = new Vector3 (0f,0f,0f);

	//going to use this to set the animation speed
	private Animator m_animator;
	private float m_speed;

	private Collider m_collider;


	public bool useRandomTarget;

	// Use this for initialization
	void Start () {
		//get the AISpawner from its parent
		m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
		m_animator = GetComponent<Animator>();

		SetUpNPC();
	}

	void SetUpNPC()
	{
		//Randomly scale each NPC
		float m_scale = Random.Range(0f, 10f);
		transform.localScale += new Vector3(m_scale * 1.5f, m_scale, m_scale);

		if (transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().enabled == true)
		{
			m_collider = transform.GetComponent<Collider>();
		}
		else if (transform.GetComponentInChildren<Collider>() != null && transform.GetComponentInChildren<Collider>().enabled == true)
		{
			m_collider = transform.GetComponentInChildren<Collider>();
		}
	}

	// Update is called once per frame
	void Update () {
		//if we have not found a way point to move to
		//if we found a waypoint we need to move there
		if (!m_hasTarget)
		{
			m_hasTarget = CanFindTarget();
		}
		else
		{
			//make sure we rotate the NPC to face its waypoint
			RotateNPC(m_wayPoint, m_speed);
			//move the NPC in a straight line toward the waypoint
			transform.position = Vector3.MoveTowards(transform.position, m_wayPoint, m_speed * Time.deltaTime);

			//Check if collided - if yes then lose the target and look for new waypoint
			CollidedNPC();
		}

		//if NPC reaches waypoint reset target
		if (transform.position == m_wayPoint)
		{
			m_hasTarget = false;
		}
	}

	//method for changing direction if NPC collides with something
	void CollidedNPC()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
		{
			//if collider has hit a waypoint or registers itself ignore raycast hit
			if (hit.collider == m_collider | hit.collider.tag == "waypoint")
			{
				return;
			}
			//otherwise have a random chance that NPC will change direction
			int randomNum = Random.Range(1, 100);
			if (randomNum < 40)
				m_hasTarget = false;
		}
	}

	//Get the Waypoint
	Vector3 GetWaypoint(bool isRandom)
	{
		//if isRandom is true then get a random position location
		if (isRandom)
		{
			return m_AIManager.RandomPosition();
		}
		//otherwise get a random waypoint from the list of waypoint gameObjects
		else
		{
			return m_AIManager.RandomWaypoint();
		}
	}

	bool CanFindTarget(float start = 20f, float end = 100f)
	{
		//make sure we dont set the same waypoint twice        
		if (m_lastWaypoint == m_wayPoint)
		{
			//get a new waypoint
			m_wayPoint = GetWaypoint(true);
			return false;
		}
		else
		{
			//set the new waypoint as the last waypoint
			m_lastWaypoint = m_wayPoint;
			//get random speed for movement and animation
			m_speed = Random.Range(start, end);
			m_animator.speed = m_speed;
			//set bool to true to say we found a WP
			return true;
		}        
	}

	//Rotate the NPC to face new waypoint
	void RotateNPC (Vector3 waypoint, float currentSpeed)
	{
		//get random speed up for the turn
		float TurnSpeed = currentSpeed * Random.Range(1f, 100f);

		//get new direction to look at for target
		Vector3 LookAt = waypoint - this.transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), TurnSpeed * Time.deltaTime);
	}

}