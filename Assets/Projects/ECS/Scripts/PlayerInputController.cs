using UnityEngine;

namespace ECSHomework
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Entity player;

        private void Update()
        {
            ProcessMoveInput();
        }

        private void ProcessMoveInput()
        {
            Vector3 inputDirection = PlayerInput.GetDirection();
            ref MoveDirection moveDirection = ref player.GetData<MoveDirection>();
            
            moveDirection.Value = inputDirection;
        }
    }
}