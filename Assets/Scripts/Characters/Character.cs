using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(CharacterInteractionIndicator))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private TextAsset _inkJson;
        [SerializeField] private CharacterInteractionIndicator _interactionIndicator;

        private bool _wantsToTalk;
        
        private void Start()
        {
            _wantsToTalk = true;
            _interactionIndicator.DeactivateIndicator();
        }
        
        public bool TryStartDialogue()
        {
            if (_inkJson != null && _wantsToTalk)
            {
                // dialogueManager.StartDialogue(_inkJson);
                Debug.Log("Dialogue started");
                return true;
            }

            return false;
        }

        public void TriggerInteraction()
        {
            if (_wantsToTalk)
            {
                _interactionIndicator.ActivateIndicator();
            }
        }

        public void EndInteraction()
        {
            _interactionIndicator.DeactivateIndicator();
        }
    }
}