using UnityEngine;

namespace Gates
{
    public class Gates : MonoBehaviour
    {
        [SerializeField] private GatesSwitcher gatesSwitcher;
        [SerializeField] private GatesSounds gatesSounds;

        private void Start()
        {
            gatesSwitcher.OnGatesSwitchedSound += gatesSounds.PlaySound;
        }
    }
}