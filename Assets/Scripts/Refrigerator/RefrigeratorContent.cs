using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Refrigerator
{
    [CreateAssetMenu(fileName = "RefrigeratorContent", menuName = "Scriptable Objects/Refrigerator/Content")]
    public class RefrigeratorContent : ScriptableObject
    {
        [SerializeField] private List<RefrigeratorProduct> _refrigeratorProducts;

        public IEnumerable<RefrigeratorProduct> RefrigeratorProducts => _refrigeratorProducts;

        private void OnValidate()
        {
            var productsDuplicates = _refrigeratorProducts
                .GroupBy(p => p.Type)
                .Where(g => g.Count() > 1)
                .ToList();

            if (productsDuplicates.Count > 0)
            {
                throw new InvalidOperationException(
                    $"{nameof(_refrigeratorProducts)} has duplicate type: {productsDuplicates.First().Key}");
            }
        }
    }
}   