using UnityEngine;

namespace Match3Linked
{
    public static class CameraExtensions
    {
        private static float GetHeight(this Camera camera)
        {
            return camera.orthographicSize * 2;
        }
        
        public static float GetWidth(this Camera camera)
        {
            return camera.GetHeight() * camera.aspect;
        }
    }
}