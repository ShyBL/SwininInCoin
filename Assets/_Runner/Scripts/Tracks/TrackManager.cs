using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrackManager : MyMonoBehaviour
{
    [SerializeField] private GameObject firstTrackTile;
    [SerializeField] private GameObject lastTrackTile;

    [SerializeField] private Vector3 nextSpawnPoint;
    [SerializeField] [Range(1,10)] private int SpawnObstacleAmount, SpawnCollectableAmount;
    [SerializeField] [Range(1, 10)]private int SpawnFirstTileAmount;

    // Data
    //[SerializeField] private List<TrackData> tutorialTrack = new ();
    [SerializeField] private List<GameObject> startTrack = new ();

    private void Start()
    {
        SpawnFirstTile();
        var temp = startTrack.Count;
        for (int i = 0; i <= temp; i++)
        {
            SpawnTile();
        }
    }
    
    public void SpawnFirstTile()
    {
        GameObject tile = Instantiate(firstTrackTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = tile.transform.GetChild(0).transform.position;
        tile.GetComponent<Track>().Initialize(this);
    }
    
    public void SpawnTile()
    {
        if (startTrack.Count != 0)
        {
            GameObject tile = Instantiate(startTrack[0], nextSpawnPoint, Quaternion.identity);
            startTrack.Remove(startTrack[0]);
            nextSpawnPoint = tile.transform.GetChild(0).transform.position;
            tile.GetComponent<Track>().Initialize(this);
        }
    }

    private void OnEnable ()
    {
        if ( !Application.isPlaying )
            return;
    
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }
    
    private void OnDisable ()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }
    
    private static void OnBeginCameraRendering (ScriptableRenderContext ctx,
        Camera cam)
    {
        cam.cullingMatrix = Matrix4x4.Ortho(-99, 99, -99, 99, 0.001f, 99) *
                            cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering (ScriptableRenderContext ctx,
        Camera cam)
    {
        cam.ResetCullingMatrix();
    }
}

public class TrackData : ScriptableObject
{
    [Range(1, 100)]
    public int tileAmount; 
    public int obstaclesAmount;
    public int collectablesAmount;
}