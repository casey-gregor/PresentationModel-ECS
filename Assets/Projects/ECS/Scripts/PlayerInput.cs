using UnityEngine;

namespace ECSHomework
{
    public static class PlayerInput
    {
        public static Vector3 GetDirection()
        {
            Vector3 inputVector = Vector3.zero;
            
            if (Input.GetKey(KeyCode.W))
            {
                inputVector.x = 1;
            } 
            else if (Input.GetKey(KeyCode.S))
            {
                inputVector.x = -1;
            } 
            if (Input.GetKey(KeyCode.A))
            {
                inputVector.z = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                inputVector.z = -1;
                
            }
            
            return inputVector;
        }

        public static bool GetActionInput()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}