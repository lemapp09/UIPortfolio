using UnityEngine;
using UnityEngine.UIElements;

public class IndexPageLayout : MonoBehaviour
{
   [SerializeField] private UIDocument _indexPage;
   [SerializeField] private StyleSheet _indexPageStyle;
   private int _numOfColumns;
   private VisualElement _root, _body, _sidebar, _mainContent;
   
    string[,] _sidebaritems = new string[12, 2]
    {
       {"Textures/Icons/main-menu", "Main Menu"},
       {"Textures/icons/hud", "HUD (Heads-Up Display)"},
       {"Textures/icons/inventory", "Inventory System"},
       {"Textures/icons/dialogue", "Dialogue System"},
       {"Textures/icons/in-game-shop", "In-Game Shop"},
       {"Textures/icons/settings", "Settings Menu"},
       {"Textures/icons/loading-screen", "Loading Screen"},
       {"Textures/icons/achievement", "Achievement System"},
       {"Textures/icons/pause-menu", "Pause Menu"},
       {"Textures/icons/tutorial", "Tutorial Overlay"},
       {"Textures/icons/unity-articles", "Unity Articles"},
       {"Textures/icons/contact", "Contact"}
    };

   private void Start()
   {
      var _numOfColumns = Screen.width / 325;
      if (_numOfColumns < 2) _numOfColumns = 2;

      _root = _indexPage.rootVisualElement;
      _root.styleSheets.Add(_indexPageStyle);
      _body = new VisualElement();
      _body.AddToClassList("body");
      _root.Add(_body);
      
      _sidebar = AssembleSideBar.SideBarAssembler( true, "Lem Apperson - Portfolio");
      _body.Add(_sidebar);
      _mainContent = new VisualElement();
      _mainContent.AddToClassList("main-content");
      _mainContent = AssembleMainContent.MainContentAssembler(_numOfColumns);
      _body.Add(_mainContent);

      _sidebar.style.width = Length.Percent((1.0f/_numOfColumns) * 100.0f);
      _mainContent.style.width = Length.Percent((1 - 1.0f/_numOfColumns) * 100.0f);
   }
}
