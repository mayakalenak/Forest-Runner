// resource: https://www.youtube.com/watch?v=xFhScBZdXxg&list=PLvcJYjdXa962PHXFjQ5ugP59Ayia-rxM3&index=6

using UnityEngine;

public class Tiles : MonoBehaviour
{
    // Spawn a new tile when the player leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        Spawner.Instance.SpawnTile();
        Spawner.Instance.RemoveTile(gameObject);
        Destroy(gameObject, 2);
    }
}