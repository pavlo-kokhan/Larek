using UnityEngine;
using UnityEngine.UI;

namespace Core.Dialogs
{
    [ExecuteAlways]
    public class DialogueGridLayoutGroup : LayoutGroup
    {
        [SerializeField] private Vector2 cellSize = new(700, 120);
        [SerializeField] private Vector2 spacing = new(0, 0);

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            ArrangeElements();
        }

        public override void CalculateLayoutInputVertical()
        {
            ArrangeElements();
        }

        public override void SetLayoutHorizontal()
        {
            ArrangeElements();
        }

        public override void SetLayoutVertical()
        {
            ArrangeElements();
        }

        private void ArrangeElements()
        {
            int childCount = transform.childCount;

            if (childCount == 0) return;

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            for (int i = 0; i < childCount; i++)
            {
                RectTransform child = (RectTransform)transform.GetChild(i);

                // Позиціонуємо елементи залежно від їхньої кількості
                if (childCount <= 3)
                {
                    // 1-3 елементи: 1 стовпець
                    float x = (parentWidth - cellSize.x) / 2; // Центруємо по горизонталі
                    float y = -i * (cellSize.y + spacing.y); // Відстань між елементами в стовпці
                    SetChildPosition(child, x, y);
                }
                else if (childCount == 4 || childCount == 6)
                {
                    // 4 елементи: 2 рядки по 2 елементи
                    int row = i / 2;
                    int column = i % 2;
                    float x = (parentWidth - 2 * cellSize.x - spacing.x) / 2 + column * (cellSize.x + spacing.x);
                    float y = -row * (cellSize.y + spacing.y);
                    SetChildPosition(child, x, y);
                }
                else if (childCount == 5)
                {
                    if (i == 4)
                    {
                        // Останній (5-й) елемент внизу посередині
                        float x = (parentWidth - cellSize.x) / 2;
                        float y = -(2 * (cellSize.y + spacing.y));
                        SetChildPosition(child, x, y);
                    }
                    else
                    {
                        // Перші 4 елементи: 2 рядки по 2 елементи
                        int row = i / 2;
                        int column = i % 2;
                        float x = (parentWidth - 2 * cellSize.x - spacing.x) / 2 + column * (cellSize.x + spacing.x);
                        float y = -row * (cellSize.y + spacing.y);
                        SetChildPosition(child, x, y);
                    }
                }
            }
        }

        private void SetChildPosition(RectTransform child, float x, float y)
        {
            child.anchorMin = new Vector2(0, 1);
            child.anchorMax = new Vector2(0, 1);
            child.pivot = new Vector2(0, 1);
            child.anchoredPosition = new Vector2(x, y);
        }
    }
}