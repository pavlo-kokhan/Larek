using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    // todo
    // -------- refactor ---------- //
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] private DialogPanelManager dialogPanelManager;
        
        public void NewGame()
        {
            SceneManager.LoadScene(1);
        }
        
        public void ContinueGame()
        {
            SceneManager.LoadScene(1);
        }

        public void Settings()
        {
            Debug.Log("Settings");
        }
        
        public void Gallery()
        {
            Debug.Log("Gallery");
        }
        
        public void QuitGame()
        {
            dialogPanelManager.ShowDialog("Are you sure you want to quit?", confirmed =>
            {
                if (confirmed)
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }
            });
        }
    }
}