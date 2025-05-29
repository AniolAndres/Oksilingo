using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ExerciseGameState : GameState
    {
        [SerializeField]
        List<MenuButton> _buttons;
        
        [SerializeField]
        GameObject _chooseExerciseLayout;
        
        [SerializeField]
        ExerciseController _exerciseController;
        
        private void OnEnable()
        {
            foreach (var button in _buttons)
            {
                button.OnClick += HandleButtonClick;
            }

            _exerciseController.OnExerciseCompleted += HandleExerciseCompleted;

        }

        private void HandleExerciseCompleted(bool completed)
        {
            _chooseExerciseLayout.SetActive(true);
            _exerciseController.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            foreach (var button in _buttons)
            {
                button.OnClick -= HandleButtonClick;
            }
            
            _exerciseController.OnExerciseCompleted -= HandleExerciseCompleted;
        }

        private void HandleButtonClick(MatchPairsExercise exercise)
        {
            _exerciseController.gameObject.SetActive(true);
            _chooseExerciseLayout.SetActive(false);
            _exerciseController.Setup(exercise);
        }

        public override void OnEnter()
        {
            _chooseExerciseLayout.SetActive(true);
            _exerciseController.gameObject.SetActive(false);
        }

        public override void OnExit()
        {
            
        }
    }
}