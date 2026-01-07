using UnityEngine;
using UnityEngine.InputSystem;

namespace Elias.Scripts
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject playerPrefab;
        public int playerCount = 4;

        private void Start()
        {
            for (int i = 0; i < playerCount; i++)
            {
                GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                PlayerInput playerInput = player.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
                }
            }
        }
    }
}
