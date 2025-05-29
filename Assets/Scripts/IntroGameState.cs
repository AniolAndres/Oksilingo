using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class IntroGameState : GameState
    {
        [SerializeField] 
        private Button _exerciseButton;
        
        [SerializeField]
        private Button _helpButton;

        private void OnEnable()
        {
            _exerciseButton.onClick.AddListener(HandleExerciseClicked);
            _helpButton.onClick.AddListener(HandleHelpClicked);
        }

        private void OnDisable()
        {
            _exerciseButton.onClick.RemoveListener(HandleExerciseClicked);
            _helpButton.onClick.RemoveListener(HandleHelpClicked);
        }

        private void HandleExerciseClicked()
        {
            FireChangeEvent(GameStateType.Exercise);
            AudioManager.Instance.PlayAudio(AudioType.Click);
        }
        
        private void HandleHelpClicked()
        {
            FireChangeEvent(GameStateType.Help);
            AudioManager.Instance.PlayAudio(AudioType.Click);
        }
        
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
            
        }
    }
}