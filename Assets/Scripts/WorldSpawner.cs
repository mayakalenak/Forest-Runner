// resource: https://www.youtube.com/watch?v=xFhScBZdXxg&list=PLvcJYjdXa962PHXFjQ5ugP59Ayia-rxM3&index=6

using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [Header("Tile Settings")]
    public GameObject[] tiles;
    public int scoreBetweenLevels = 10;
    public int tilesPerDifficulty = 3; 
    public int difficulty = 0;
    private int scoreTracker = 0;
    public float tileLength = 50f;
    private float spawnZ = 0;
    private readonly System.Collections.Generic.List<GameObject> activeTiles = new System.Collections.Generic.List<GameObject>();
    public int initialTileCount = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ResetSpawner();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerMovement.Instance?.ResetPlayer();
        if (scene.name == "GameScene")
        {
            ResetSpawner();
        }
    }

    private void ResetSpawner()
    {
        spawnZ = 0f;
        difficulty = 0;
        scoreTracker = 0;
        DestroyExistingTiles();

        for (int i = 0; i < initialTileCount; i++)
        {
            SpawnTile();
        }
    }

    private void DestroyExistingTiles()
    {
        for (int i = activeTiles.Count - 1; i >= 0; i--)
        {
            if (activeTiles[i] != null)
            {
                Destroy(activeTiles[i]);
            }
        }

        activeTiles.Clear();
    }

    private void Update()
    {
        // Increase difficulty as score thresholds are reached
        if (GameManager.Instance.score >= scoreTracker&& 
            difficulty * tilesPerDifficulty < tiles.Length)
        {
            scoreTracker += scoreBetweenLevels * (difficulty + 1);
            difficulty++;
        }

        // Clean up tiles that are far behind the player
        if (PlayerMovement.Instance != null)
        {
            float playerZ = PlayerMovement.Instance.transform.position.z;
            for (int i = activeTiles.Count - 1; i >= 0; i--)
            {
                if (activeTiles[i] != null && activeTiles[i].transform.position.z < playerZ - tileLength * 2)
                {
                    Destroy(activeTiles[i]);
                    activeTiles.RemoveAt(i);
                }
            }
        }
    }

    public void SpawnTile()
    {
        // Calculate how many tiles are currently unlocked
        int availableTiles = Mathf.Max(1, Mathf.Min(difficulty * tilesPerDifficulty, tiles.Length));  
      
        // Pick a random tile from the unlocked range to spawn
        int randomIndex = Random.Range(0, availableTiles);
        
        GameObject spawnedTile = Instantiate(tiles[randomIndex], Vector3.forward * spawnZ, Quaternion.identity);
        activeTiles.Add(spawnedTile);
        spawnZ += tileLength;
    }

    public void RemoveTile(GameObject tile)
    {
        activeTiles.Remove(tile);
    }
}