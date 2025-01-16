using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Match3Linked.Game
{
    /// <summary>
    /// Manages audio playback and volume settings within the game.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioMixer audioMixer; // The AudioMixer used for controlling audio parameters.
        [SerializeField] private List<AudioClip> audioClips; // List of available audio clips for playback.

        private float _volumeScale; // Internal volume scale value between 0 and 1.

        /// <summary>
        /// Gets or sets the volume scale, which is clamped between 0 and 1.
        /// When set, the volume is adjusted and saved.
        /// </summary>
        public float VolumeScale
        {
            get => _volumeScale;
            set
            {
                _volumeScale = Mathf.Clamp01(value);
                AdjustVolume(_volumeScale);
            }
        }

        private string PlayerPrefsVolumeKey => $"{GetType().Name}.Volume"; // PlayerPrefs key for saving volume.

        private AudioSource _audioSource; // The audio source used to play sound effects.

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>(); // Cache the AudioSource component.
        }

        private void Start()
        {
            LoadVolumeSettings(); // Load saved volume settings when the manager starts.
        }

        /// <summary>
        /// Plays a one-shot audio clip by name with an optional volume scale.
        /// </summary>
        /// <param name="clipName">The name of the audio clip to play.</param>
        /// <param name="volumeScale">The volume scale, default is 1.0 (max volume).</param>
        public void PlayOneShot(string clipName, float volumeScale = 1.0f)
        {
            var clip = audioClips.Find(audioClip => audioClip.name == clipName);
            if (clip)
            {
                _audioSource.PlayOneShot(clip, volumeScale);
            }
            else
            {
                Debug.LogWarning($"Audio clip with name '{clipName}' not found.");
            }
        }

        /// <summary>
        /// Adjusts the volume of the audio system and saves the setting.
        /// </summary>
        /// <param name="volumeScale">The desired volume scale.</param>
        private void AdjustVolume(float volumeScale)
        {
            ApplyVolumeToMixer("SFX Volume", volumeScale); // Apply volume to the audio mixer.
            SaveVolumeSettings(); // Save the volume setting for future sessions.
        }

        /// <summary>
        /// Applies the volume to the specified parameter in the audio mixer.
        /// </summary>
        /// <param name="volumeParameterName">The name of the volume parameter in the mixer.</param>
        /// <param name="volumeScale">The volume scale (0 to 1).</param>
        private void ApplyVolumeToMixer(string volumeParameterName, float volumeScale)
        {
            volumeScale = Mathf.Clamp01(volumeScale); // Ensure the scale is between 0 and 1.

            // Convert the volume scale to a linear value.
            float linearValue = Mathf.Lerp(0.0001f, 1f, volumeScale); 

            // Convert the linear value to decibels.
            float dBValue = 20 * Mathf.Log10(linearValue);

            // Set the volume on the audio mixer.
            audioMixer.SetFloat(volumeParameterName, dBValue);
        }

        /// <summary>
        /// Saves the current volume scale to PlayerPrefs.
        /// </summary>
        private void SaveVolumeSettings()
        {
            PlayerPrefs.SetFloat(PlayerPrefsVolumeKey, VolumeScale); // Save the volume scale.
            PlayerPrefs.Save(); // Ensure changes are persisted.
        }

        /// <summary>
        /// Loads the saved volume scale from PlayerPrefs.
        /// </summary>
        private void LoadVolumeSettings()
        {
            VolumeScale = PlayerPrefs.HasKey(PlayerPrefsVolumeKey) 
                ? PlayerPrefs.GetFloat(PlayerPrefsVolumeKey) 
                : 1.0f; // Default to full volume if no saved setting exists.
        }
    }
}