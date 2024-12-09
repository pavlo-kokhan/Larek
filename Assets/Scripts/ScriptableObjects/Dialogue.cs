using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public string characterPhrase;
        public string[] playerResponses;
        public Dialogue[] nextDialogues;
    }
}