using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Scripts
{
    public class GenericButtonExercise :MonoBehaviour
    {
        [SerializeField] 
        private Button _button;

        [SerializeField] 
        private TextMeshProUGUI _textComponent;

        [SerializeField] 
        private GameObject _idle;
        
        [SerializeField] 
        private GameObject _selected;
        
        [SerializeField] 
        private GameObject _completed;

        [SerializeField] 
        private bool _isLeft;

        [SerializeField] private PlayableDirector _shakeDirector;
        [SerializeField] private PlayableDirector _pressDirector;

        public bool IsLeft => _isLeft;

        public event Action<GenericButtonExercise> OnClicked;

        private string _text;

        public string Text => _text;

        public void SetText(string text)
        {
            _textComponent.SetText(text);
            _text = text;
        }

        public void Shake()
        {
            _shakeDirector.Play();   
        }
        
        public void SetState(ButtonState state)
        {
            switch (state)
            {
                case ButtonState.Idle:
                    _idle.SetActive(true);
                    _selected.SetActive(false);
                    _completed.SetActive(false);
                    _button.interactable = true;
                    break; 
                case ButtonState.Completed:
                    _completed.SetActive(true);
                    _idle.SetActive(false);
                    _selected.SetActive(true);
                    _button.interactable = false;
                    break;
                case ButtonState.Selected:
                    _completed.SetActive(false);
                    _selected.SetActive(true);
                    _idle.SetActive(false);
                    break;
                    default:
                        throw new NotImplementedException();
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnClicked?.Invoke(this);
            _pressDirector.Play();
        }
    }

    public enum ButtonState
    {
        Idle,
        Selected,
        Completed
    }
}