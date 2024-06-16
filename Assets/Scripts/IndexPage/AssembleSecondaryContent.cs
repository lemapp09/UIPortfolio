using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace LemApperson_UIPortfolio
{
    public struct AssembleSecondaryContent
    {
        private static MenuSettings _menuSettings;
        private UIDocument _indexPage;
        private StyleSheet _indexPageStyle;
        private int _screenWidth;
        private float _numOfColumns;
        private VisualElement _root, _body, _sidebar, _mainContent;
        private static VisualElement[] _secondaryCards;

        public static (VisualElement, VisualElement[], VisualElement ) MainContentAssembler(int numberofColumns,
            bool darkMode, string[,] secondaryContentItems)
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            if (secondaryContentItems == null) Debug.LogError("Secondary Content Items not found");

            _secondaryCards = new VisualElement[8];
            var secondaryContent = new VisualElement();

            //Prepare Alternative Label
            var alternativeLabel = new Label
            {
                name = "Alternative Label",
                text = secondaryContentItems[0, 5]
            };

            secondaryContent.name = "secondary-content";
            if (darkMode)
            {
                secondaryContent.AddToClassList("main-content-darkMode");
                alternativeLabel.AddToClassList("card-alternative-label-darkMode");
            }
            else
            {
                secondaryContent.AddToClassList("main-content");
                alternativeLabel.AddToClassList("card-alternative-label");
            }

            int i = 0;
            // There are 8 secondary content items each with a different icon and label
            // The number of rows is 8 / number of Columns

            for (int k = 0; k < (8 / (numberofColumns - 1)); k++)
            {
                // Create a row
                var row = new VisualElement();
                row.name = "row" + k;
                row.style.width = Length.Percent(100);
                row.style.flexDirection = FlexDirection.Row;
                for (int l = 0; l < (numberofColumns - 1); l++)
                {
                    var card = new VisualElement();
                    card.AddToClassList("card");
                    card.AddToClassList("secondary-card");
                    int cardIndex = i % 3 + 1;
                    string modeSuffix = darkMode ? "-darkMode" : "";
                    card.AddToClassList($"card{cardIndex}{modeSuffix}");

                    if (l == 0) card.style.marginLeft = 20f;
                    if (l == numberofColumns - 2) card.style.marginRight = 20f;
                    card.style.width = Length.Percent(100f / (numberofColumns - 1f));
                    card.name = "card" + secondaryContentItems[i, 3];

                    if (secondaryContentItems[i, 0] != "") // 1st card is blank
                    {
                        var image = new VisualElement();

                        // add image to card
                        image.AddToClassList("card-image");
                        image.style.backgroundImage = Resources.Load<Texture2D>(secondaryContentItems[i, 1]);
                        card.Add(image);

                        // add text to card
                        var text = new Label(secondaryContentItems[i, 2]);
                        text.AddToClassList("card-text");
                        card.Add(text);

                        // add clickable response to open scene
                        if (secondaryContentItems[i, 6] != "")
                        {
                            var webLink = secondaryContentItems[i, 6];// Define the event handler
                            EventCallback<MouseUpEvent> mouseUpEventHandler = evt =>
                            {
                                AudioManager.PlaySFXSound();
                                Application.OpenURL(webLink);
                            };
                            card.RegisterCallback(mouseUpEventHandler);
                            MouseEventManager.Instance.RegisterEvent(card, mouseUpEventHandler);
                        }
                        else if (secondaryContentItems[i, 3] != "")
                        {
                            var sceneName = secondaryContentItems[i, 3];
                            var i1 = i;// Define the event handler
                            EventCallback<MouseUpEvent> mouseUpEventHandler = evt =>
                            {
                                AudioManager.PlaySFXSound();
                                BaseTertiaryPage.BuildPage(sceneName);
                            };

                            // Register the event handler with the card element
                            card.RegisterCallback(mouseUpEventHandler);

                            // Register the event handler with the event manager
                            MouseEventManager.Instance.RegisterEvent(card, mouseUpEventHandler);
                        }
                        else
                        {
                            
                            EventCallback<MouseUpEvent> mouseUpEventHandler = evt =>
                            {
                                AudioManager.PlaySFXSound();
                            };

                            // Register the event handler with the card element
                            card.RegisterCallback(mouseUpEventHandler);

                            // Register the event handler with the event manager
                            MouseEventManager.Instance.RegisterEvent(card, mouseUpEventHandler);
                        }

                        #region Tooltip Hidden Element

                        // add hidden element to store the text for the tooltip
                        VisualElement hiddenElement = new VisualElement();
                        hiddenElement.AddToClassList("hidden-element");
                        hiddenElement.name = "hiddenElement" + secondaryContentItems[i, 3];
                        hiddenElement.style.width = Length.Percent(100);

                        // resources icons/main-menu.png
                        var icon = new VisualElement();
                        icon.AddToClassList("hidden-element-image");
                        icon.style.backgroundImage = Resources.Load<Texture2D>(secondaryContentItems[i, 4]);
                        hiddenElement.Add(icon);

                        Label hiddenElementLabel1 = new Label();
                        hiddenElementLabel1.AddToClassList("hidden-element-label1");
                        hiddenElementLabel1.text = secondaryContentItems[i, 2];
                        hiddenElement.Add(hiddenElementLabel1);

                        Label hiddenElementLabel2 = new Label();
                        hiddenElementLabel2.AddToClassList("hidden-element-label2");
                        hiddenElementLabel2.text = secondaryContentItems[i, 5];
                        hiddenElement.Add(hiddenElementLabel2);

                        card.Add(hiddenElement);

                        #endregion
                    }

                    if (i == 0)
                    {
                        card.Add(alternativeLabel);
                    }

                    row.Add(card);
                    _secondaryCards[i] = card;
                    i++;
                }

                secondaryContent.Add(row);
            }

            return (secondaryContent, _secondaryCards, alternativeLabel);
        }
    }
}


/*
 * How can I change what element is focused next when using directional navigation?

   You can configure directional navigation to have other targets other than the default ones.

   The following code example allows element A to navigate to elements U, D, L, R when navigating up, down, left, and right respectively:

   A.RegisterCallback <NavigationMoveEvent>(e =>
   {
       switch(e.direction)
       {
           case NavigationMoveEvent.Direction.Up: U.Focus(); break;
           case NavigationMoveEvent.Direction.Down: D.Focus(); break;
           case NavigationMoveEvent.Direction.Left: L.Focus(); break;
           case NavigationMoveEvent.Direction.Right: R.Focus(); break;
       }
       e.PreventDefault();
   });

   Second, I've found that implementing some checks within the Focus Events (FocusEvent, FocusOutEvent, FocusInEvent, etc.) can be useful to decide whether the navigation should be allowed. For example, I decided to add an empty/dummy "Focus Boundary" element on the edges of certain UI screens that will automatically return navigation to nearby elements.

   The following code sets up the FocusEvent for all VisualElements with the name "FocusBoundary" so that the focus is immediately returned to the prior focused element:
   Code (CSharp):

       masterMenuRoot.Query<VisualElement>("FocusBoundary").ForEach((element) => element.RegisterCallback<FocusEvent>(OnBoundaryFocus));

       private void OnBoundaryFocus(FocusEvent evt)
       {
           evt.relatedTarget.Focus();
       }

   This is useful for edge cases (literally) where the default navigation behavior of UI Toolkit can be strange/buggy.

*/