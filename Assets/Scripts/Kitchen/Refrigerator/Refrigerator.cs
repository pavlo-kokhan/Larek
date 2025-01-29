using System;
using System.Linq;
using Core;
using Data;
using Kitchen.Refrigerator.Content;
using UnityEngine;
using Zenject;

namespace Kitchen.Refrigerator
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

            var content = _persistentData.RefrigeratorData.ContentState;
            var products = content.ProductStates.ToList();
            panel.InstantiateProductPrefabs(products);
        }
    }
}