using UnityEngine;

namespace Cursor
{
    public class CursorView : MonoBehaviour
    {
        public static CursorView Instance { get; private set; }
        
        [SerializeField] private Texture2D _idleTexture;
        [SerializeField] private Texture2D _questionTexture;
        [SerializeField] private Texture2D _exclamationTexture;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SetDefaultCursorTexture();
        }

        public void SetCursorTexture(CursorTextureMode cursorTextureMode) 
        {
            switch (cursorTextureMode)
            {
                case CursorTextureMode.Idle:
                    SetDefaultCursorTexture();
                    break;
                case CursorTextureMode.Question:
                    UnityEngine.Cursor.SetCursor(_questionTexture, Vector2.zero, CursorMode.Auto);
                    break;
                case CursorTextureMode.Exclamation:
                    UnityEngine.Cursor.SetCursor(_exclamationTexture, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }

        public void SetDefaultCursorTexture()
        {
            UnityEngine.Cursor.SetCursor(_idleTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}