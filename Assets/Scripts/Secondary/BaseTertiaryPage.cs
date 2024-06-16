using System;
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
        
        // Drag and Drop parameters
        private static Vector2 targetStartPosition { get; set; }
        private static Vector3 pointerStartPosition { get; set; }
        private static bool enabled { get; set; }
        #endregion

        public static void BuildPage(string sceneName)
        {
            var contentId = Convert.ToInt32(sceneName);
            string categoryName;

            switch (contentId%100)
            {
                case 0:
                    categoryName = "Home";
                    break;
                case 1:
                    categoryName = "Main Menu";
                    break;
                case 2:
                    categoryName = "HUD";
                    break;
                case 3:
                    categoryName = "Inventory System";
                    break;
                case 4:
                    categoryName = "Dialogue System";
                    break;
                case 5:
                    categoryName = "In-Game Shop";
                    break;
                case 6:
                    categoryName = "Settings Menu";
                    break;
                case 7:
                    categoryName = "Loading Screen";
                    break;
                case 8:
                    categoryName = "Achievement System";
                    break;
                case 9:
                    categoryName = "Pause Menu";
                    break;
                case 10:
                    categoryName = "Tutorial Overlay";
                    break;
                case 11:
                    categoryName = "Unity Articles";
                    break;
                case 12:
                    categoryName = "Contact";
                    break;
                default:
                    categoryName = "Home";
                    break;
            }
            
            // begin the root document
            _menuSettings = AssetDatabase.LoadAssetAtPath<MenuSettings>("Assets/Scripts/Settings/MenuSettings.asset");
            if (_menuSettings == null) Debug.LogError("Menu Settings not found");
            _indexPage = _menuSettings.GetIndexPage();
            _indexPageStyle = _menuSettings.GetIndexPageStyle();
            if(_indexPage == null) Debug.LogError("Index Page not found");
            if(_indexPage == null) Debug.LogError("Index Page Style not found");
            
            // Unregister all events
            MouseEventManager.Instance.UnregisterAllEvents();

            // Clear all visual elements from the root
            _indexPage.rootVisualElement.Clear();
            _root = _indexPage.rootVisualElement;
            _root.AddToClassList("root");
            _root.styleSheets.Add(_indexPageStyle);

            // floating element, above the main content
            // when click, will show the side bar
            var _floatingElement = new VisualElement();
            _floatingElement.name = "Menu_" + categoryName;
            _floatingElement.AddToClassList("floating-element");
            _floatingElement.AddToClassList("floating-element");
            if (_menuSettings.GetDarkMode()) _floatingElement.AddToClassList("floating-element-darkMode");
            else _floatingElement.AddToClassList("floating-element-lightMode");

            #region MouseUp
            EventCallback<MouseUpEvent> mouseUpEventHandler = evt => HandlerMouseUp(evt);
            RegisterEventHandler(_floatingElement, mouseUpEventHandler);
            #endregion

            #region PointerDown
            EventCallback<PointerDownEvent> pointerDownEventHandler = evt => HandlerPointerDown(evt, _floatingElement);
            RegisterEventHandler(_floatingElement, pointerDownEventHandler);
            #endregion

            #region PointerMove
            EventCallback<PointerMoveEvent> pointerMoveEventHandler = evt => HandlerPointerMove(evt, _floatingElement);
            RegisterEventHandler(_floatingElement, pointerMoveEventHandler);
            #endregion

            #region PointerUp
            EventCallback<PointerUpEvent> pointerUpEventHandler = evt =>  HandlerPointerUp(evt, _floatingElement);
            RegisterEventHandler(_floatingElement, pointerUpEventHandler);
            #endregion

            #region PointerCapture
            EventCallback<PointerCaptureEvent> pointerCaptureEventHandler = evt => HandlerPointerCapture(evt, _floatingElement);
            RegisterEventHandler(_floatingElement, pointerCaptureEventHandler);
            #endregion
            
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
        
        private static void RegisterEventHandler<TEventType>(VisualElement element, EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new()
        {
            // Register the event handler with the card element
            element.RegisterCallback(callback);

            // Register the event handler with the EventManager
            MouseEventManager.Instance.RegisterEvent(element, callback);
        }

        
        private static void HandlerMouseUp(MouseUpEvent evt)
        {
            AudioManager.PlaySFXSound();
        }
        
        private static void HandlerPointerDown(PointerDownEvent evt, VisualElement floatingElement)
        {
            targetStartPosition = floatingElement.transform.position;
            pointerStartPosition = evt.position;
            floatingElement.CapturePointer(evt.pointerId);
            enabled = true;
        }
        
        private static void HandlerPointerMove(PointerMoveEvent evt, VisualElement floatingElement)
        {
            if (enabled && floatingElement.HasPointerCapture(evt.pointerId))
            {
                Vector3 pointerDelta = evt.position - pointerStartPosition;

                floatingElement.transform.position = new Vector2(
                    Mathf.Clamp(targetStartPosition.x + pointerDelta.x, 0, floatingElement.panel.visualTree.worldBound.width),
                    Mathf.Clamp(targetStartPosition.y + pointerDelta.y, 0, floatingElement.panel.visualTree.worldBound.height));
            }
        }
        
        private static void HandlerPointerUp(PointerUpEvent evt, VisualElement floatingElement)
        {
            if (enabled && floatingElement.HasPointerCapture(evt.pointerId))
            {
                floatingElement.ReleasePointer(evt.pointerId);
            }
        }
        
        private static void HandlerPointerCapture(PointerCaptureEvent evt, VisualElement floatingElement)
        {
            if (enabled)
            {
                /*    There are no slots
                VisualElement slotsContainer = floatingElement.Q<VisualElement>("slots");
                UQueryBuilder<VisualElement> allSlots =
                    slotsContainer.Query<VisualElement>(className: "slot");
                UQueryBuilder<VisualElement> overlappingSlots =
                    allSlots.Where(OverlapsTarget);
                VisualElement closestOverlappingSlot =
                    FindClosestSlot(overlappingSlots);
                Vector3 closestPos = Vector3.zero;
                if (closestOverlappingSlot != null)
                {
                    closestPos = RootSpaceOfSlot(closestOverlappingSlot);
                    closestPos = new Vector2(closestPos.x - 5, closestPos.y - 5);
                }
                floatingElement.transform.position =
                    closestOverlappingSlot != null ?
                        closestPos :
                        targetStartPosition;
               */

                enabled = false;
            }
        }
    }
}