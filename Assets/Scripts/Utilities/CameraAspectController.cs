using UnityEngine;

namespace Match3Linked
{
    /// <summary>
    /// A component that adjusts the camera's aspect ratio to support a minimum width.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class CameraAspectController : MonoBehaviour
    {
        [SerializeField] private float minimumAspectWidth = 6.75f; // The minimum width the camera should support

        private Camera _mainCamera;
        private Vector2 _previousScreenDimensions = Vector2.zero;

        private void Start()
        {
            InitializeCamera();
            UpdateCameraAspect();
        }

        private void Update()
        {
            if (!_mainCamera)
                InitializeCamera();
            
            DetectAndHandleScreenSizeChanges();
        }

        /// <summary>
        /// Initializes the camera component.
        /// </summary>
        private void InitializeCamera()
        {
            _mainCamera = GetComponent<Camera>();
            if (!_mainCamera)
            {
                Debug.LogError("CameraAspectController requires a Camera component.");
            }
        }

        /// <summary>
        /// Detects changes in screen dimensions and updates the camera aspect ratio if needed.
        /// </summary>
        private void DetectAndHandleScreenSizeChanges()
        {
            if (ScreenDimensionsChanged())
            {
                UpdateCameraAspect();
            }
        }

        /// <summary>
        /// Checks if the screen dimensions have changed since the last update.
        /// </summary>
        /// <returns>True if screen dimensions have changed, otherwise false.</returns>
        private bool ScreenDimensionsChanged()
        {
            return !Mathf.Approximately(_previousScreenDimensions.x, Screen.width) ||
                   !Mathf.Approximately(_previousScreenDimensions.y, Screen.height);
        }

        /// <summary>
        /// Updates the camera's aspect ratio based on the current screen dimensions.
        /// </summary>
        private void UpdateCameraAspect()
        {
            _previousScreenDimensions = new Vector2(Screen.width, Screen.height);
            AdjustCameraAspectRatio();
        }

        /// <summary>
        /// Adjusts the camera's aspect ratio, applying letterboxing if necessary.
        /// </summary>
        private void AdjustCameraAspectRatio()
        {
            if (!_mainCamera)
            {
                Debug.Log("Main Camera is not initialized.");
                return;
            }

            ResetCameraAspect();

            float currentAspectWidth = CalculateCameraWidth();

            if (currentAspectWidth < minimumAspectWidth)
            {
                ApplyLetterbox(currentAspectWidth / minimumAspectWidth);
            }
        }

        /// <summary>
        /// Resets the camera's aspect ratio to its default state.
        /// </summary>
        private void ResetCameraAspect()
        {
            _mainCamera.rect = new Rect(0, 0, 1, 1);
            _mainCamera.ResetAspect();
        }

        /// <summary>
        /// Calculates the current width of the camera's visible area.
        /// </summary>
        /// <returns>The width of the camera's view.</returns>
        private float CalculateCameraWidth()
        {
            return _mainCamera.orthographicSize * 2 * _mainCamera.aspect;
        }

        /// <summary>
        /// Applies a letterbox effect to the camera's view.
        /// </summary>
        /// <param name="aspectRatio">The ratio of the current width to the minimum width.</param>
        private void ApplyLetterbox(float aspectRatio)
        {
            Rect adjustedRect = new Rect(0, 0, 1, 1);
            adjustedRect.height *= aspectRatio;
            adjustedRect.y = (1 - adjustedRect.height) / 2f;

            // Round values for better precision
            adjustedRect.y = Mathf.Round(adjustedRect.y * 10000) / 10000f;
            adjustedRect.height = Mathf.Round(adjustedRect.height * 10000) / 10000f;

            _mainCamera.rect = adjustedRect;
        }
    }
}