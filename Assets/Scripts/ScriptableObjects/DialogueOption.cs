using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New DialogueOption", menuName = "Scriptable Objects/DialogueOption")]
    public class DialogueOption : ScriptableObject
    {
        public string response;
        public Dialogue nextDialogue;
    }
}