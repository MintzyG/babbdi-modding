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
using static ChangeSceneButton;

namespace Babbdi_Racing
{
    public class BabbdiRacing : MelonMod
    {

        public static BabbdiRacing instance;

        private static KeyCode StartGame;

        public override void OnEarlyInitializeMelon()
        {
            Melon<BabbdiRacing>.Logger.Msg("Mod Opened!");

            instance = this;

            StartGame = KeyCode.P;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Melon<BabbdiRacing>.Logger.Msg($"Scene {sceneName} was loaded");

        }

        public override void OnLateUpdate()
        {
            if (Input.GetKeyDown(StartGame) && SceneManager.GetActiveScene().name == "MainMenu")
            {
                SceneManager.LoadScene("Scene_AAA");
            }
        }

    }
}
