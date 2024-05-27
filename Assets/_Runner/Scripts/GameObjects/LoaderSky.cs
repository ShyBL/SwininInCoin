using DG.Tweening;
using UnityEngine;

public class LoaderSky : MonoBehaviour
{
    [SerializeField] private float _cycleLength = 2;


    // Update is called once per frame
    void Update()
    {
        transform.DORotate(new Vector3(0, 360, 0), _cycleLength * 0.5f, RotateMode. FastBeyond360).SetLoops (-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}