using System;
using System.Collections.Generic;
using UnityEngine;

namespace Radio
{
    public class RadioInputController : MonoBehaviour
    {
        [SerializeField] private GameObject radioUI;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                radioUI.SetActive(false);
            }
        }
    }
}
