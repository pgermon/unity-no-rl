using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.SoundManagerNamespace
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource[] MusicAudioSources;
        public AudioSource[] SoundAudioSources;

        private float last_roar = 0;
        private float next_roar_in = 10;

        private void PlaySound(int index)
        {
            SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
        }

        private void PlayMusic(int index)
        {
            MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, true);
        }

        // Start is called before the first frame update
        void Start()
        {
            PlayMusic(0);
            next_roar_in = 10 + Random.value * 2; 
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - last_roar > next_roar_in)
            {
                int roar_index = Random.Range(0, SoundAudioSources.Length);
                PlaySound(roar_index);
                next_roar_in = 10 + Random.value * 2;
                last_roar = Time.time;
            }
        }
    }
}
