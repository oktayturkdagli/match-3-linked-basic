using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Match3Linked.Core;

namespace Match3Linked.Game
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI valueText;

        protected void Start()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            SetSliderValue(AudioManager.Instance.VolumeScale);
            SetValueText(AudioManager.Instance.VolumeScale);
        }

        private void SetSliderValue(float volumeScale)
        {
            slider.value = volumeScale;
        }

        private void SetValueText(float volumeScale)
        {
            valueText.text = (int)(volumeScale * 100) + " %";
        }

        private void OnSliderValueChanged(float value)
        {
            AudioManager.Instance.VolumeScale = value;
            SetValueText(AudioManager.Instance.VolumeScale);
        }
    }
}