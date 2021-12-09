using UnityEngine;

namespace _0_Game.Scripts
{
    public class Collectable : MonoBehaviour
    {
        public void OnCollected()
        {
            Destroy(gameObject);
        }
    }
}