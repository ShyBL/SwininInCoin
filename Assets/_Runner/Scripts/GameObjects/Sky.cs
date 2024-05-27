using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [SerializeField] private float _cycleLength = 2;

    [SerializeField] private GameObject SkyBox;
    [SerializeField] private GameObject Clouds;

    private void Start()
    {
        Clouds.transform.DOMoveZ(-215,80f);
    }

    // Update is called once per frame
    void Update()
    {
        SkyBox.transform.DOLocalRotate(new Vector3(0, 360,0), _cycleLength * 0.5f, RotateMode. FastBeyond360).SetLoops (-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}