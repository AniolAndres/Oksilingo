using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MatchPairsExercise", order = 1)]
    public class MatchPairsExercise : ScriptableObject
    {
        [SerializeField]
        List<MatchPair> _matchPairs;

        public List<MatchPair> MatchPairs => _matchPairs;
    }

    [Serializable]
    public class MatchPair
    {
        public string First;
        public string Second;
    }
}