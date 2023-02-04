using MelonLoader;
using UnityEngine;

namespace debabbdi
{
    public class debabbdi : MelonMod
    {
        public static debabbdi instance;
        
        private static KeyCode freezeToggleKey;
        
        private static bool frozen;
        private static float baseTimeScale;
        public override void OnEarlyInitializeMelon()
        {
            instance = this;
            freezeToggleKey = KeyCode.Q;
        }

        public override void OnLateUpdate()
        {
            if (Input.GetKeyDown(freezeToggleKey))
            {
                ToggleFreeze();
            }
        }
        
        /*
        public static void DrawFrozenText()
        {
            .Label(new Rect(20, 20, 1000, 200), "<b><color=cyan><size=100>Frozen</size></color></b>");
        }
        */

        private static void ToggleFreeze()
        {
            frozen = !frozen;

            if (frozen)
            {
                instance.LoggerInstance.Msg("Freezing");
                
                // MelonEvents.OnGUI.Subscribe(DrawFrozenText, 100); // Register the 'Frozen' label
                baseTimeScale = Time.timeScale; // Save the original time scale before freezing
                Time.timeScale = 0;
            }
            else
            {
                instance.LoggerInstance.Msg("Unfreezing");
                
                // MelonEvents.OnGUI.Unsubscribe(DrawFrozenText); // Unregister the 'Frozen' label
                Time.timeScale = baseTimeScale; // Reset the time scale to what it was before we froze the time
            }
        }

        public override void OnDeinitializeMelon()
        {
            if (frozen)
            {
                ToggleFreeze(); // Unfreeze the game in case the melon gets unregistered
            }
        }
    }
}