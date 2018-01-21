using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaDragDrop : MonoBehaviour {
	private bool isHeld;
	private GameObject reticle;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		/*if (isHeld) {
			Ray ray = new Ray (reticle.transform.position, reticle.transform.forward);
			transform.position = ray.GetPoint (4);
		}*/
	}
	/*public void callStart()
	{
		HandleGazeTriggerStart ();
	}
	public void callEnd()
	{
		HandleGazeTriggerEnd ();
	}
	public void HandleGazeTriggerStart()
	{
		isHeld = true;
	}

	public void HandleGazeTriggerEnd()
	{
		isHeld = false;
	}*/
	public void moveStraw()
	{
		transform.position = new Vector3 (17f, 3.57f, -18f);
		transform.rotation = new Quaternion (90, 100, 0, 0);
	}
	public void deleteCan()
	{
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Can"));
	}
}
