
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class MainGameManager : MonoBehaviour
    {
        [SerializeField] 
        private List<StateObject> _states;

        [SerializeField] 
        private GameStateType _firstState;
        
        private GameState _currentState;

        private void Start()
        {
            foreach (var state in _states)
            {
                state.State.Hide();
                state.State.OnChange += HandleStateChange;
            }
            
            var first = _states.FirstOrDefault(x => x.Type == _firstState);
            if (first == null)
            {
                throw new Exception("First state not found");
            }
            first.State.Show();
            _currentState = first.State;
            _currentState.OnEnter();
        }

        private void OnDestroy()
        {
            foreach (var state in _states)
            {
                state.State.OnChange -= HandleStateChange;
            }
        }

        private void HandleStateChange(GameStateType type)
        {
            var next = _states.FirstOrDefault(x => x.Type == type);
            if (next == null)
            {
                throw new Exception("next state not found");
            }
            _currentState.OnExit();
            _currentState.Hide();
            _currentState = next.State;
            _currentState.Show();
            _currentState.OnEnter();
        }
    }

    [Serializable]
    public class StateObject
    {
        public GameStateType Type;

        public GameState State;
    }
    
}
