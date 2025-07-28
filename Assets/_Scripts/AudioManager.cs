using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSO audioClips;
    private AudioSource audioSource;
    [SerializeField] private VoidEventChannel onCardMatched;
    [SerializeField] private VoidEventChannel onCardNotMatched;
    [SerializeField] private VoidEventChannel onCardFlipped;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        onCardMatched.OnEventInvoked += OnCardMatched;
        onCardNotMatched.OnEventInvoked += OnCardNotMatched;
        onCardFlipped.OnEventInvoked += OnCardFlipped;
        
    }

    private void OnCardFlipped(NoParam obj)
    {
        PlayFlip();
    }

    private void OnCardNotMatched(NoParam obj)
    {
        PlayMisMatch();
    }

    private void OnCardMatched(NoParam obj)
    {
        PlayMatch();
    }

    private void PlayMisMatch()
    {
        PlayClip(audioClips.mismatch);
    }

    private void PlayMatch()
    {
        PlayClip(audioClips.match);
    }

    private void PlayFlip()
    {
        PlayClip(audioClips.flip);
    }
    private void PlayClip(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;

        audioSource.clip = clip;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play();
    }
}
