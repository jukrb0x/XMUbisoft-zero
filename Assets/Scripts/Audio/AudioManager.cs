using System;
using UnityEngine;

/// <summary>
/// Central place to hold and play all the sounds.<br/>
/// 
/// To add your own sound, go to SoundEnum to add your own sound enum, then in the inspector,
/// inspect the AudioManager game object and add your sound entry.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
	public Audio[] sounds;

	protected override void Awake()
	{
		foreach (Audio sound in sounds)
		{
			sound.source = gameObject.AddComponent<AudioSource>();
			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;
		}
	}

	/// <summary>
	/// Plays a sound.
	/// </summary>
	/// <param name="audioEnum">the sound</param>
	public AudioSource Play(AudioEnum audioEnum)
	{
		//print("bzzzztz");
		if (audioEnum == AudioEnum.NoAudio)
			return null;
		Audio audio = GetSound(audioEnum);
		audio?.source.Play();
		return audio?.source;
	}

	public AudioSource PlayOneShot(AudioEnum audioEnum)
	{
		if (audioEnum == AudioEnum.NoAudio)
			return null;
		Audio audio = GetSound(audioEnum);
		audio?.source.PlayOneShot(audio.clip);
		return audio?.source;
	}

	/// <summary>
	/// Stops a sound.
	/// </summary>
	/// <param name="audioEnum">the sound</param>
	public void Stop(AudioEnum audioEnum)
	{
		if (audioEnum == AudioEnum.NoAudio)
			return;
		Audio audio = GetSound(audioEnum);
		audio?.source.Stop();
	}

	public Audio GetSound(AudioEnum audioEnum)
	{
		Audio theAudio = Array.Find(sounds, audio => audio.audioEnum == audioEnum);
		if (theAudio == null)
			Debug.LogError("Sound with sound enum " + audioEnum + " does not exist!");
		return theAudio;
	}
}
