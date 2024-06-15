using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace LemApperson_UIPortfolio
{
    public struct AssembleSideBar
    {
        private static MenuSettings _menuSettings;
        private int _screenWidth;
        private float _numOfColumns;
        private VisualElement _root, _body, _sidebar, _mainContent;
        private static string[,] _sidebarItems;

        public static VisualElement SideBarAssembler(bool isMainPage, string headline)
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");

            var sidebar = new VisualElement();
            _sidebarItems = _menuSettings.GetSidebarItems();
            sidebar.AddToClassList("side-bar");

            var header = new Label("<line-height=70%>" + headline + "</line-height>");
            header.AddToClassList("side-bar-header");
            header.AddToClassList("wrap");
            sidebar.Add(header);

            // There are 12 content items each with a different icon and label
            for (int i = 0; i < 13; i++)
            {
                if (isMainPage && i == 0)
                {
                    continue;
                }

                var content = new VisualElement();
                content.AddToClassList("side-bar-content");
                if (!isMainPage && i == 0)
                {
                    // Add bottom padding of 20px
                    content.style.paddingBottom = 20f;
                }

                // resources icons/main-menu.png
                var icon = new VisualElement();
                icon.AddToClassList("side-bar-content-image");
                icon.style.backgroundImage = Resources.Load<Texture2D>(_sidebarItems[i, 0]);
                content.Add(icon);

                var label = new Label(_sidebarItems[i, 1]);
                label.AddToClassList("side-bar-content-label");
                label.AddToClassList("wrap");


                // Play SFX sound when clicked, load scene
                var i1 = i ;
                content.RegisterCallback<MouseUpEvent>(evt =>
                {
                    AudioManager.PlaySFXSound();
                    if (i1 == 0)
                    {
                        IndexPageLayout.BuildPage();
                    }
                    else
                    {
                        BaseSecondaryPage.BuildSecondaryPage(i1);
                    }
                });
                content.name = _sidebarItems[i, 1];
                content.Add(label);
                sidebar.Add(content);
            }

            var footer = new Label("Lem Apperson<br>LemApp Studios<br>Copyright\u00a9 2024");
            footer.AddToClassList("side-bar-footer");
            sidebar.Add(footer);

            // Create the Dark Mode toggle button
            var darkModeButton = new Button();
            darkModeButton.text = "Toggle Dark Mode";
            darkModeButton.clicked += ToggleDarkMode;
            darkModeButton.AddToClassList("side-bar-dark-mode-button");
            // Play Click Sound when button is clicked
            darkModeButton.RegisterCallback<MouseUpEvent>(evt => { AudioManager.PlaySFXSound(); });
            sidebar.Add(darkModeButton);

            return sidebar;
        }

        private static void ToggleDarkMode()
        {
            _menuSettings.ToggleDarkMode();
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