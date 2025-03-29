using System;
using AYellowpaper.SerializedCollections;
using Kitchen.Products.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.AssemblyBoard
{
    public class ShapeButton : MonoBehaviour
    {
        public event Action<ProductShapeType> ShapeChanged;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _shapeImage;

        [SerializedDictionary] 
        [SerializeField] 
        private SerializedDictionary<ProductShapeType, Sprite> _shapeImages = new();
        
        private ProductShapeType _currentShapeType;

        private void OnEnable()
        {
            _button.onClick.AddListener(SwitchShape);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchShape);
        }

        private void SwitchShape()
        {
            _currentShapeType = GetNextShapeType();
            _shapeImage.sprite = _shapeImages[_currentShapeType];
            ShapeChanged?.Invoke(_currentShapeType);
        }
        
        private ProductShapeType GetNextShapeType()
        {
            return _currentShapeType switch
            {
                ProductShapeType.Circle => ProductShapeType.CircleBig,
                ProductShapeType.CircleBig => ProductShapeType.Oval,
                ProductShapeType.Oval => ProductShapeType.Circle,
                _ => _currentShapeType,
            };
        }
    }
}