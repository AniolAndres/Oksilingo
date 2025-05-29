using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class ChooseExerciseController : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton;

        public event Action OnBack;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(HandleBackButton);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(HandleBackButton);
        }

        private void HandleBackButton()
        {
            OnBack?.Invoke();
            AudioManager.Instance.PlayAudio(AudioType.Click);
        }
    }
}