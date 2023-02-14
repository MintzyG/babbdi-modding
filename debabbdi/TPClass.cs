using UnityEngine;

namespace debabbdi
{
    public class TpClass
    {

        private Vector3 _target;
        
        public Vector3 TeleportFixed(string place)
        {

            _target = new Vector3(0.0f, 0.0f, 0.0f);
            
            switch (place) {
                case "NoToolsJump":
                    _target.x = -222.33f;
                    _target.y = -15.33f;
                    _target.z = 101.07f;
                    return _target;
                case "Ticket":
                    _target.x = 165.18f;
                    _target.y = 44.33f;
                    _target.z = -7.79f;
                    return _target;
                case "Warp":
                    _target.x = -333.53f;
                    _target.y = 50.92f;
                    _target.z = 190.86f;
                    return _target;
                case "BridgeJump":
                    _target.x = -272.21f;
                    _target.y = 5.04f;
                    _target.z = 63.93f;
                    return _target;
                case "NoToolsZoop":
                    _target.x = 30.81f;
                    _target.y = 16.90f;
                    _target.z = 32.28f;
                    return _target;
            }

            GameObject player = GameObject.Find("Player");
            Vector3 failCase = player.transform.position;
            return failCase;
        }
        
        
    }
}