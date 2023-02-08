using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace debabbdi
{
    public class Debabbdi : MelonMod
    {
        private static Debabbdi _instance;
        
        private static KeyCode _dumpDebugInfo;
        private static KeyCode _debugMenuKeyCode;

        private float _coordinateX;
        private float _coordinateY;
        private float _coordinateZ;
        private Vector3 _velocity;
        private double _acceleration;
        private string _sCoordX;
        private string _sCoordY;
        private string _sCoordZ;
        private string _sVel;
        private string _sAccel;

        private static bool _cheatDetected;
        private static bool _menu;
        private static bool _debugMenu;

        private static float _sliderHome = 1.0f;
        private static float _gameSpeed = 1.0f;
        
        public override void OnEarlyInitializeMelon()
        {
            _instance = this;
            _dumpDebugInfo = KeyCode.L;
            _debugMenuKeyCode = KeyCode.I;
        }

        public override void OnLateUpdate()
        {
            if (Input.GetKeyDown(_dumpDebugInfo))
            {
                DumpDebug();
            }
            else if (Input.GetKeyDown(_debugMenuKeyCode))
            {
                DebugMenu();
            }
            
            // Gets player pos info off of the camera
            foreach (var camera in Camera.allCameras)
            {
                if (camera.name == "Main Camera")
                {
                    _velocity = camera.velocity;
                    _coordinateX = camera.transform.position.x;
                    _coordinateY = camera.transform.position.y;
                    _coordinateZ = camera.transform.position.z;
                    _acceleration = Math.Sqrt(Math.Pow(_velocity.x, 2) + Math.Pow(_velocity.y, 2) +
                                             Math.Pow(_velocity.z, 2));

                    _sVel = _velocity.ToString("0.00");
                    _sCoordX = _coordinateX.ToString("0.00");
                    _sCoordY = _coordinateY.ToString("0.00");
                    _sCoordZ = _coordinateZ.ToString("0.00");
                    _sAccel = _acceleration.ToString("0.00");


                }
            }
        }
        private static void DumpDebug()
        {
            // Gets current scene name
            var sceneName = SceneManager.GetActiveScene().name;
            _instance.LoggerInstance.Msg("Loaded scene -> " + sceneName + "\n");

            // Gets all cameras in loaded scene
            foreach (var camera in Camera.allCameras)
            {
                _instance.LoggerInstance.Msg("Found Camera: " + camera.name);
            }

            /* Gets all objects in the loaded scene
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            Instance.LoggerInstance.Msg("Object List: \n");
            foreach (var i in rootGameObjects)
            {
                Instance.LoggerInstance.Msg("Found Object: " + i.name);
            }
            */
        }

        private void DrawDebugMenu()
        {
            
            GUI.Box(new Rect(0, 210, 350, 800), "Debug Menu\n \n Speed Mod:");
            
            _sliderHome = GUI.HorizontalSlider(new Rect(70, 320, 200, 20), _sliderHome, 0.1f, 2.0f);
            
            if (GUI.Button(new Rect(40, 280, 70, 30), "Confirm"))
            {
                _gameSpeed = _sliderHome;
                ToggleFreeze();
                _cheatDetected = true;

                if (_cheatDetected)
                {
                    MelonEvents.OnGUI.Unsubscribe(cheatOn);
                    MelonEvents.OnGUI.Subscribe(cheatOn);
                }

            } 
            else if (GUI.Button(new Rect(130, 280, 70, 30), "Default"))
            {
                _sliderHome = 1.0f;
                _gameSpeed = 1.0f;
                ToggleFreeze();
            }
            
            GUI.Box(new Rect(220, 280, 70, 30), _sliderHome.ToString("0.00"));

            if (GUI.Button(new Rect(40, 340, 100, 30), "Toggle Info"))
            {
                ToggleMenu();
            }
            
        }
        private void ToggleMenu()
        {
            _menu = !_menu;

            if (_menu)
            {
                MelonEvents.OnGUI.Subscribe(DrawMenu, 100);
            }
            else
            {
                MelonEvents.OnGUI.Unsubscribe(DrawMenu);
            }
        }
        private void DebugMenu()
        {
            _debugMenu = !_debugMenu;

            if (_debugMenu)
            {
                MelonEvents.OnGUI.Subscribe(DrawDebugMenu, 100);
            }
            else
            {
                MelonEvents.OnGUI.Unsubscribe(DrawDebugMenu);
            }
        }
        private void DrawMenu()
        {
            GUI.Box(new Rect(0, 0, 300, 150), "Info \n \n Position X: " + _sCoordX +
                                              "\t \n Position Y: " + _sCoordY + " \t \n Position Z: " + _sCoordZ +
                                              "\t \n Speed: " + _sVel + "\t \n Acceleration: " + _sAccel + 
                                              "\t \n Game Speed: " + _gameSpeed.ToString("0.00") +
                                              "\t \n");
        }
        private static void ToggleFreeze()
        {
            _instance.LoggerInstance.Msg("Game speed set to: " + _sliderHome);
            Time.timeScale = _sliderHome;
        }
        private static void cheatOn()
        {
            GUI.Box(new Rect(1817, 35, 100, 25),"<b><color=red>Cheated</color></b>", "richText");
        }
        public override void OnDeinitializeMelon()
        {
            _sliderHome = 1.0f;
            ToggleFreeze(); // Unfreeze the game in case the melon gets unregistered
        }
    }
}
