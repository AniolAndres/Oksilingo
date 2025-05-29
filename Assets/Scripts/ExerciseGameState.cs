using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ExerciseGameState : GameState
    {
        [SerializeField]
        List<MenuButton> _buttons;
        
        [SerializeField]
        ChooseExerciseController _chooseExerciseLayout;
        
        [SerializeField]
        ExerciseController _exerciseController;
        
        private void OnEnable()
        {
            foreach (var button in _buttons)
            {
                button.OnClick += HandleButtonClick;
            }

            _exerciseController.OnExerciseCompleted += HandleExerciseCompleted;
            _chooseExerciseLayout.OnBack += HandleBack;
        }


        private void OnDisable()
        {
            foreach (var button in _buttons)
            {
                button.OnClick -= HandleButtonClick;
            }
            
            _exerciseController.OnExerciseCompleted -= HandleExerciseCompleted;
            _chooseExerciseLayout.OnBack -= HandleBack;
        }
        
        private void HandleBack()
        {
            FireChangeEvent(GameStateType.Main);
        }
        
        private void HandleExerciseCompleted(bool completed)
        {
            _chooseExerciseLayout.gameObject.SetActive(true);
            _exerciseController.gameObject.SetActive(false);
        }

        private void HandleButtonClick(MatchPairsExercise exercise)
        {
            _exerciseController.gameObject.SetActive(true);
            _chooseExerciseLayout.gameObject.SetActive(false);
            _exerciseController.Setup(exercise);
        }

        public override void OnEnter()
        {
            _chooseExerciseLayout.gameObject.SetActive(true);
            _exerciseController.gameObject.SetActive(false);
        }

        public override void OnExit()
        {
            
        }
    }
}