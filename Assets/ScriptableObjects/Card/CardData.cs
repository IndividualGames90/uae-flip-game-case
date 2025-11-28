using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;

namespace IndividualGames.CardMatch.Card
{
    [CreateAssetMenu(fileName = "CardData", menuName = "IndividualGames/CardMatch/Card Data")]
    public class CardData : ScriptableObject
    {
        [Header("Sprites")]
        public Sprite FrontFace;
        public Sprite BackFace;

        [Header("Generated Card ID (Read Only)")]
        [SerializeField] private int cardID;
        public int CardID => cardID;

        private void OnValidate()
        {
            if (FrontFace == null)
            {
                cardID = 0;
                return;
            }

            // Hash sprite name → int
            cardID = GenerateSpriteNameHash(FrontFace.name);

            // Ensure it's never short/accidental zero
            if (cardID < 100000)  // pad threshold
                cardID = cardID * 123457 + 98765;
        }

        private int GenerateSpriteNameHash(string input)
        {
            // MD5 gives stable deterministic output
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = md5.ComputeHash(bytes);

                // Take first 4 bytes → int
                return Math.Abs(BitConverter.ToInt32(hash, 0));
            }
        }
    }
}
