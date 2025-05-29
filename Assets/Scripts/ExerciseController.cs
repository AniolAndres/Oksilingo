using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class ExerciseController : MonoBehaviour
    {
        [SerializeField]
        Button _backButton;
        
        [SerializeField] 
        private List<GenericButtonExercise> _leftButtons;
        
        [SerializeField] 
        private List<GenericButtonExercise> _rightButtons;
        
        [SerializeField]
        private PlayableDirector _winDirector;
        
        [SerializeField]
        GameObject _winPanel;
        
        Dictionary<string,string> _pairs;

        private GenericButtonExercise _selectedOption;

        public event Action<bool> OnExerciseCompleted;

        private int _completed = 0;
        
        Coroutine _coroutine;
        
        public void OnEnable()
        {
            foreach (var b in _rightButtons)
            {
                b.OnClicked += HandleClick;
            }

            foreach (var b in _leftButtons)
            {
                b.OnClicked += HandleClick;
            }
            
            _backButton.onClick.AddListener(HandleBackButtonClicked);
        }

        private void OnDisable()
        {
            foreach (var b in _rightButtons)
            {
                b.OnClicked -= HandleClick;
            }

            foreach (var b in _leftButtons)
            {
                b.OnClicked -= HandleClick;
            }
            
            _backButton.onClick.RemoveListener(HandleBackButtonClicked);
        }

        private void HandleBackButtonClicked()
        {
            OnExerciseCompleted?.Invoke(false);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private void HandleClick(GenericButtonExercise option)
        {
            //First click, nothing selected
            if (_selectedOption == null)
            {
                _selectedOption = option;
                _selectedOption.SetState(ButtonState.Selected); 
                AudioManager.Instance.PlayAudio(AudioType.Click);
                return;
            }

            //Click same column
            if (_selectedOption.IsLeft == option.IsLeft)
            {
                _selectedOption.SetState(ButtonState.Idle);
                _selectedOption = option;
                _selectedOption.SetState(ButtonState.Selected);
                AudioManager.Instance.PlayAudio(AudioType.Click);
                return;
            }

            if (_pairs.TryGetValue(_selectedOption.Text, out var result1))
            {
                if (string.Equals(result1, option.Text))
                {
                    SetAsCompleted(option);
                    return;
                }
            }else if (_pairs.TryGetValue(option.Text, out var result))
            {
                if (string.Equals(result, _selectedOption.Text))
                {
                    SetAsCompleted(option);
                    return;
                }
            }

            ShowError(option);
        }

        private void ShowError(GenericButtonExercise option)
        {
            option.Shake();
            _selectedOption.Shake();
            
            _selectedOption.SetState(ButtonState.Idle);
            Debug.LogError(_selectedOption.Text);
            _selectedOption = null;
            AudioManager.Instance.PlayAudio(AudioType.Error);
        }

        private void SetAsCompleted(GenericButtonExercise option)
        {
            _selectedOption.SetState(ButtonState.Completed);
            option.SetState(ButtonState.Completed);
            _selectedOption = null;

            _completed++;
            AudioManager.Instance.PlayAudio(AudioType.Win);

            if (_completed >= _pairs.Count)
            {
                _coroutine = StartCoroutine(DelayedWin());
            }
        }

        private IEnumerator DelayedWin()
        {
            _winDirector.Play();
            AudioManager.Instance.PlayAudio(AudioType.Congratulation);
            yield return new WaitForSeconds(3.0f);
            
            OnExerciseCompleted?.Invoke(true);
        }

        public void Setup(MatchPairsExercise exercise)
        {
            _winPanel.SetActive(false);
            
            _pairs = new Dictionary<string, string>();

            var pairs = new List<MatchPair>(exercise.MatchPairs);
            pairs.Shuffle();

            var straight = Random.Range(0, 2) == 0;
            var leftIndexList = new List<int> { 0, 1, 2, 3, 4 }; 
            var rightIndexList = new List<int> { 0, 1, 2, 3, 4 };
            leftIndexList.Shuffle();
            rightIndexList.Shuffle();
            
            for (int i = 0; i < leftIndexList.Count; i++)
            {
                var pair = pairs[i];
                _pairs.Add(pair.First, pair.Second);
                
                var leftIndex = leftIndexList[i];
                var rightIndex = rightIndexList[i];
                
                var leftButton = _leftButtons[leftIndex];
                leftButton.SetState(ButtonState.Idle);
                leftButton.SetText(straight ? pair.First : pair.Second);
                
                var rightButton = _rightButtons[rightIndex];
                rightButton.SetState(ButtonState.Idle);
                rightButton.SetText(straight ? pair.Second : pair.First);
            }
        }
    }
}