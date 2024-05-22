using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public struct AssembleMainContent 
{
    private UIDocument _indexPage;
    private StyleSheet _indexPageStyle;
    private int _screenWidth;
    private float _numOfColumns;
    private VisualElement _root, _body, _sidebar, _mainContent;
    private static string[,] _contentitems;
    
    public static VisualElement MainContentAssembler( int numberofColumns)
    {
        var maincontent = new VisualElement();
        _contentitems = new string[12, 4] {
            {"Textures/Images","Textures/Images/image1", "Main Menu", "main-menu"},
            {"Textures/icons/hud","Textures/Images/image2", "HUD (Heads-Up Display)", "hud"},
            {"Textures/icons/inventory","Textures/Images/image3", "Inventory System", "inventory"},
            {"Textures/icons/dialogue","Textures/Images/image4", "Dialogue System", "dialogue"},
            {"Textures/icons/in-game-shop","Textures/Images/image5", "In-Game Shop", "in-game-shop"},
            {"Textures/icons/settings","Textures/Images/image6", "Settings Menu", "settings"},
            {"Textures/icons/loading-screen","Textures/Images/image7", "Loading Screen", "loading-screen"},
            {"Textures/icons/achievement","Textures/Images/image8", "Achievement System", "achievement"},
            {"Textures/icons/pause-menu","Textures/Images/image9", "Pause Menu", "pause-menu"},
            {"Textures/icons/tutorial","Textures/Images/image10", "Tutorial Overlay", "tutorial"},
            {"Textures/icons/unity-articles","Textures/Images/image11", "Unity Articles", "articles"},
            {"Textures/icons/contact","Textures/Images/image12", "Contact", "contact"}
        };
        maincontent.AddToClassList("main-content");

      int i = 0;
      // There are 12 content items each with a different icon and label
      // The number of rows is 12 / number of Columns

      for (int k = 0; k < (12 / (numberofColumns - 1)); k++)
      {
          var row = new VisualElement();
          row.name = "row" + k;
          row.style.width = Length.Percent(100);
          row.style.flexDirection = FlexDirection.Row;
          for (int l = 0; l < (numberofColumns - 1); l++)
          {
              var card = new VisualElement();
              switch (i%3)
              {
                  case 0:
                      card.style.backgroundColor = new Color(0.95f, 0.79f, 0.7f, 1f);
                      break;
                  case 1:
                      card.style.backgroundColor = new Color(0.18f, 0.18f, 0.18f, 1f);
                      card.style.color = new Color(1.0f, 1.0f, 1.0f, 1f);
                      break;
                  case 2:
                      card.style.backgroundColor = new Color(0.76f, 0.79f, 0.8f, 1f); 
                      break;
              }
              if (l == 0) card.style.marginLeft = 20f;
              if (l == numberofColumns - 2) card.style.marginRight = 20f;
              card.AddToClassList("card");
              card.style.width = Length.Percent(100 / (numberofColumns - 1));
              var image = new VisualElement();
              image.AddToClassList("card-image");
              image.style.backgroundImage = Resources.Load<Texture2D>(_contentitems[i,1]);
              card.Add(image);
              var text = new Label(_contentitems[i,2]);
              text.AddToClassList("card-text");
              card.Add(text);
              card.name = "card" + _contentitems[i,3];
              // add clickable response to open scene
              var sceneName = _contentitems[i,3];
              card.RegisterCallback<MouseUpEvent>(evt => {
                  SceneManager.LoadScene(sceneName);
              });
              row.Add(card);
              i++;
              
          }
          maincontent.Add(row);
      }

        return maincontent;
    }
}
