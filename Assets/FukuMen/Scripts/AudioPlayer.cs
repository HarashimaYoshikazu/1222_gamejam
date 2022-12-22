using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("âπÇê›íË")]
    [SerializeField]
    private AudioClip _clipDamage;
    [SerializeField]
    private AudioClip _clipJump;
    [SerializeField]
    private AudioClip _clipGoal;

    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void AudioPlay(string name)
    {
        switch (name)
        {
            case "Damage":
                {
                    _audioSource.PlayOneShot(_clipDamage);
                }
                break;
            case "Jump":
                {
                    _audioSource.PlayOneShot(_clipJump);
                }
                break;
            case "Goal":
                {
                    _audioSource.PlayOneShot(_clipGoal);
                }
                break;
        }
    }
}
