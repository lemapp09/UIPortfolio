using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace LemApperson_UIPortfolio
{
    [CreateAssetMenu(fileName = "MenuSettings", menuName = "ScriptableObjects/MenuSettings", order = 1)]
    public class MenuSettings : ScriptableObject
    {
        public event Action<bool> OnDarkModeChanged;

        [SerializeField] private bool darkMode;
        private UIDocument _indexPage;
        private StyleSheet _indexPageStyle;

        private static string[,] SidebarItems = new string[13, 3]
        {
            { "Textures/Icons/home", "Home", "index" },
            { "Textures/Icons/main-menu", "Main Menu", "main-menu" },
            { "Textures/icons/hud", "HUD (Heads-Up Display)", "hud" },
            { "Textures/icons/inventory", "Inventory System", "inventory" },
            { "Textures/icons/dialogue", "Dialogue System", "dialogue" },
            { "Textures/icons/in-game-shop", "In-Game Shop", "in-game-shop" },
            { "Textures/icons/settings", "Settings Menu", "settings" },
            { "Textures/icons/loading-screen", "Loading Screen", "loading-screen" },
            { "Textures/icons/achievement", "Achievement System", "achievement" },
            { "Textures/icons/pause-menu", "Pause Menu", "pause-menu" },
            { "Textures/icons/tutorial", "Tutorial Overlay", "tutorial" },
            { "Textures/icons/unity-articles", "Unity Articles", "articles" },
            { "Textures/icons/contact", "Contact", "contact" }
        };

        private string[,] MainContentItems = new string[12, 6]
        {
            {
                "Textures/Images", "Textures/Images/image1", "Main Menu", "main-menu", "Textures/Icons/main-menu",
                "The <b>Main Menu</b> is the central hub of any video game, offering players options to start, load, or customize their game experience."
            },
            {
                "Textures/icons/hud", "Textures/Images/image2", "HUD (Heads-Up Display)", "hud", "Textures/icons/hud",
                "The <b>HUD</b> (Heads Up Display) provides players with real-time information about their character's health, ammo, and other vital stats."
            },
            {
                "Textures/icons/inventory", "Textures/Images/image3", "Inventory System", "inventory",
                "Textures/icons/inventory",
                "The <b>Inventory System</b> allows players to manage and organize the items they collect during their adventure."
            },
            {
                "Textures/icons/dialogue", "Textures/Images/image4", "Dialogue System", "dialogue",
                "Textures/icons/dialogue",
                "The <b>Dialogue System</b> facilitates interactions between characters, helping to drive the game's story forward."
            },
            {
                "Textures/icons/in-game-shop", "Textures/Images/image5", "In-Game Shop", "in-game-shop",
                "Textures/icons/in-game-shop",
                "The <b>In-game Shop</b> offers players a place to purchase items, upgrades, or abilities using in-game currency."
            },
            {
                "Textures/icons/settings", "Textures/Images/image6", "Settings Menu", "settings",
                "Textures/icons/settings",
                "The <b>Settings Menu</b> allows players to customize their gaming experience by adjusting controls, graphics, and audio preferences."
            },
            {
                "Textures/icons/loading-screen", "Textures/Images/image7", "Loading Screen", "loading-screen",
                "Textures/icons/loading-screen",
                "The <b>Loading Screen</b> provides players with helpful tips or game lore while new game assets are being loaded."
            },
            {
                "Textures/icons/achievement", "Textures/Images/image8", "Achievement System", "achievement",
                "Textures/icons/achievement",
                "The <b>Achievement System</b> rewards players for completing specific challenges or milestones within the game."
            },
            {
                "Textures/icons/pause-menu", "Textures/Images/image9", "Pause Menu", "pause-menu",
                "Textures/icons/pause-menu",
                "The <b>Pause Menu</b> allows players to temporarily stop the game, access settings, or save their progress."
            },
            {
                "Textures/icons/tutorial", "Textures/Images/image10", "Tutorial Overlay", "tutorial",
                "Textures/icons/tutorial",
                "The <b>Tutorial Overlay</b> guides new players through the game's mechanics and controls, ensuring a smooth learning curve."
            },
            {
                "Textures/icons/unity-articles", "Textures/Images/image11", "Unity Articles", "articles",
                "Textures/icons/unity-articles",
                ""
            },
            {
                "Textures/icons/contact", "Textures/Images/image12", "Contact", "contact", "Textures/icons/contact",
                ""
            }
        };

        private string[,] AchievementContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Achievement System</b> rewards players for completing specific challenges or milestones within the game.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small achievement menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small achievement menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small achievement menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small achievement menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small achievement menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small achievement menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small achievement menu.", ""
            }
        };

        private string[,] ArticlesContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "Welcome to my <b>Articles</b> page! Dive into a wealth of knowledge about the Unity3D Game Engine, C# Programming Language, and C++ Programming Language. Recently, I've begun exploring UI Design. Whether you're a developer, designer, or enthusiast, there's something here for everyone. Stay tuned for daily insights and tips!",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Unity & C#", "", "Textures/icons/hud",
                "<b>Unity C#</b>", "https://medium.com/@lemapp09/list/beginning-game-development-bac8f38b9f0e"
            },
            {
                "-", "Textures/Icons/main-menu", "C++", "",
                "Textures/icons/inventory",
                "<b>C++</b>", "https://medium.com/@lemapp09/list/c-programming-4a5022178368"
            },
            {
                "-", "Textures/Icons/dialogue", "UI Design", "",
                "Textures/icons/dialogue",
                "<b>UI Design</b>", "https://medium.com/@lemapp09/list/mastering-ui-design-2ab01ee9012e"
            },
            {
                "-", "Textures/Icons/in-game-shop", "Unreal", "",
                "Textures/icons/in-game-shop",
                "<b>Unreal</b>", "https://medium.com/@lemapp09/list/learning-unreal-57ed1d2286bc"
            },
            {
                "", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small Articles menu.", ""
            },
            {
                "", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small Articles menu.", ""
            },
            {
                "", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small Articles menu.", ""
            }
        };

        private string[,] ContactContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "Welcome to my personal <b>Contacts</b> page! Connect with me through various platforms like email, LinkedIn, GitHub, and more. Whether you have a project in mind, need collaboration, or just want to say hello, I'm always excited to network and share ideas. Let's create something amazing together!",
                ""
            },
            {
                "-", "Textures/Icons/hud",
                "<b>Email</b>\n<a href='mailto:lemapp.yahoo.com>lemapp@yahoo.com'>lemapp.yahoo.com>lemapp@yahoo.com</a>",
                "", "Textures/icons/hud",
                "<b>Email</b>", "mailto:lemapp.yahoo.com"
            },
            {
                "-", "Textures/Icons/main-menu",
                "<b>LinkedIn Profile</b>\nhttps://www.linkedin.com/\nin/lem-apperson-b9733b50/", "",
                "Textures/icons/inventory",
                "<b>LinkedIn</b>", "https://www.linkedin.com/in/lem-apperson-b9733b50/"
            },
            {
                "-", "Textures/Icons/dialogue", "<b>GitHub</b>\nhttps://github.com/\nlemapp09", "",
                "Textures/icons/dialogue",
                "<b>GitHub</b>", "https://github.com/lemapp09"
            },
            {
                "-", "Textures/Icons/in-game-shop", "Itch.io\nhttps://lemapp09.itch.io/", "",
                "Textures/icons/in-game-shop",
                "<b>Itch.io</b>", "https://lemapp09.itch.io/"
            },
            {
                "-", "Textures/Icons/settings", "<b>Online Portfolio</b>\nhttp://lemapperson.42web.io/", "",
                "Textures/icons/settings",
                "<b>Online Portfolio</b>", "http://lemapperson.42web.io/"
            },
            {
                "", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b>", ""
            },
            {
                "", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b>", ""
            }
        };

        private string[,] DialogueContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Dialogue System</b> facilitates interactions between characters, helping to drive the game's story forward.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small Dialogue menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small Dialogue menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small Dialogue menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small Dialogue menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small Dialogue menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small Dialogue menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small Dialogue menu.", ""
            }
        };

        private string[,] HUDContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>HUD</b> (Heads Up Display) provides players with real-time information about their character's health, ammo, and other vital stats.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small HUD (Heads Up Display) menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small HUD (Heads Up Display) menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small HUD (Heads Up Display) menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small HUD (Heads Up Display) menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small HUD (Heads Up Display) menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small HUD (Heads Up Display) menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small HUD (Heads Up Display) menu.", ""
            }
        };

        private string[,] InGameShopContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>In-game Shop</b> offers players a place to purchase items, upgrades, or abilities using in-game currency.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small In Game Shop menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small In Game Shop menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small In Game Shop menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small In Game Shop menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small In Game Shop menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small In Game Shop menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small In Game Shop menu.", ""
            }
        };

        private string[,] InventoryContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Inventory System</b> allows players to manage and organize the items they collect during their adventure.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small Inventory menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small Inventory menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small Inventory menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small Inventory menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small Inventory menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small Inventory menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small Inventory menu.", ""
            }
        };

        private string[,] LoadingScreenContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Loading Screen</b> provides players with helpful tips or game lore while new game assets are being loaded.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small loading screen.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small loading screen.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small loading screen.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small loading screen.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small loading screen.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small loading screen.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small loading screen.", ""
            }
        };

        private string[,] MainMenuContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Main Menu</b> is the central hub of any video game, offering players options to start, load, or customize their game experience.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "main-menu01", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small main menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "main-menu02",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small main menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "main-menu03",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small main menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "main-menu04",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small main menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "main-menu05",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small main menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "main-menu06",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small main menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "main-menu07",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small main menu.", ""
            }
        };

        private string[,] PauseMenuContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Pause Menu</b> allows players to temporarily stop the game, access settings, or save their progress.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small pause menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small pause menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small pause menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small pause menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small pause menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small pause menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small pause menu.", ""
            }
        };

        private string[,] SettingsContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Settings Menu</b> allows players to customize their gaming experience by adjusting controls, graphics, and audio preferences.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small Settings menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small Settings menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small Settings menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small Settings menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small Settings menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small Settings menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small Settings menu.", ""
            }
        };

        private string[,] TutorialContentItems = new string[8, 7]
        {
            // { Visible, Icon, Name, Scene, Hidden Icon, Hidden Description, Web Link }
            {
                "", "", "", "", "",
                "The <b>Tutorial Overlay</b> guides new players through the game's mechanics and controls, ensuring a smooth learning curve.",
                ""
            },
            {
                "-", "Textures/Icons/hud", "Example 1", "", "Textures/icons/hud",
                "<b>Example 1</b> is a sample of a small tutorial menu.", ""
            },
            {
                "-", "Textures/Icons/main-menu", "Example 2", "",
                "Textures/icons/inventory",
                "<b>Example 2</b> is a sample of a small tutorial menu.", ""
            },
            {
                "-", "Textures/Icons/dialogue", "Example 3", "",
                "Textures/icons/dialogue",
                "<b>Example 3</b> is a sample of a small tutorial menu.", ""
            },
            {
                "-", "Textures/Icons/in-game-shop", "Example 4", "",
                "Textures/icons/in-game-shop",
                "<b>Example 4</b> is a sample of a small tutorial menu.", ""
            },
            {
                "-", "Textures/Icons/settings", "Example 5", "",
                "Textures/icons/settings",
                "<b>Example 5</b> is a sample of a small tutorial menu.", ""
            },
            {
                "-", "Textures/Icons/loading-screen", "Example 6", "",
                "Textures/icons/loading-screen",
                "<b>Example 6</b> is a sample of a small tutorial menu.", ""
            },
            {
                "-", "Textures/Icons/achievement", "Example 7", "",
                "Textures/icons/achievement",
                "<b>Example 7</b> is a sample of a small tutorial menu.", ""
            }
        };

        public void ToggleDarkMode()
        {
            darkMode = !darkMode;
            OnDarkModeChanged?.Invoke(darkMode);
        }

        public bool GetDarkMode()
        {
            return darkMode;
        }

        public string[,] GetSidebarItems()
        {
            return SidebarItems;
        }

        public string[,] GetContentItems()
        {
            return MainContentItems;
        }

        public string[,] GetSecondaryContentItems(int index)
        {
            switch (index)
            {
                case 0:
                    return null;
                case 1:
                    return MainMenuContentItems;
                case 2:
                    return HUDContentItems;
                case 3:
                    return InventoryContentItems;
                case 4:
                    return DialogueContentItems;
                case 5:
                    return InGameShopContentItems;
                case 6:
                    return SettingsContentItems;
                case 7:
                    return LoadingScreenContentItems;
                case 8:
                    return AchievementContentItems;
                case 9:
                    return PauseMenuContentItems;
                case 10:
                    return TutorialContentItems;
                case 11:
                    return ContactContentItems;
                case 12:
                    return ContactContentItems;
                default:
                    return null;
            }
        }

        public void SetIndexPage(UIDocument indexPage)
        {
            _indexPage = indexPage;
        }

        public UIDocument GetIndexPage()
        {
            return _indexPage;
        }

        public void SetIndexPageStyle(StyleSheet indexStylePage)
        {
            _indexPageStyle = indexStylePage;
        }

        public StyleSheet GetIndexPageStyle()
        {
            return _indexPageStyle;
        }
    }
}