using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Babbdi_Racing
{
    public class BabbdiRacing : MelonMod
    {
        public static BabbdiRacing instance;
        SecretFinder Secrets = new SecretFinder();

        private static MenuInterface MenuI = new MenuInterface();

        public bool RacingMode;
        public bool MappingMode;

        private static KeyCode _toggleMenu;

        public override void OnEarlyInitializeMelon()
        {
            Melon<BabbdiRacing>.Logger.Msg("Mod Opened!");
            instance = this;

            _toggleMenu = KeyCode.P;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                RacingMode = false; MappingMode = false;
            } 
        }

        public override void OnLateUpdate()
        {
            if (Input.GetKeyDown(_toggleMenu) && SceneManager.GetActiveScene().name == "MainMenu")
            {
                MenuI.ToggleMenu("MainMenu", instance);
            }
            else if (Input.GetKeyDown(_toggleMenu))
            {
                MenuI.ToggleMenu("InGame", instance);
                MelonEvents.OnGUI.Subscribe(Menu);
            }

        }

        public void Menu()
        {
            GUI.Box(new Rect(1700, 40, 120, 80), Secrets.target.ToString());
        }

        public void OpenGame() 
        {
            MenuI.ToggleMenu("MainMenu", instance);
            SceneManager.LoadScene("Scene_AAA");
        }


    }

}

