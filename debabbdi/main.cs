using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace debabbdi
{
    public class Debabbdi : MelonMod
    {
        private static Debabbdi _instance;
        
        private static KeyCode _dumpDebugInfo;
        private static KeyCode _debugMenuKeyCode;

        private TpClass _tp = new TpClass();
        private GroundedCheck _groundedCheck = new GroundedCheck();

        private GameObject _player;

        

        private Vector3 _myVector;
        
        private Vector3 _velocity;
        private double lastVec;
        private double _acceleration;
        private string _sCoordX;
        private string _sCoordY;
        private string _sCoordZ;
        private string _sVel;
        private string _sAccel;
        private int _bounces;
        private double _lastVelocity;
        private double _velDiff;

        private static bool _cheatDetected;
        private static bool _menu;
        private static bool _debugMenu;
        private static bool _teleports;
        private static bool _isGrounded;
        
        private static float _sliderHome = 1.0f;
        private static float _gameSpeed = 1.0f;
        
        public override void OnEarlyInitializeMelon()
        {
            _instance = this;
            _dumpDebugInfo = KeyCode.L;
            _debugMenuKeyCode = KeyCode.I;
        }
        public override void OnUpdate()
        {
            if (SceneManager.GetActiveScene().name == "Scene_AAA")
            {
                _isGrounded = _groundedCheck.CheckGrounded(_player);
            }
        }
        
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (SceneManager.GetActiveScene().name == "Scene_AAA")
            {
                _player = GameObject.Find("Player");
            }
        }

        public override void OnFixedUpdate()
        {

            // Gets player pos info off of the camera
            foreach (var camera in Camera.allCameras)
            {
                if (camera.name == "Main Camera")
                {
                    _myVector = camera.transform.position;
                    _velocity = camera.velocity;

                    

                    _acceleration = (Math.Sqrt(Math.Pow(_velocity.x, 2) + Math.Pow(_velocity.y, 2) + Math.Pow(_velocity.z, 2)) - lastVec) / Time.deltaTime;

                    lastVec = Math.Sqrt(Math.Pow(_velocity.x, 2) + Math.Pow(_velocity.y, 2) + Math.Pow(_velocity.z, 2));

                    _sVel = _velocity.ToString("0.00");
                    _sCoordX = _myVector.x.ToString("0.00");
                    _sCoordY = _myVector.y.ToString("0.00");
                    _sCoordZ = _myVector.z.ToString("0.00");
                    _sAccel = _acceleration.ToString("0.00");

                    _velDiff = _lastVelocity - _velocity.y;

                    if ((Math.Abs(_velDiff) > 10) && (_acceleration < 0) && (_isGrounded == false)) 
                    {

                        _bounces += 1;
                    
                    }

                    _lastVelocity = _velocity.y;

                    

                }
            }
        }
        // Happens after everything else happened in a frame
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

            // Gets all childs from Object
            /*
            GameObject Object = GameObject.Find("Object");
            for (var i = 0; i < Object.transform.childCount; i++)
            {
                _instance.LoggerInstance.Msg(player.transform.GetChild(i));
            }
            */
            
            /* Gets all objects in the loaded scene
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            Instance.LoggerInstance.Msg("Object List: \n");
            foreach (var i in rootGameObjects)
            {
                Instance.LoggerInstance.Msg("Found Object: " + i.name);
            }
            */
        }
        // Draws cheat menu
        private void DrawDebugMenu()
        {
            
            GUI.Box(new Rect(0, 210, 350, 800), "Debug Menu\n \n Speed Mod:");
            
            _sliderHome = GUI.HorizontalSlider(new Rect(70, 320, 200, 20), _sliderHome, 0f, 2.0f);
            
            if (GUI.Button(new Rect(40, 280, 70, 30), "Confirm"))
            {
                _gameSpeed = _sliderHome;
                ToggleFreeze();
                _cheatDetected = true;
                DetectCheat();

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

            if (GUI.Button(new Rect(40, 400, 100, 30), "Teleports"))
            {
                ToggleTeleportsMenu();
            }
        }
        private void ToggleTeleportsMenu()
        {
            _teleports = !_teleports;

            if (_teleports)
            {
                MelonEvents.OnGUI.Subscribe(TeleportsMenu, 100);
            }
            else
            {
                MelonEvents.OnGUI.Unsubscribe(TeleportsMenu);
            } 
        }

        // Teleports buttons menu
        private void TeleportsMenu()
        {
            
            string[] names = { "NoToolsJump", "HighPoint", "NoToolsZoop", "Warp", "Ticket" };

            GUI.Box(new Rect(1700, 40, 200, 800), "Teleports \n \n \n \n \n \n \n \n \n \n X: \t Y: \t Z:");

            GUI.TextField(new Rect(1720, 380, 40, 30), "");
            GUI.TextField(new Rect(1780, 380, 40, 30), "");
            GUI.TextField(new Rect(1840, 380, 40, 30), "");
            
            for (int i = 0; i < 5; i++)
            {
                if (GUI.Button(new Rect(1740, 20 + (50 * (i + 1)), 120, 30), names[i]))
                {
                    if (SceneManager.GetActiveScene().name == "Scene_AAA")
                    {
                        _cheatDetected = true;
                        DetectCheat();

                        switch (names[i])
                        {
                            case "NoToolsJump":
                                _player.transform.position = _tp.TeleportFixed("NoToolsJump");
                                _instance.LoggerInstance.Msg("NTJump-TP");
                                break;
                            case "HighPoint":
                                _player.transform.position = _tp.TeleportFixed("HighPoint");
                                _instance.LoggerInstance.Msg("HighPoint-TP");
                                break;
                            case "NoToolsZoop":
                                _player.transform.position = _tp.TeleportFixed("NoToolsZoop");
                                _instance.LoggerInstance.Msg("NTZoop-TP");
                                break;
                            case "Warp":
                                _player.transform.position = _tp.TeleportFixed("Warp");
                                _instance.LoggerInstance.Msg("Warp-TP");
                                break;
                            case "Ticket":
                                _player.transform.position = _tp.TeleportFixed("Ticket");
                                _instance.LoggerInstance.Msg("Ticket-TP");
                                break;
                        }
                    }
                    else
                    {
                        _instance.LoggerInstance.Msg("Cannot TP from menu");  
                    }
                }
            }
        }
        // Toggles cheat menu
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
        // Activates and deactivates debug info menu
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
        // This is the debug info menu
        private void DrawMenu()
        {
            GUI.Box(new Rect(0, 0, 300, 170), "Info \n \n Position X: " + _sCoordX +
                                              "\t \n Position Y: " + _sCoordY + " \t \n Position Z: " + _sCoordZ +
                                              "\t \n Speed: " + _sVel + "\t \n Acceleration: " + _sAccel + 
                                              "\t \n Game Speed: " + _gameSpeed.ToString("0.00") +
                                              "\t \n Is Grounded: " + _isGrounded + "\t \n Bounces: " + _bounces);
        }
        // Slows down the game
        private static void ToggleFreeze()
        {
            _instance.LoggerInstance.Msg("Game speed set to: " + _sliderHome);
            Time.timeScale = _sliderHome;
        }
        // Detect cheat
        private void DetectCheat()
        {
            if (_cheatDetected)
            {
                MelonEvents.OnGUI.Unsubscribe(cheatOn);
                MelonEvents.OnGUI.Subscribe(cheatOn);
            }
        }
        
        // Shows you are cheating/cheated
        private static void cheatOn()
        {
            GUI.Box(new Rect(1817, 35, 100, 25),"<b><color=red>Cheated</color></b>", "richText");
        }
        // Unfreezes game in case mod gets unloaded
        public override void OnDeinitializeMelon()
        {
            _sliderHome = 1.0f;
            ToggleFreeze();
        }
    }
}
