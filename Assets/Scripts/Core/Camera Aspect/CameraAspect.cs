using UnityEngine;

namespace Match3Linked.Core
{
    [RequireComponent(typeof(Camera)), ExecuteInEditMode]
    public class CameraAspect : MonoBehaviour
    {
        [SerializeField] private float minWidth = 6.75f;
        private Camera _camera;
        private Vector2 _screenDimensions = Vector2.zero;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            UpdateCameraAspect();
        }

        private void ApplyAspectRatio()
        {
            // Reset Camera aspect
            _camera ??= GetComponent<Camera>();
            _camera.rect = new Rect(0, 0, 1, 1);
            _camera.ResetAspect();

            // Get the current values of the camera
            float cameraWidth = _camera.GetWidth();

            // Is the available width smaller than expected? Apply a letterbox
            if (cameraWidth < minWidth)
            {
                ApplyLetterbox(cameraWidth / minWidth);
            }
        }

        private void ApplyLetterbox(float ratio)
        {
            Rect rect = new Rect(0, 0, 1, 1);
            rect.height *= ratio;
            rect.y = (1 - rect.height) / 2;

            // Round values to 4 digits after dot
            rect.y = (float)System.Math.Round(rect.y, 5);
            rect.height = (float)System.Math.Round(rect.height, 5);
            _camera.rect = rect;
        }

        private void Update()
        {
            // Detect and handle screen size changes
            if (!Mathf.Approximately(_screenDimensions.x, Screen.width) || !Mathf.Approximately(_screenDimensions.y, Screen.height))
            {
                UpdateCameraAspect();
            }
        }

        private void UpdateCameraAspect()
        {
            _screenDimensions = new Vector2()
            {
                x = Screen.width,
                y = Screen.height
            };

            ApplyAspectRatio();
        }
    }
}