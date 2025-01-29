using System;
using System.Collections.Generic;
using Core;
using Kitchen.Products;
using UnityEngine;
using Zenject;

namespace Kitchen.ProductsContainer.Components
{
    public class ProductsContainer : MonoBehaviour
    {
        [SerializeField] private List<Transform> _slotsPositions;
        [SerializeField] private GameObject _slotPrefab;
        
        [Inject] private GameobjectFactory _gameobjectFactory;

        private void Start()
        {
            InstantiateSlots();
        }

        private void InstantiateSlots()
        {
            foreach (var slotPosition in _slotsPositions)
            {
                _gameobjectFactory.Create(_slotPrefab, slotPosition);
            }
        }
    }
}