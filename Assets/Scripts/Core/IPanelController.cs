using System;

namespace Core
{
    public interface IPanelController
    {
        public event Action<bool> PanelActivationChanged;
    }
}