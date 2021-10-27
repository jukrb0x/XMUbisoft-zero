using UnityEngine;

namespace Core
{
    public class Character : MonoBehaviour
    {
        private enum CharacterTypes
        {
            Player,
            AI
        }

        [SerializeField] private CharacterTypes characterType;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}