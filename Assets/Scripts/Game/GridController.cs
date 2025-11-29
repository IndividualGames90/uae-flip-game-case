using IndividualGames.CardMatch.Card;
using UnityEngine;

namespace IndividualGames.CardMatch.Game
{
    public class GridController : MonoBehaviour
    {
        public enum GridPreset
        {
            Grid2x2,
            Grid2x3,
            Grid5x6,
            Custom
        }

        [Header("Preset")]
        [SerializeField] private GridPreset gridPreset = GridPreset.Grid2x2;

        [Header("Custom Grid( Requires one EVEN number )")]
        [SerializeField] private int customRows = 2;
        [SerializeField] private int customColumns = 2;

        [Header("References")]
        [SerializeField] private RectTransform container;
        [SerializeField] private CardEventController cardEventController;

        [Header("Prefab")]
        [SerializeField] private GameObject cardPrefab;

        [Header("Card Data (used for pairs)")]
        [SerializeField] private CardData[] cardDatas;

        [Header("Card Aspect Ratio")]
        [SerializeField] private float cardAspect = 0.7f;

        [Header("Shuffle Settings")]
        [SerializeField] private int shuffleAmount = 10;

        private CardController[,] cardSlots;
        private int rows;
        private int columns;

        private void Start()
        {
            ApplyPreset();
            BuildGrid();
            PositionCards();
        }

        private void ApplyPreset()
        {
            switch (gridPreset)
            {
                case GridPreset.Grid2x2:
                    rows = 2; columns = 2;
                    break;
                case GridPreset.Grid2x3:
                    rows = 2; columns = 3;
                    break;
                case GridPreset.Grid5x6:
                    rows = 5; columns = 6;
                    break;
                case GridPreset.Custom:
                default:
                    rows = Mathf.Max(1, customRows);
                    columns = Mathf.Max(1, customColumns);
                    break;
            }
        }

        private void BuildGrid()
        {
            ClearOldCards();

            int totalSlots = rows * columns;

            if (totalSlots % 2 != 0)
            {
                Debug.LogError("Grid must have an EVEN number of slots for pairing!");
                return;
            }

            int requiredPairs = totalSlots / 2;

            if (cardDatas.Length == 0)
            {
                Debug.LogError("CardDatas array is EMPTY!");
                return;
            }

            var list = new System.Collections.Generic.List<CardData>(totalSlots);

            for (int i = 0; i < requiredPairs; i++)
            {
                CardData data = cardDatas[i % cardDatas.Length];
                list.Add(data);
                list.Add(data);
            }

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            cardSlots = new CardController[rows, columns];
            int index = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    GameObject go = Instantiate(cardPrefab, container);
                    CardController card = go.GetComponent<CardController>();

                    card.Initialize(list[index]);
                    cardEventController.RegisterCard(card);
                    cardSlots[r, c] = card;

                    index++;
                }
            }
        }


        private void PositionCards()
        {
            Vector2 area = container.rect.size;

            float cellW = area.x / columns;
            float cellH = area.y / rows;

            float finalW = cellW;
            float finalH = finalW / cardAspect;

            if (finalH > cellH)
            {
                finalH = cellH;
                finalW = finalH * cardAspect;
            }

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var card = cardSlots[r, c];
                    RectTransform rt = card.GetComponent<RectTransform>();

                    float posX = (c * cellW) + (cellW - finalW) * 0.5f;
                    float posY = (r * cellH) + (cellH - finalH) * 0.5f;

                    rt.anchorMin = new Vector2(0, 1);
                    rt.anchorMax = new Vector2(0, 1);
                    rt.pivot = new Vector2(0, 1);

                    rt.anchoredPosition = new Vector2(posX, -posY);
                    rt.sizeDelta = new Vector2(finalW, finalH);
                }
            }
        }

        private void ClearOldCards()
        {
            for (int i = container.childCount - 1; i >= 0; i--)
                Destroy(container.GetChild(i).gameObject);
        }
    }
}
