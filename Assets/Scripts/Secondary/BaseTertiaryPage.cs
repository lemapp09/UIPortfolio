using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace LemApperson_UIPortfolio
{
    public static class BaseTertiaryPage 
    {
        #region parameters

        private static VisualElement _root;
        private static MenuSettings _menuSettings;
        private static UIDocument _indexPage;
        private static StyleSheet _indexPageStyle;

        #endregion

        public static void BuildPage(int exampleId)
        {
            // begin the root document
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _indexPage = _menuSettings.GetIndexPage();
            _indexPageStyle = _menuSettings.GetIndexPageStyle();
            if(_indexPage == null) Debug.LogError("Index Page not found");
            if(_indexPage == null) Debug.LogError("Index Page Style not found");
            _indexPage.rootVisualElement.Clear();
            _root = _indexPage.rootVisualElement;
            _root.AddToClassList("root");
            _root.styleSheets.Add(_indexPageStyle);

            // floating element, above the main content
            // when click, will show the side bar
            var _floatingElement = new VisualElement();
            _floatingElement.AddToClassList("floating-element");
            _floatingElement.AddToClassList("floating-element");
            if (_menuSettings.GetDarkMode()) _floatingElement.AddToClassList("floating-element-darkMode");
            else _floatingElement.AddToClassList("floating-element-lightMode");
            _floatingElement.RegisterCallback<MouseUpEvent>(evt => { AudioManager.PlaySFXSound(); });

            var _floatingElementText = new Label("Menu");
            _floatingElementText.name = "floating-element-text";
            _floatingElement.Add(_floatingElementText);
            _root.Add(_floatingElement);

            // holder for the side bar
            var _sideBarHolder = new VisualElement();
            _sideBarHolder.name = "side-bar-holder";
            _sideBarHolder.AddToClassList("side-bar-holder");
            var sideBar = AssembleSideBar.SideBarAssembler(false, "Main Menu");
            sideBar.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            _sideBarHolder.Add(sideBar);
            _floatingElement.Add(_sideBarHolder);
        }
    }
}