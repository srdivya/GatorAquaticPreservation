using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedProjector : MonoBehaviour {

	public float FPS = 30.0f;
	public Texture2D[] frames;
	private int frameIndex;
	private Projector projector;
	void start()
	{
		projector = GetComponent<Projector> ();
		NextFrame ();
		InvokeRepeating ("NextFrame", 1 / FPS, 1 / FPS);
	}
	void NextFrame()
	{
		projector.material.SetTexture ("_ShadowTex", frames [frameIndex]);
		frameIndex = (frameIndex + 1) % frames.Length;
	}
}