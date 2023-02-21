using UnityEngine;

namespace Speedrun
{
    public class GroundedCheck
    {
        private bool _isGrounded;

        public bool CheckGrounded(GameObject player)
        {
            
            _isGrounded = Physics.Raycast(player.transform.position, Vector3.down, 0.6f);
    
            if (!_isGrounded) 
                return false;

            return true;
        }
    }
}