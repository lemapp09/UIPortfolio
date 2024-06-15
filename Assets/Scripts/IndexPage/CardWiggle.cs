using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace LemApperson_UIPortfolio
{
    public class CardWiggle : MonoBehaviour
    {

        public void WiggleCards(VisualElement[] cards)
        {
            StartCoroutine(WiggleRandomCard(cards));
        }
        
        private IEnumerator WiggleRandomCard(VisualElement[] _cards)
        {
            float _wiggleAmountX, _wiggleAmountY;
            while (true)
            {
                // Random delay before the next wiggle
                float delay = Random.Range(1f, 3f);
                yield return new WaitForSeconds(delay);

                // Select a random card
                int randomIndex = Random.Range(0, _cards.Length);
                VisualElement card = _cards[randomIndex];

                // Random wiggle duration
                float wiggleDuration = Random.Range(1f, 3f);
                float elapsed = 0f;

                Vector3 originalPosition = card.transform.position;
                Quaternion originalRotation = card.transform.rotation;
                while (elapsed < wiggleDuration)
                {
                    _wiggleAmountX = Mathf.Sin(Time.time * 10) * 7; // Adjust the wiggle speed and intensity here
                    _wiggleAmountY = Mathf.Cos(Time.time * 7) * 5;

                    card.transform.position = originalPosition + new Vector3(_wiggleAmountX, _wiggleAmountY, 0);
                    elapsed += Time.deltaTime;
                    yield return null;
                }

                // Reset the card position after wiggle
                card.transform.position = originalPosition;
                card.transform.rotation = originalRotation;
            }
        }
    }
}