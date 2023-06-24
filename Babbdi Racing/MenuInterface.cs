using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MelonLoader.MelonLogger;

namespace Babbdi_Racing
{
    public class MenuInterface
    { 

        private static bool _startMenu;

    
        private void StartMenu()
        {
            SecretFinder instance = new SecretFinder();

            GUI.Box(new Rect(1700, 40, 120, 80), instance.target.ToString());


            /*
            if (GUI.Button(new Rect(1700, 40, 120, 80), "Mapping"))
            {
                
                instance.OpenGame();
            }

            if (GUI.Button(new Rect(1700, 120, 120, 80), "Racing"))
            {
               
                instance.OpenGame();
            }
            */
        }

        public void ToggleMenu(string menu, BabbdiRacing instance)
        {

            switch (menu)
            {
                case "MainMenu":
                    _startMenu = !_startMenu;
                    if (_startMenu)
                    {
                        MelonEvents.OnGUI.Unsubscribe(StartMenu);
                    }
                    else
                    {
                        MelonEvents.OnGUI.Subscribe(StartMenu);
                    }
                    break;
                case "InGame":
                    if (instance.MappingMode)
                    {
                        Melon<BabbdiRacing>.Logger.Msg("Mapping!");
                    }                     
                    else if (instance.RacingMode)
                    {
                        Melon<BabbdiRacing>.Logger.Msg("Racing!");
                    }
                    else
                    {
                        Melon<BabbdiRacing>.Logger.Msg("What!");
                    }
                    break;
                default:
                    Melon<BabbdiRacing>.Logger.Msg("Death!");
                    break;
            }
        }
        
    }
}
