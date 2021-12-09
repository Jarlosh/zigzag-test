using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    [CreateAssetMenu(menuName = "SO/GeneratorConfig", fileName = "GeneratorConfig", order = 0)]
    public class GeneratorConfig : ScriptableObject
    {
        public GenerationType Type;
        [Range(0.25f,10)] public float Width;
        public int minLength;
        public int maxLength;
    }
}