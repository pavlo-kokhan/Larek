using AYellowpaper.SerializedCollections;
using Kitchen.Products.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.AssemblyBoard
{
    public class BoardShapeImage : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private ShapeButton _shapeButton;
        
        [SerializedDictionary] 
        [SerializeField] 
        private SerializedDictionary<ProductShapeType, Sprite> _shapeImages = new();
        
        private void OnEnable()
        {
            _shapeButton.ShapeChanged += SwitchShape;
        }

        private void OnDisable()
        {
            _shapeButton.ShapeChanged -= SwitchShape;
        }

        private void SwitchShape(ProductShapeType productShapeType)
        {
            _image.sprite = _shapeImages[productShapeType];
        }
    }
}