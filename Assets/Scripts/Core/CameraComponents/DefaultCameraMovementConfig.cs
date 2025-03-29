using UnityEngine;

namespace Core.CameraComponents
{
    [CreateAssetMenu(fileName = "DefaultCameraMovementConfig", menuName = "Configs/Camera/DefaultCameraMovementConfig")]
    public class DefaultCameraMovementConfig : ScriptableObject
    {
        [field: SerializeField] 
        [field: Range(0f, 200f)] 
        public float HorizontalEdgeThreshold { get; private set; } = 100f;
        
        [field: SerializeField] 
        [field: Range(0f, 200f)] 
        public float VerticalEdgeThreshold { get; private set; } = 100f;

        [field: SerializeField]
        [field: Range(0f, 2f)]
        public float SmoothSpeed { get; private set; } = 0.5f;
    }
}