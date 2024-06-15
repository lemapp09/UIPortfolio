using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LemApperson_UIPortfolio
{
    public class MainMenuPageLayout06 : MonoBehaviour
    {
        private VisualElement _root;
        [SerializeField] private MenuSettings _menuSettings;
        private UIDocument _indexPage;
        [SerializeField] private StyleSheet _indexPageStyle;

        private void Start()
        {
            // begin the root document
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            _indexPage = _menuSettings.GetIndexPage();
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