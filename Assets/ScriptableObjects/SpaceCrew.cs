using UnityEngine;

namespace ScriptableObjects
{

    public enum SpaceCrewType
    {
        Red, Yellow, Cyan
    }
    [CreateAssetMenu(menuName = "SpaceCrew", fileName = "SpaceCrew")]
    public class SpaceCrew : UnityEngine.ScriptableObject
    {
        public Sprite alive;
        public Sprite dead;
        public SpaceCrewType type;
    }
}