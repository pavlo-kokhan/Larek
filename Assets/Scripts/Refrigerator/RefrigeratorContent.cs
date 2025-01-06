using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Refrigerator
{
    [CreateAssetMenu(fileName = "RefrigeratorContent", menuName = "Scriptable Objects/Refrigerator/Content")]
    public class RefrigeratorContent : ScriptableObject
    {
        [SerializeField] private List<RefrigeratorProduct> _products = new();

        public IEnumerable<RefrigeratorProduct> Products => _products;

        private void OnValidate()
        {
            _products = _products
                .GroupBy(p => p.Type)
                .Select(g => g.First())
                .ToList();
        }
    }
}   