using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Match3Linked.Game;

namespace Match3Linked.UI
{
    /// <summary>
    /// This class handles the volume slider UI component, allowing users to adjust the audio volume.
    /// It updates both the slider and the displayed volume percentage in real-time.
    /// </summary>
    public class UIVolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;  // Slider UI component for volume control
        [SerializeField] private TextMeshProUGUI volumePercentageText;  // Text UI component for displaying volume as a percentage

        /// <summary>
        /// Initializes the volume slider and text components at the start.
        /// Sets the initial values based on the current volume scale.
        /// </summary>
        protected void Start()
        {
            if (!volumeSlider || !volumePercentageText)
            {
                Debug.LogError("VolumeSlider: Missing slider or text component.");
                return;
            }

            volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
            InitializeSliderAndText(AudioManager.Instance.VolumeScale);
        }
        
        private void OnDestroy()
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeSliderValueChanged);
        }

        /// <summary>
        /// Initializes the slider and text with the given volume scale.
        /// </summary>
        /// <param name="volumeScale">The current volume scale (from 0.0f to 1.0f).</param>
        private void InitializeSliderAndText(float volumeScale)
        {
            SetSliderValue(volumeScale);
            SetVolumePercentageText(volumeScale);
        }

        /// <summary>
        /// Sets the volume slider's value.
        /// </summary>
        /// <param name="volumeScale">The value to set for the slider, representing volume scale.</param>
        private void SetSliderValue(float volumeScale)
        {
            volumeSlider.value = Mathf.Clamp01(volumeScale);  // Ensure value is between 0 and 1
        }

        /// <summary>
        /// Updates the volume percentage text.
        /// </summary>
        /// <param name="volumeScale">The current volume scale to display as a percentage.</param>
        private void SetVolumePercentageText(float volumeScale)
        {
            volumePercentageText.text = $"{(int)(volumeScale * 100)} %";
        }

        /// <summary>
        /// Called when the slider value changes. Updates the AudioManager volume scale and the displayed percentage text.
        /// </summary>
        /// <param name="value">The new value of the volume slider.</param>
        private void OnVolumeSliderValueChanged(float value)
        {
            AudioManager.Instance.VolumeScale = value;
            SetVolumePercentageText(value);
        }
    }
}
