using System;
using UnityEngine;

[Serializable]
public class Audio
{
	public AudioEnum audioEnum;
	public AudioClip clip;
	[Range(0, 1)]
	public float volume = 1;
	public float pitch;
	public bool loop;
	 public AudioSource source;
}
