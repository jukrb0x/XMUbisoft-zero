using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Controller
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        public Sentence[] sentences;
    }
}