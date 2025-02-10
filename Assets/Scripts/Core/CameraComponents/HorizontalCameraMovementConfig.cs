using UnityEngine;

namespace Core.CameraComponents
{
    [CreateAssetMenu(fileName = "HorizontalCameraMovementConfig", menuName = "Scriptable Objects/Configs/HorizontalCameraMovementConfig")]
    public class HorizontalCameraMovementConfig : ScriptableObject
    {
        [field: SerializeField] 
        [field: Range(0f, 200f)] 
        public float EdgeThreshold { get; private set; } = 100f;

        [field: SerializeField]
        [field: Range(0f, 2f)]
        public float SmoothSpeed { get; private set; } = 0.5f;
    }
}