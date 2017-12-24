﻿/*
 * Author(s): Isaiah Mann 
 * Description: Wrapper class to store a list of AudioFiles
 * Has array-like properties
 * Dependencies: AudioFile
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AudioList {
	Dictionary<AudioClip, AudioFile> clipToFileDictionary = new Dictionary<AudioClip, AudioFile>();
	public bool AreEventsSubscribed = false;
	public AudioList (AudioFile[] files) {
		Files = files;
		SubscribeEvents();
	}

	// Destructor for Garbage Collection:
	~AudioList () {
		UnsubscribeEvents();
	}

	public AudioList(){}

	public AudioFile[] Files;
	public AudioGroup[] Groups;

	public AudioFile this[int index] {
		get {
			return Files[index];
		}
	}

	public int Length {
		get {
			if (Files == null) { 
				return 0;
			} else {
				return Files.Length;
			}
		}
	}

	public void PopulateGroups () {
		foreach (AudioFile file in Files) {
			foreach (AudioGroup group in Groups) {
				if (ArrayUtil.Contains(file.Groups, group.Name)) {
					group.AddFile(file);
				}
			}
		}
	}

	public AudioFile GetAudioFile (AudioClip fromClip) {
		AudioFile toFile;
		if (clipToFileDictionary.TryGetValue(fromClip, out toFile)) {
			return toFile;
		} else {
			return AudioFile.Default;
		}
	}

	public AudioType GetAudioType (AudioClip clip) {
		return GetAudioFile(fromClip:clip).TypeAsEnum;
	}

	void ProcessAudioFileAccess (AudioFile file) {
		if(file.Clip)
		{
			AddToClipDictionary(file);
		}
		else
		{
			Debug.LogErrorFormat("Clip for AudioFile is null: \n{0}", file);
		}
	}

	void AddToClipDictionary (AudioFile file) {
		if (!clipToFileDictionary.ContainsKey(file.Clip)) {
			clipToFileDictionary.Add(file.Clip, file);
		}
	}

	public void SubscribeEvents () {
		for (int i = 0; i < Files.Length; i++) {
			Files[i].OnClipRequest += ProcessAudioFileAccess;
		}
		AreEventsSubscribed = true;
	}

	void UnsubscribeEvents () {
		for (int i = 0; i < Files.Length; i++) {
			Files[i].OnClipRequest -= ProcessAudioFileAccess;
		}
	}

	void HandleClipRequest (AudioFile file) {
		ProcessAudioFileAccess(file);
	}		
}
