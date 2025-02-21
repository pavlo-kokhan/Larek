using Core.Dialogs;
using Spine.Unity;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(Collider2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterPhrasePanel _phrasePanel;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private string _name;
        [SerializeField] private TextAsset _inkJson;

        private DialogueService _dialogueService;
        
        [Inject]
        public void Construct(DialogueService dialogueService)
        {
            _dialogueService = dialogueService;
        }

        private void Start()
        {
            Stand();
        }

        private void OnEnable()
        {
            _phrasePanel.StartedTypingPhrase += Speak;
            _phrasePanel.EndedTypingPhrase += Stand;
        }

        private void OnDisable()
        {
            _phrasePanel.StartedTypingPhrase -= Speak;
            _phrasePanel.EndedTypingPhrase -= Stand;
        }

        private void OnMouseDown()
        {
            StartDialogue();
        }

        private void StartDialogue()
        {
            _dialogueService.StartDialogue(_phrasePanel, _inkJson);
        }

        private void Speak()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "speak", true);
        }

        private void Stand()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "base", true);
        }
    }
}