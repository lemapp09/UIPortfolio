using UnityEngine;
using UnityEngine.UIElements;

namespace LemApperson_UIPortfolio
{

   public class InGameShopPageLayout : MonoBehaviour
   {
      [SerializeField] private UIDocument _indexPage;
      [SerializeField] private StyleSheet _indexPageStyle;
      private int _numOfColumns;
      private VisualElement _root, _body, _sidebar, _mainContent;

      private void Start()
      {
         var _numOfColumns = Screen.width / 325;
         if (_numOfColumns < 2) _numOfColumns = 2;

         _root = _indexPage.rootVisualElement;
         _root.styleSheets.Add(_indexPageStyle);
         _body = new VisualElement();
         _body.AddToClassList("body");
         _root.Add(_body);

         _sidebar = AssembleSideBar.SideBarAssembler(false, "In-Game Shop");
         _body.Add(_sidebar);
         _mainContent = new VisualElement();
         _mainContent.AddToClassList("main-content");
         _body.Add(_mainContent);
         _sidebar.style.width = Length.Percent((1.0f / _numOfColumns) * 100.0f);
         _mainContent.style.width = Length.Percent((1 - 1.0f / _numOfColumns) * 100.0f);
      }
   }
}