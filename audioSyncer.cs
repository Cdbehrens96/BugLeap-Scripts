﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSyncer : MonoBehaviour {

	public float bias;
	public float timeStep;
	public float timeToBeat;
	public float restSmoothTime;

	private float m_previousAudioValue;
	private float m_audioValue;
	private float m_timer;

	protected bool m_isBeat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		OnUpdate();
	}

	public virtual void OnUpdate(){
		m_previousAudioValue = m_audioValue;
		m_audioValue = audioSpectrum.spectrumValue;\
		if(m_previousAudioValue > bias && m_audioValue <=bias){
			if(m_timer > timeStep)
				// calls OneBeat function
				OnBeat();
		}
	}

	public virtual void OnBeat(){
		Debug.Log("beat");
		m_timer = 0;
		m_isBeat = true;
	}
}
