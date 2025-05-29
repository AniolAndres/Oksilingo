

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        [SerializeField]
        private MatchPairsExercise _exercise;
        
        public event Action<MatchPairsExercise> OnClick;

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
            OnClick?.Invoke(_exercise);
        }
    }
}