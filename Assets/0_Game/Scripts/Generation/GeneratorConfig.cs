using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    [CreateAssetMenu(menuName = "SO/GeneratorConfig", fileName = "GeneratorConfig", order = 0)]
    public class GeneratorConfig : ScriptableObject
    {
        public GenerationStrategy Strategy;
        [Range(0.25f, 10)] 
        public int Width;
        public int minLength;
        public int maxLength;
    }
}