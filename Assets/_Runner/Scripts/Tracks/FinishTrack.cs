using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrack : Track
{
    [SerializeField] private Transform stopPoint;
    [SerializeField] private string name;
    [SerializeField] private string description;
    public TrackType type => TrackType.End;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Stop(stopPoint.position);
            
            var temp = player.StopPopupReferance;
            
            temp.InitializePopup(name, description, type, player);
            temp.button.onClick.AddListener(Finish);
        }
    }

    void Finish()
    {
        SceneManager.LoadScene("Home");
    }
}