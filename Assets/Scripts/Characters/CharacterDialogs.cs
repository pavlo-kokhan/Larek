using System;
using ScriptableObjects;
using UnityEngine;

namespace Characters
{
    public class CharacterDialogs : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        private void Start()
        {
            LogOptions();
        }

        // test shit
        private void Update()
        {
            for (int i = 0; i <= 9; i++)
            {
                try
                {
                    if (Input.GetKeyUp(KeyCode.Alpha0 + i))
                    {
                        if (_dialogue.playerResponses[i] != null)
                        {
                            var response = _dialogue.playerResponses[i];
                            Debug.Log($"Player: {response}");

                            if (_dialogue.nextDialogues[i] != null)
                            {
                                _dialogue = _dialogue.nextDialogues[i];
                                LogOptions();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Debug.Log("No such options");
                }
            }
        }

        private void LogOptions()
        {
            Debug.Log($"Character: {_dialogue.characterPhrase}");
            for (int i = 0; i < _dialogue.playerResponses.Length; i++)
            {
                Debug.Log($"{i}: {_dialogue.playerResponses[i]}");
            }
        }
    }
}