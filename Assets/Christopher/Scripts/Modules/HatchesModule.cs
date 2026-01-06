using Elias.Scripts.Managers;
using Elias.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Christopher.Scripts.Modules
{
    public class HatchesModule : SubmarinModule
    {
        [SerializeField] private GameObject greenLights;
        [SerializeField] private GameObject redLights;
        [SerializeField] private AudioSource audioSource;
        
        public void SetHighlight(bool highlight)
        {
            // Assuming you have a Material or Renderer for the highlight effect
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.SetFloat("_Highlight", highlight ? 1f : 0f);
                // Or use a different property/method depending on your shader
            }
        }

        private void OnEnable()
        {
            GameManager.OnWaterShouldDrain += OnWaterShouldDrain;
        }

        private void OnDisable()
        {
            GameManager.OnWaterShouldDrain -= OnWaterShouldDrain;
        }

        private void OnWaterShouldDrain()
        {
            SetHighlight(true);
        }
        
        void Start()
        {
            PlayerUsingModule = null;
            
        }
        void Update() {
            if (IsActivated) {
                State = 1;
                greenLights.SetActive(true);
                redLights.SetActive(false);
                playerDetector.SetActive(true);
            }
            else {
                State = 0;
                greenLights.SetActive(false);
                redLights.SetActive(true);
                playerDetector.SetActive(false);
            }
            
            if (GameManager.Instance.activeModuleCount > 0 || GameManager.Instance.water.transform.position.y <= GameManager.Instance._originalWaterPosition.y)
            {
                SetHighlight(false); //disable mat
            }
            
            Material[]mats = StateDisplayObject[0].transform.GetComponent<MeshRenderer>().materials;
            mats[3] = StatesMaterials[State];
            StateDisplayObject[0].transform.GetComponent<MeshRenderer>().materials = mats;
        }

        public override void Activate()
        {
            IsActivated = true;
            audioSource.Play();
        }

        public override void Deactivate()
        {
            IsActivated = false;
            audioSource.Stop();
        }

        public override void Interact(GameObject playerUsingModule)
        {
            if (IsActivated && PlayerUsingModule == null) {
                PlayerUsingModule = playerUsingModule;
                PlayerUsingModule.transform.GetComponent<PlayerController>().inputActivatePanel.SetActive(true);
            }
            GameCycleController.Instance.CountActiveBreach();
            if (GameCycleController.Instance.noActiveBreach)
            {
                GameManager.Instance.hatchActivated = true;
            }
            audioSource.Play();
            PlayerUsingModule.GetComponent<PlayerController>().QuitInteraction();
        }

        public override void StopInteract() {
            PlayerUsingModule.transform.GetComponent<PlayerController>().inputActivatePanel.SetActive(false);
            PlayerUsingModule = null;
        }

        public override void Validate() {
        }
        
        public override void NavigateX(float moveX) { }

        public override void NavigateY(float moveY) { }

        public override void Up() { }

        public override void Down() { }

        public override void Left() { }

        public override void Right() { }
    }
}
