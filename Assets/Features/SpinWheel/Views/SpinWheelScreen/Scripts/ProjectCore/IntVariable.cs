using UnityEngine;

namespace ProjectCore
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObjects/Variables/IntVariable", order = 1)]
    public class IntVariable : ScriptableObject
    {
        public int value;
    }
}

