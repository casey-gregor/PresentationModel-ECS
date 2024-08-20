using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace ECSHomework
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Entity player;

        private void Update()
        {
            ProcessMoveInput();
            ProcessFireInput();
        }

        private void ProcessMoveInput()
        {
            Vector3 inputDirection = PlayerInput.GetDirection();
            ref MoveDirection moveDirection = ref player.GetData<MoveDirection>();
            
            moveDirection.Value = inputDirection;
        }

        private void ProcessFireInput()
        {
            bool fireInput = PlayerInput.GetActionInput();
            if (fireInput)
                player.SetData(new FireRequest());
        }
    }
}