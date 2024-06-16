using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace LemApperson_UIPortfolio
{
    public class BaseSecondaryPage : MonoBehaviour
    {
        #region parameters

        private static UIDocument _indexPage;
        private static StyleSheet _indexPageStyle;
        private SecondaryPages _secondaryPages;
        private static int _numOfColumns;
        private static VisualElement _root;
        private static VisualElement _body;
        private static VisualElement _sidebar;
        private static VisualElement _secondaryContent;
        private static VisualElement _alternativeLabel;
        private static VisualElement[] _secondaryCards = new VisualElement[8];
        private static bool _darkMode = true;
        private static MenuSettings _menuSettings;
        private static ToggleDarkMode _toggleDarkMode;

        #endregion


        private void OnEnable()
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _darkMode = _menuSettings.GetDarkMode();
        }


        public static void BuildSecondaryPage(int contentIndex)
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _indexPage = _menuSettings.GetIndexPage();
            if(_indexPage == null) Debug.LogError("Index Page not found");
            _indexPageStyle = _menuSettings.GetIndexPageStyle();
            if(_indexPageStyle == null) Debug.LogError("Index Page Style not found");
            _menuSettings.OnDarkModeChanged += UpdateElementTheme;
            _numOfColumns = Screen.width / 325;
            if (_numOfColumns < 2) _numOfColumns = 2;

            // Unregister all events
            MouseEventManager.Instance.UnregisterAllEvents();

            // Clear all visual elements from the root
            _indexPage.rootVisualElement.Clear();
            _root = _indexPage.rootVisualElement;
            _root.styleSheets.Add(_indexPageStyle);
            _root.AddToClassList("root");

            _body = new VisualElement();
            _body.AddToClassList("body");
            _root.Add(_body);

            _sidebar = AssembleSideBar.SideBarAssembler(false, GetHeadline(contentIndex));
            _body.Add(_sidebar);
            _secondaryContent = new VisualElement();
            _secondaryContent.AddToClassList("main-content");

            var contentItems = _menuSettings.GetSecondaryContentItems(contentIndex );
            if (contentItems == null) Debug.LogError("Content Items not found, index: " + (contentIndex ));
            var buildStuff =
                AssembleSecondaryContent.MainContentAssembler(_numOfColumns, _menuSettings.GetDarkMode(),
                    contentItems );
            _secondaryContent = buildStuff.Item1;
            _secondaryCards = buildStuff.Item2;
            _alternativeLabel = buildStuff.Item3;
            _body.Add(_secondaryContent);
            _sidebar.style.width = Length.Percent((1.0f / _numOfColumns) * 100.0f);
            _secondaryContent.style.width = Length.Percent((1 - 1.0f / _numOfColumns) * 100.0f);

            // Add fade-in panel   
            var fadeInPanel = new VisualElement();
            fadeInPanel.AddToClassList("fade-panel");
            _root.Add(fadeInPanel);
            FadePanel(fadeInPanel);
        }

        private static void FadePanel(VisualElement FadePanel)
        {
            //transition fadePanel opacity from 1 to 0 over 1 second
            FadePanel.style.opacity = 1;
            FadePanel.schedule.Execute(() => FadePanel.style.opacity = 0).ForDuration(1000);
            FadePanel.schedule.Execute(() => FadePanel.RemoveFromHierarchy()).StartingIn(1100);
        }

        private static void UpdateElementTheme(bool darkMode)
        {
            _toggleDarkMode.ToggleCardElements(_secondaryCards, darkMode);
            _toggleDarkMode.ToggleMainContent(_secondaryContent, darkMode);
            _toggleDarkMode.ToggleAlternativeLabel(_alternativeLabel, darkMode);
        }

        public static string GetHeadline(int contentIndex)
        {
            switch (contentIndex)
            {
                case 0:
                    return "Home";
                case 1:
                    return "MainMenu";
                case 2:
                    return "HUD (Heads Up Display)";
                case 3:
                    return "Inventory";
                case 4:
                    return "Dialogue";
                case 5:
                    return "In Game Shop";
                case 6:
                    return "Settings";
                case 7:
                    return "Loading Screen";
                case 8:
                    return "Achievements";
                case 9:
                    return "PauseMenu";
                case 10:
                    return "Tutorials";
                case 11:
                    return "Articles";
                case 12:
                    return "Contact";
                default:
                    return "";
            }
        }

        private void OnDisable()
        {
            _menuSettings.OnDarkModeChanged -= UpdateElementTheme;
        }
    }
}

enum SecondaryPages
{
    MainMenu,
    HUD,
    Inventory,
    Dialogue,
    InGameShop,
    Settings,
    LoadingScreen,
    Achievements,
    PauseMenu,
    Tutorials,
    Articles,
    Contact
}