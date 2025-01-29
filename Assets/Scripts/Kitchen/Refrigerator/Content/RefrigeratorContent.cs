using System.Collections.Generic;
using System.Linq;
using Kitchen.Products;
using UnityEngine;

namespace Kitchen.Refrigerator.Content
{
    [CreateAssetMenu(fileName = "RefrigeratorContent", menuName = "Scriptable Objects/Refrigerator/RefrigeratorContent")]
    public class RefrigeratorContent : ScriptableObject
    {
        [SerializeField] private List<Product> _products = new();

        public IEnumerable<Product> Products => _products;

        private void OnValidate()
        {
            _products = _products
                .GroupBy(p => p.Type)
                .Select(g => g.First())
                .ToList();
        }
    }
}   