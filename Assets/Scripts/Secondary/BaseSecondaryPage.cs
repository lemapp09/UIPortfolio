using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace LemApperson_UIPortfolio 
{

    public  class BaseSecondaryPage : MonoBehaviour
    {
        #region parameters

        [SerializeField]  private UIDocument _indexPage;
        [SerializeField]  private StyleSheet _indexPageStyle;
        [SerializeField]  private SecondaryPages _secondaryPages;
         private int _numOfColumns, _contentIndex;
         private VisualElement _root, _body, _sidebar, _secondaryContent, _alternativeLabel;
         private VisualElement[] _secondaryCards = new VisualElement[8];
         private bool _darkMode = true;
         private MenuSettings _menuSettings;
         private ToggleDarkMode _toggleDarkMode;

        #endregion


        private  void OnEnable()
        {
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _darkMode = _menuSettings.GetDarkMode();
            _menuSettings.OnDarkModeChanged += UpdateElementTheme;
        }

        private  void Start()
        {
            BuildSecondaryPage(_secondaryPages.GetHashCode());
        }

        public  void BuildSecondaryPage(int _contentIndex)
        {
            _numOfColumns = Screen.width / 325;
            if (_numOfColumns < 2) _numOfColumns = 2;

            _root = _indexPage.rootVisualElement;
            _root.styleSheets.Add(_indexPageStyle);
            _root.AddToClassList("root");
            _body = new VisualElement();
            _body.AddToClassList("body");
            _root.Add(_body);

            _sidebar = AssembleSideBar.SideBarAssembler(false, GetHeadline(_contentIndex));
            _body.Add(_sidebar);
            _secondaryContent = new VisualElement();
            _secondaryContent.AddToClassList("main-content");

            var buildStuff =
                AssembleSecondaryContent.MainContentAssembler(_numOfColumns, _darkMode, _menuSettings.GetSecondaryContentItems(_contentIndex));
            _secondaryContent = buildStuff.Item1;
            _secondaryCards = buildStuff.Item2;
            _alternativeLabel = buildStuff.Item3;
            _body.Add(_secondaryContent);
            _sidebar.style.width = Length.Percent((1.0f / _numOfColumns) * 100.0f);
            _secondaryContent.style.width = Length.Percent((1 - 1.0f / _numOfColumns) * 100.0f);
        }

        private  void UpdateElementTheme(bool darkMode)
        {
            _toggleDarkMode.ToggleCardElements(_secondaryCards, darkMode);
            _toggleDarkMode.ToggleMainContent(_secondaryContent, darkMode);
            _toggleDarkMode.ToggleAlternativeLabel(_alternativeLabel, darkMode);
        }    
        public  string GetHeadline(int contentIndex){

            switch (contentIndex)
            {
                case 0:
                    return "Achievements"; 
                case 1:
                    return "Articles";
                case 2:
                    return "Contact";
                case 3:
                    return "Dialogue";
                case 4:
                    return "HUD (Heads Up Display)";
                case 5:
                    return "In Game Shop";
                case 6:
                    return "Inventory";
                case 7:
                    return "Loading Screen";
                case 8:
                    return "MainMenu";
                case 9:
                    return "PauseMenu";
                case 10:
                    return "Settings";
                case 11:
                    return "Tutorials";
                default:
                    return "";
            }
        }

        private  void OnDisable()
        {
            _menuSettings.OnDarkModeChanged -= UpdateElementTheme;
        }
    }
}

enum SecondaryPages
{
    Achievements,
    Articles,
    Contact,
    Dialogue,
    HUD,
    InGameShop,
    Inventory,
    LoadingScreen,
    MainMenu,
    PauseMenu,
    Settings,
    Tutorials

}