using System;

namespace Orders
{
    [Serializable]
    public class OrderItem
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public bool IsForHuman { get; set; }

        public OrderItem(bool isSelected, string name, int count, int price, bool isForHuman)
        {
            IsSelected = isSelected;
            Name = name;
            Count = count;
            Price = price;
            IsForHuman = isForHuman;
        }

        public override string ToString()
        {
            return $"IsSelected: {IsSelected} Name: {Name}, Count: {Count}, Price: {Price}, IsForHuman: {IsForHuman}";
        }
    }
}