using _0_Game.Scripts.Management;
using UnityEngine;

namespace _0_Game.Scripts.Generation
{
    //todo: reimplement as object pool!
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject collectablePrefab;
        [SerializeField] private Transform tileContainer;
        
        public Tile SetTile(Vector3 position, float width, bool lookRight)
        {
            var go = Instantiate(tilePrefab, position, Quaternion.identity, tileContainer);
            var tile = go.GetComponent<Tile>();
            tile.SetWidth(width, lookRight);
            return tile;
        }

        public void AddCollectable(Tile tile, int i)
        {
            var position = tile.Position + Vector3.up;
            var addedRot = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
            var rot = collectablePrefab.transform.rotation * addedRot;
            var go = Instantiate(collectablePrefab, position, rot);
            go.transform.parent = tile.transform;
        }
    }
}