using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonnou
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip[] _clips;
        AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlaySound(int num)
        {
            _source.clip = _clips[num];
            _source.Play();
        }
    }
}
