using UnityEngine;
using ER.Managers;

public class Building : MyMonoBehaviour
{
    [SerializeField] private Transform stopPoint;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private TrackType type;

    private bool _doOnce;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_doOnce)
        {
            if (other.TryGetComponent(out Player player))
            {

                var temp = player.StopPopupReferance;

                temp.InitializePopup(name, description, type, player);

                temp.gameObject.SetActive(true);

                player.Stop(stopPoint.position);

                _doOnce = true;

            }
        }
    }
}