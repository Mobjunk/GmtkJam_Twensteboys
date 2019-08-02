using UnityEngine;
using UnityEngine.UI;

namespace r0w3ntje
{
    public class SoundManager : Singleton<SoundManager>
    {
        //[SerializeField] private Sprite volume_On = null;
        //[SerializeField] private Sprite volume_Off = null;

        //[SerializeField] private Image speakerButtonIcon = null;

        [SerializeField] private AudioClip start = null;

        //private void Start()
        //{
        //    RefreshSpeakerIcon();
        //}

        //private void OnValidate()
        //{
        //    RefreshSpeakerIcon();
        //}

        public void PlayStartSound()
        {
            PlaySound(start);
        }

        public void PlaySound(AudioClip _ac)
        {
            GameObject si = new GameObject("SoundInstance");
            si.transform.SetParent(transform);

            var soundInstance = si.AddComponent<SoundInstance>();

            var audioSource = si.AddComponent<AudioSource>();
            soundInstance.audioSouce = audioSource;

            soundInstance.audioSouce.clip = _ac;
            //soundInstance.audioSouce.volume = Data.file.mute ? 0f : 1f;
            soundInstance.audioSouce.Play();
        }

        //public void SoundToggle()
        //{
        //    //Data.file.mute = !Data.file.mute;

        //    Data.Save();

        //    RefreshSpeakerIcon();
        //}

        //private void RefreshSpeakerIcon()
        //{
        //speakerButtonIcon.sprite = SaveData.file.mute ? volume_Off : volume_On;
        //}
    }

    public class SoundInstance : MonoBehaviour
    {
        public AudioSource audioSouce;

        private void FixedUpdate()
        {
            if (!audioSouce.isPlaying)
                Destroy(gameObject);
        }
    }
}