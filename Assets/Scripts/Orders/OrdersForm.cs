using System.Collections.Generic;
using System.Linq;
using Orders.Input;
using UnityEngine;

namespace Orders
{
    public class OrdersForm : MonoBehaviour
    {
        [SerializeField] private OrderFormInput _orderFormInput;
        [SerializeField] private Transform _itemsContainer;
        
        private List<OrderItem> _orderItems;
        private GameObject _orderItemPrefab;

        private void OnEnable()
        {
            _orderFormInput.PageTurned += OnPageTurned;
            _orderFormInput.OrderConfirmed += OnOrderConfirmed;
        }

        private void OnDisable()
        {
            _orderFormInput.PageTurned -= OnPageTurned;
            _orderFormInput.OrderConfirmed -= OnOrderConfirmed;
        }

        private void Start()
        {
            _orderItemPrefab = Resources.Load<GameObject>("Prefabs/OrderItem");
            
            _orderItems = new List<OrderItem>()
            {
                new (false, "Milk", 1, 15, true),
                new (false, "Bread", 1, 25, true),
                new (false, "Dildo BIG", 1, 45, true),
                new (false, "Some other stuff", 1, 120, true),
                new (false, "Monster item 1", 1, 15, false),
                new (false, "Monster item 2", 1, 25, false),
                new (false, "Monster item 3", 1, 45, false),
            };
            
            OnPageTurned(true);
        }
        
        private void OnPageTurned(bool isHumanPage)
        {
            ResetOrderItems();
            DestroyOrderItems();
            ShowOrderItems(_orderItems.Where(i => i.IsForHuman == isHumanPage));
        }

        private void ResetOrderItems()
        {
            foreach (var item in _orderItems)
            {
                item.IsSelected = false;
                item.Count = 1;
            }
        }
        
        private void DestroyOrderItems()
        {
            foreach (var item in _itemsContainer.GetComponentsInChildren<OrderItemView>())
            {
                Destroy(item.gameObject);
            }
        }

        private void ShowOrderItems(IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                var instance = Instantiate(_orderItemPrefab, _itemsContainer);
                var itemUI = instance.GetComponent<OrderItemView>();
                itemUI.Initialize(item);
            }
        }

        private void OnOrderConfirmed(bool isHumanPage)
        {
            var selectedItems = _orderItems.Where(i => 
                i.IsSelected && i.IsForHuman == isHumanPage);
            
            Debug.Log("You bought these items:");
            foreach (var item in selectedItems)
            {
                Debug.Log(item);
            }
        }
    }
}