using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public struct AssembleSideBar 
{
    private UIDocument _indexPage;
    private StyleSheet _indexPageStyle;
    private int _screenWidth;
    private float _numOfColumns;
    private VisualElement _root, _body, _sidebar, _mainContent;
    private static string[,] _sidebaritems;
    
    public static VisualElement SideBarAssembler(bool isMainPage, string headline)
    {
        var sidebar = new VisualElement();
        _sidebaritems = new string[13, 3] {
            {"Textures/Icons/home", "Home", "index"},
            {"Textures/Icons/main-menu", "Main Menu", "main-menu"},
            {"Textures/icons/hud", "HUD (Heads-Up Display)", "hud"},
            {"Textures/icons/inventory", "Inventory System", "inventory"},
            {"Textures/icons/dialogue", "Dialogue System", "dialogue"},
            {"Textures/icons/in-game-shop", "In-Game Shop", "in-game-shop"},
            {"Textures/icons/settings", "Settings Menu", "settings"},
            {"Textures/icons/loading-screen", "Loading Screen", "loading-screen"},
            {"Textures/icons/achievement", "Achievement System", "achievement"},
            {"Textures/icons/pause-menu", "Pause Menu", "pause-menu"},
            {"Textures/icons/tutorial", "Tutorial Overlay", "tutorial"},
            {"Textures/icons/unity-articles", "Unity Articles", "articles"},
            {"Textures/icons/contact", "Contact", "contact"}
        };
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
            icon.style.backgroundImage = Resources.Load<Texture2D>(_sidebaritems[i,0]);
            content.Add(icon);
            var label = new Label(_sidebaritems[i,1]);
            label.AddToClassList("side-bar-content-label");
            label.AddToClassList("wrap");
            // add clickable response to open scene
            var sceneName = _sidebaritems[i,2];
            content.RegisterCallback<MouseUpEvent>(evt => {
                SceneManager.LoadScene(sceneName);
            });
            content.name = _sidebaritems[i,1];
            content.Add(label);
            sidebar.Add(content);
        }
      
        var footer = new Label("Lem Apperson<br>LemApp Studios<br>Copyright\u00a9 2024");
        footer.AddToClassList("side-bar-footer");
        sidebar.Add(footer);

        return sidebar;
    }
}
