using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace LemApperson_UIPortfolio
{

    public abstract class BaseTertiaryPage : MonoBehaviour
    {
        #region parameters

        [SerializeField] protected UIDocument _indexPage;
        [SerializeField] protected StyleSheet _indexPageStyle;
        protected int _numOfColumns;
        protected VisualElement _root, _body, _sidebar, _tertiaryContent, _alternativeLabel;
        protected VisualElement[] _tertiaryCards = new VisualElement[8];
        protected bool _darkMode = true;
        protected static MenuSettings _menuSettings;
        protected ToggleDarkMode _toggleDarkMode;
        protected static string[,] _tertiaryContentItems;

        #endregion

        protected abstract string Headline { get; }
        protected abstract string[,] tertiaryContentItems { get; }

        private void OnEnable()
        {
            _tertiaryContentItems = tertiaryContentItems;
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _darkMode = _menuSettings.GetDarkMode();
            _menuSettings.OnDarkModeChanged += UpdateElementTheme;
        }

        private void Start()
        {
            _numOfColumns = Screen.width / 325;
            if (_numOfColumns < 2) _numOfColumns = 2;

            _root = _indexPage.rootVisualElement;
            _root.styleSheets.Add(_indexPageStyle);
            _root.AddToClassList("root");
            _body = new VisualElement();
            _body.AddToClassList("body");
            _root.Add(_body);

            _sidebar = AssembleSideBar.SideBarAssembler(false, Headline);
            _body.Add(_sidebar);
            _tertiaryContent = new VisualElement();
            _tertiaryContent.AddToClassList("main-content");

            var buildStuff =
                AssembleSecondaryContent.MainContentAssembler(_numOfColumns, _darkMode, _tertiaryContentItems);
            _tertiaryContent = buildStuff.Item1;
            _tertiaryCards = buildStuff.Item2;
            _alternativeLabel = buildStuff.Item3;
            _body.Add(_tertiaryContent);
            _sidebar.style.width = Length.Percent((1.0f / _numOfColumns) * 100.0f);
            _tertiaryContent.style.width = Length.Percent((1 - 1.0f / _numOfColumns) * 100.0f);
        }

        private void UpdateElementTheme(bool darkMode)
        {
            _toggleDarkMode.ToggleCardElements(_tertiaryCards, darkMode);
            _toggleDarkMode.ToggleMainContent(_tertiaryContent, darkMode);
            _toggleDarkMode.ToggleAlternativeLabel(_alternativeLabel, darkMode);
        }

        private void OnDisable()
        {
            _menuSettings.OnDarkModeChanged -= UpdateElementTheme;
        }
    }
}