using System.Collections.Generic;
using UnityEngine;

namespace Panels
{
    public class InteractivePanelsRegistrator
    {
        private readonly List<GameObject> _activePanels = new();

        public void RegisterPanel(GameObject panel)
        {
            if (!_activePanels.Contains(panel))
            {
                panel.gameObject.SetActive(true);
                _activePanels.Add(panel);
            }
        }

        public void UnregisterPanel(GameObject panel)
        {
            panel.gameObject.SetActive(false);
            _activePanels.Remove(panel);
        }

        public void CloseAllPanels()
        {
            foreach (var panel in _activePanels)
            {
                panel.SetActive(false);
            }
            
            _activePanels.Clear();
        }
    }
}