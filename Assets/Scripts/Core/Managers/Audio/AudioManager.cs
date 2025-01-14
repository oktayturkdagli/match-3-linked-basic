using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Match3Linked.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private List<AudioClip> audioClipList;
        
        private float _volumeScale;
        
        public float VolumeScale
        {
            get => _volumeScale;

            set
            {
                _volumeScale = Mathf.Clamp01(value);
                SetVolume(_volumeScale);
            }
        }

        private string PlayerPrefsVolumeKey => $"{GetType().Name}.Volume";

        private AudioSource _audioSource;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void Start()
        {
            LoadSettings();
        }
        
        public void PlayOneShot(string clipName, float volumeScale = 1.0f)
        {
            var clip = audioClipList.Find(audioClip => audioClip.name == clipName);
            _audioSource.PlayOneShot(clip, volumeScale);
        }

        private void SetVolume(float volumeScale)
        { 
            SetVolume("SFX Volume", volumeScale); 
            SaveSettings();
        }
        
        private void SetVolume(string volumeParameterName, float volumeScale)
        {
            //Ensure scale is in [0,1]
            volumeScale = Mathf.Clamp01(volumeScale);

            const float min = 0.0001f; //0.0001 equals -80dB
            const float max = 1; //1 equals 0dB

            //Scale volume
            float linearValue = Mathf.Lerp(min, max, volumeScale);

            //Convert volume to decibel
            float dBValue = 20 * Mathf.Log10(linearValue);

            //Set volume
            audioMixer.SetFloat(volumeParameterName, dBValue);
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetFloat(PlayerPrefsVolumeKey, VolumeScale);
            PlayerPrefs.Save();
        }
        
        private void LoadSettings()
        {
            VolumeScale = PlayerPrefs.HasKey(PlayerPrefsVolumeKey) ? PlayerPrefs.GetFloat(PlayerPrefsVolumeKey) : 1.0f;
        }
    }
}