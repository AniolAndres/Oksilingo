using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class IntroGameState : GameState
    {
        [SerializeField] 
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            FireChangeEvent(GameStateType.Exercise);
        }
        
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
            
        }
    }
}