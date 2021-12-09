using _0_Game.Scripts.Management;
using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject collectablePrefab;
        [SerializeField] private Transform tileContainer;
        
        public Tile SetTile(Vector3 position)
        {
            var go = Instantiate(tilePrefab, position, Quaternion.identity, tileContainer);
            return go.GetComponent<Tile>();
        }

        public void AddCollectable(Tile tile, int i)
        {
            var position = tile.Position + Vector3.up;
            var addedRot = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
            var rot = collectablePrefab.transform.rotation * addedRot;
            Instantiate(collectablePrefab, position, rot, tileContainer);
        }
    }
}