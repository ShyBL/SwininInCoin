using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Track : MonoBehaviour
{
    private TrackManager _trackManager;
    
    [SerializeField] [Range(0,10)] private int badCollectiblesAmount;
    [SerializeField] [Range(0,10)] private int goodCollectibleAmount;
    [SerializeField] private Transform buildingSpawnPoint;
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private GameObject bannedCollectibles;
    [SerializeField] private GameObject goodsCollectibles;
    [SerializeField] private Collider goodCollectablesCollider;
    [SerializeField] private Collider badCollectablesCollider;
    [SerializeField] private TrackType trackType;
    
    public void Initialize(TrackManager trackManager)
    {
        _trackManager = trackManager;
        SpawnBadCollectibles(badCollectiblesAmount);
        SpawnGoodCollectibles(goodCollectibleAmount);
        
        if (trackType != TrackType.None || trackType != TrackType.End)
        {
            SpawnBuilding(trackType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if (other.GetComponent<Player>())
        // {
        //     if (SceneManager.GetActiveScene().name == "Main")
        //     {
        //         Destroy(gameObject, 20);
        //     }
        // }
    }

    void SpawnBuilding(TrackType track)
    {
        var spawnPoint = buildingSpawnPoint.position;
        var building = Instantiate(buildingPrefab, spawnPoint, Quaternion.identity);
        building.transform.SetParent(buildingSpawnPoint);  
    }
    
    void SpawnBadCollectibles(int amount)
    {
       for (int i = 0; i < amount; i++)
       {
           var spawnPoint = GetRandomPointInCollider(badCollectablesCollider);
           var obstacle = Instantiate(bannedCollectibles, spawnPoint + (Vector3.down * 1.5f), Quaternion.identity);
           
           obstacle.GetComponent<Collectible>().type = CollectibleType.BannedCollectibles;
       }
      
    }
    
    void SpawnGoodCollectibles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var spawnPoint = GetRandomPointInCollider(goodCollectablesCollider);
            var collectable = Instantiate(goodsCollectibles, spawnPoint + (Vector3.down * 1.5f), Quaternion.identity);
            
            collectable.GetComponent<Collectible>().type = CollectibleType.GoodsCollectibles;
        }
    }
    
    Vector3 GetRandomPointInCollider (Collider collider)
    {
        var bounds = collider.bounds;
        Vector3 point = new Vector3 (
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y), 
            Random.Range(bounds.min.z, bounds.max.z)
        );
        
        if (point != collider.ClosestPoint(point)) 
        {
            point = GetRandomPointInCollider(collider);
        }
        return point;
    }
}
