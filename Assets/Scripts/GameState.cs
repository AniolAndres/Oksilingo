using System;
using UnityEngine;

namespace Scripts
{

    public abstract class GameState : MonoBehaviour
    {
        public abstract void OnEnter();

        public abstract void OnExit();

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        protected void FireChangeEvent(GameStateType type)
        {
            OnChange?.Invoke(type);
        }
        
        public event Action<GameStateType> OnChange;
    }

    public enum GameStateType
    {
        Main,
        Exercise,
    }

}