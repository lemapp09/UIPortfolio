using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace LemApperson_UIPortfolio
{
    public struct AssembleMainContent
    {
        private static MenuSettings _menuSettings;
        private UIDocument _indexPage;
        private StyleSheet _indexPageStyle;
        private int _screenWidth;
        private float _numOfColumns;
        private VisualElement _root, _body, _sidebar, _mainContent;
        private static string[,] _contentItems;
        private static VisualElement[] _cards;

        public static (VisualElement, VisualElement[] ) MainContentAssembler(int numberofColumns, bool darkMode)
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if(_menuSettings == null) Debug.LogError("Menu Settings not found");
            darkMode = _menuSettings.GetDarkMode();
            
            _cards = new VisualElement[12];
            
            var maincontent = new VisualElement();
            _contentItems = new string[12, 4]
            {
                { "Textures/Images", "Textures/Images/image1", "Main Menu", "main-menu" },
                { "Textures/icons/hud", "Textures/Images/image2", "HUD (Heads-Up Display)", "hud" },
                { "Textures/icons/inventory", "Textures/Images/image3", "Inventory System", "inventory" },
                { "Textures/icons/dialogue", "Textures/Images/image4", "Dialogue System", "dialogue" },
                { "Textures/icons/in-game-shop", "Textures/Images/image5", "In-Game Shop", "in-game-shop" },
                { "Textures/icons/settings", "Textures/Images/image6", "Settings Menu", "settings" },
                { "Textures/icons/loading-screen", "Textures/Images/image7", "Loading Screen", "loading-screen" },
                { "Textures/icons/achievement", "Textures/Images/image8", "Achievement System", "achievement" },
                { "Textures/icons/pause-menu", "Textures/Images/image9", "Pause Menu", "pause-menu" },
                { "Textures/icons/tutorial", "Textures/Images/image10", "Tutorial Overlay", "tutorial" },
                { "Textures/icons/unity-articles", "Textures/Images/image11", "Unity Articles", "articles" },
                { "Textures/icons/contact", "Textures/Images/image12", "Contact", "contact" }
            };
            maincontent.name = "main-content";
            if (darkMode) { maincontent.AddToClassList("main-content-darkMode");}
            else {   maincontent.AddToClassList("main-content"); }
            

            int i = 0;
            // There are 12 content items each with a different icon and label
            // The number of rows is 12 / number of Columns

            for (int k = 0; k < (12 / (numberofColumns - 1)); k++)
            {
                var row = new VisualElement();
                row.name = "row" + k;
                row.style.width = Length.Percent(100);
                row.style.flexDirection = FlexDirection.Row;
                for (int l = 0; l < (numberofColumns - 1); l++)
                {
                    var card = new VisualElement();
                    card.AddToClassList("card");
                    switch (i % 3)
                    {
                        case 0:
                            if (darkMode)
                            {
                                card.AddToClassList("card1-darkMode");
                            }
                            else
                            {
                                card.AddToClassList("card1");
                            }
                            break;
                        case 1:
                            if (darkMode)
                            {
                                card.AddToClassList("card2-darkMode");
                            }
                            else
                            {
                                card.AddToClassList("card2");
                            }
                            break;
                        case 2:
                            if (darkMode)
                            {
                                card.AddToClassList("card3-darkMode");
                            }
                            else
                            {
                                card.AddToClassList("card3");
                            }
                            break;
                    }

                    if (l == 0) card.style.marginLeft = 20f;
                    if (l == numberofColumns - 2) card.style.marginRight = 20f;
                    card.style.width = Length.Percent(100 / (numberofColumns - 1));
                    var image = new VisualElement();
                    image.AddToClassList("card-image");
                    image.style.backgroundImage = Resources.Load<Texture2D>(_contentItems[i, 1]);
                    card.Add(image);
                    var text = new Label(_contentItems[i, 2]);
                    text.AddToClassList("card-text");
                    card.Add(text);
                    card.name = "card" + _contentItems[i, 3];
                    // add clickable response to open scene
                    var sceneName = _contentItems[i, 3];
                    card.RegisterCallback<MouseUpEvent>(evt => { 
                        AudioManager.PlaySFXSound();
                        SceneManager.LoadScene(sceneName); });

                    // add hidden element to store the text for the tooltip
                    VisualElement hiddenElement = new VisualElement();
                    hiddenElement.AddToClassList("hidden-element");
                    card.style.width = Length.Percent(100 / (numberofColumns - 1));
                    Label hiddenElementLabel = new Label();
                    hiddenElementLabel.AddToClassList("hidden-element-label");
                    hiddenElementLabel.name = "hiddenElement" + _contentItems[i, 3];
                    hiddenElementLabel.text = _contentItems[i, 2];
                    hiddenElement.Add(hiddenElementLabel);
                    card.Add(hiddenElement);
                    row.Add(card);
                    _cards[i] = card;
                    i++;
                }
                maincontent.Add(row);
            }
            return (maincontent, _cards);
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