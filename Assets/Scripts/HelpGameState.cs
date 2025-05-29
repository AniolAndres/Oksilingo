
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class HelpGameState : GameState
    {
        [SerializeField]
        Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            FireChangeEvent(GameStateType.Main);
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

    
