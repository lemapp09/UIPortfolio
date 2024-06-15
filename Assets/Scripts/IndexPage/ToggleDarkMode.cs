using UnityEngine.UIElements;

namespace LemApperson_UIPortfolio
{
    public struct ToggleDarkMode
    {
        public void ToggleCardElements(VisualElement[] cards, bool isDarkMode)
        {
            foreach (var card in cards)
            {
                if (isDarkMode)
                {
                    // Add appropriate dark mode class based on current class
                    if (card.ClassListContains("card1"))
                    {
                        card.RemoveFromClassList("card1");
                        card.AddToClassList("card1-darkMode");
                    }
                    else if (card.ClassListContains("card2"))
                    {
                        card.RemoveFromClassList("card2");
                        card.AddToClassList("card2-darkMode");
                    }
                    else if (card.ClassListContains("card3"))
                    {
                        card.RemoveFromClassList("card3");
                        card.AddToClassList("card3-darkMode");
                    }
                }
                else
                {
                    // Revert to original classes
                    if (card.ClassListContains("card1-darkMode"))
                    {
                        card.RemoveFromClassList("card1-darkMode");
                        card.AddToClassList("card1");
                    }
                    else if (card.ClassListContains("card2-darkMode"))
                    {
                        card.RemoveFromClassList("card2-darkMode");
                        card.AddToClassList("card2");
                    }
                    else if (card.ClassListContains("card3-darkMode"))
                    {
                        card.RemoveFromClassList("card3-darkMode");
                        card.AddToClassList("card3");
                    }
                }
            }
        }

        public void ToggleMainContent(VisualElement mainContent, bool isDarkMode)
        {
            if (isDarkMode)
            {
                mainContent.RemoveFromClassList("main-content");
                mainContent.AddToClassList("main-content-darkMode");
            }
            else
            {
                mainContent.RemoveFromClassList("main-content-darkMode");
                mainContent.AddToClassList("main-content");
            }
        }

        public void ToggleAlternativeLabel(VisualElement alternativeLabel, bool isDarkMode)
        {
            if (isDarkMode)
            {
                alternativeLabel.RemoveFromClassList("card-alternative-label");
                alternativeLabel.AddToClassList("card-alternative-label-darkMode");
            }
            else
            {
                alternativeLabel.RemoveFromClassList("card-alternative-label-darkMode");
                alternativeLabel.AddToClassList("card-alternative-label");
            }
        }
    }
}