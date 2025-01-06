using System;
using System.Linq;
using Core;
using Data;
using UnityEngine;
using Zenject;

namespace Refrigerator
{
    public class Refrigerator : ClickableObjectWithUI
    {
        [SerializeField] private RefrigeratorContent _defaultRefrigeratorContent;
        [Inject] private RefrigeratorDataProvider _dataProvider;
        [Inject] private IPersistentData _persistentData;
        
        protected override void OnPanelLoaded()
        {
            var panel = _interactivePanelInstance.GetComponent<RefrigeratorPanel>();

            if (panel == null)
            {
                throw new InvalidOperationException(
                    $"Component {typeof(RefrigeratorPanel)} does not exist in {nameof(_interactivePanelInstance)}");
            }

            if (_dataProvider.TryLoad() == false)
            {
                _persistentData.RefrigeratorData = new RefrigeratorData(_defaultRefrigeratorContent);
            }

            var content = _persistentData.RefrigeratorData.RefrigeratorContent;
            var products = content.Products.ToList();
            panel.InstantiateProductPrefabs(products);
        }
    }
}