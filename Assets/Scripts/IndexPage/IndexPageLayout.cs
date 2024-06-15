using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace LemApperson_UIPortfolio
{
    public class IndexPageLayout : MonoBehaviour
    {
        private static MenuSettings _menuSettings;
        private static UIDocument _indexPage;
        private static StyleSheet _indexPageStyle;
        private static ToggleDarkMode _toggleDarkMode;
        private static int _numOfColumns;
        private static VisualElement _root, _body, _sidebar, _mainContent;
        private static bool _darkMode = true;
        private static VisualElement[] _cards = new VisualElement[12];
        private static float _wiggleAmountX, _wiggleAmountY;
        

        private void Start()
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            _indexPage = GetComponent<UIDocument>();
            if(_indexPage == null) Debug.LogError("Index Page not found");
            _indexPageStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UI Toolkit/StyleSheets/IndexStyleSheet.uss");
            if(_indexPageStyle == null) Debug.LogError("Index Page Style not found");
            _menuSettings.OnDarkModeChanged += UpdateElementTheme;
            _menuSettings.SetIndexPage(_indexPage);
            _menuSettings.SetIndexPageStyle(_indexPageStyle);
            BuildPage();
        }

        public static void BuildPage()
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _darkMode = _menuSettings.GetDarkMode();

            var _numOfColumns = Screen.width / 325;
            if (_numOfColumns < 2) _numOfColumns = 2;
            
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _indexPage = _menuSettings.GetIndexPage();
            _indexPageStyle = _menuSettings.GetIndexPageStyle();
            if(_indexPage == null) Debug.LogError("Index Page not found");
            if(_indexPage == null) Debug.LogError("Index Page Style not found");

            _indexPage.rootVisualElement.Clear();
            _root = _indexPage.rootVisualElement;
            _root.styleSheets.Add(_indexPageStyle);
            _root.AddToClassList("root");
            _body = new VisualElement();
            _body.AddToClassList("body");
            _root.Add(_body);

            // Assemble the sidebar
            _sidebar = AssembleSideBar.SideBarAssembler(true, "Lem Apperson - Portfolio");
            _body.Add(_sidebar);

            // Assemble the main content
            _mainContent = new VisualElement();
            _mainContent.AddToClassList("main-content");
            var buildStuff = AssembleMainContent.MainContentAssembler(_numOfColumns, _darkMode);
            _mainContent = buildStuff.Item1;
            _cards = buildStuff.Item2;
            _body.Add(_mainContent);

            // Adjust the sidebar and main content to the screen size
            _sidebar.style.width = Length.Percent((1.0f / _numOfColumns) * 100.0f);
            _mainContent.style.width = Length.Percent((1 - 1.0f / _numOfColumns) * 100.0f);

            // Wiggle the cards
            // StartCoroutine(WiggleCard());
        }

        private void UpdateElementTheme(bool darkMode)
        {
            _toggleDarkMode.ToggleCardElements(_cards, darkMode);
            _toggleDarkMode.ToggleMainContent(_mainContent, darkMode);
        }
        
        private IEnumerator WiggleCard()
        {
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

        private void OnDisable()
        {
            _menuSettings.OnDarkModeChanged -= UpdateElementTheme;
        }
    }
}