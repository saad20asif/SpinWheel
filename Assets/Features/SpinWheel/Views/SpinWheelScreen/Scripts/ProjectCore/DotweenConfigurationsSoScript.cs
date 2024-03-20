using DG.Tweening;
using UnityEngine;

namespace ProjectCore
{
    [CreateAssetMenu(fileName = "DotweenConfigurationsSoScript", menuName = "ScriptableObjects/Dotween Configurations", order = 1)]
    public class DotweenConfigurationsSoScript : ScriptableObject
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _delay;
        [SerializeField] private Ease _ease;

        public float Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public float Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        public Ease Ease
        {
            get { return _ease; }
            set { _ease = value; }
        }
    }
}

