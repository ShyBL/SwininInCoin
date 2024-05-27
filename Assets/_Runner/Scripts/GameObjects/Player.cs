using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class Player : MyMonoBehaviour
{
    // Movement
    [SerializeField] public float forwardSpeed;
    [SerializeField] public float sidewaysSpeed ;
    
    
    [SerializeField] public Transform CollectPoint;
    private Rigidbody rb;
    public bool isStoppingForward = false;
    public bool isStoppingSideways = false;
    [SerializeField] public Popup StopPopupReferance;
    [SerializeField] public Dialogue DialoguePopupReferance;
    public Camera MainCamera => Camera.main;
    
    // Stats
    [SerializeField] public int Health = 10;
    [SerializeField] public bool IsAlive = true;
    
    // Model
    [SerializeField] public List<GameObject> ShipPrefabs;
    public ShipType currentShip;
    [SerializeField] private ParticleSystem GoodsParticle;
    [SerializeField] private ParticleSystem BannedParticle;
    [SerializeField] private ParticleSystem WaterSplashParticle;

    [SerializeField] private int InitialParticleStartSize = 2;
    [SerializeField] private int tutorialInitialForwardSpeed = 3;
    [SerializeField] private int tutorialInitialSidewaysSpeed = 6;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            currentShip = ShipType.Tutorial;
            Instantiate(ShipPrefabs[0], gameObject.transform);

            forwardSpeed = tutorialInitialForwardSpeed;
            sidewaysSpeed = tutorialInitialSidewaysSpeed;
            
            var particleMainModule = WaterSplashParticle.main;
            particleMainModule.startSize = 1;
        }
        else
        {
            forwardSpeed = GameManager.PlayerManager.InitialForwardSpeed;
            sidewaysSpeed = GameManager.PlayerManager.InitialSidewaysSpeed;
            
            currentShip = GameManager.PlayerManager.CurrentShip;
            switch (currentShip)
            {
                case ShipType.Cadet:
                    Instantiate(ShipPrefabs[1], gameObject.transform);
                    break;
                case ShipType.Pirate:
                    Instantiate(ShipPrefabs[2], gameObject.transform);
                    break;
                case ShipType.Privateer:
                    Instantiate(ShipPrefabs[3], gameObject.transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void Update()
    {
        if (!isStoppingForward && forwardSpeed > 0)
        {
            Vector3 forwardMove = transform.forward * (forwardSpeed * Time.deltaTime);
            transform.position += forwardMove;
        }
    }

    public void DoParticle(CollectibleType collectibleType)
    {
        switch (collectibleType)
        {
            case CollectibleType.GoodsCollectibles:
                GoodsParticle.Play();
                break;
            case CollectibleType.BannedCollectibles:
                BannedParticle.Play();
                break;
        }
    }

    public void DoDialogue(string textToDisplay, float timeBetweenLetters )
    {
        if (!isStoppingForward && forwardSpeed > 0)
        {
            Stop(gameObject.transform.position);
        }
        
        DialoguePopupReferance.Initialize(textToDisplay,timeBetweenLetters, this);
    }
    
    public void Stop(Vector3 stopPoint)
    {
        WaterSplashParticle.Stop();
        
        isStoppingForward = true;
        isStoppingSideways = true;
        
        rb.velocity = Vector3.zero;
        DOTween.To(() => sidewaysSpeed, x => sidewaysSpeed = x, 0, 0.5f).SetEase(Ease.OutQuad);
        DOTween.To(() => forwardSpeed, x => forwardSpeed = x, 0, 0.5f).SetEase(Ease.OutQuad);
        transform.DOLocalMove(stopPoint, 2f);
    }

    public void ReleaseFromStop()
    {
        WaterSplashParticle.Play();
        
        isStoppingForward = false;
        isStoppingSideways = false;
        
        float fwdEndValue;
        float swEndValue;
        
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
             fwdEndValue = tutorialInitialForwardSpeed;
             swEndValue = tutorialInitialSidewaysSpeed;
        }
        else
        {
             fwdEndValue = GameManager.PlayerManager.InitialForwardSpeed;
             swEndValue = GameManager.PlayerManager.InitialSidewaysSpeed;
        }
        
        DOTween.To(() => forwardSpeed, x => forwardSpeed = x, fwdEndValue, 1f).SetEase(Ease.OutQuad);
        DOTween.To(() => sidewaysSpeed, x => sidewaysSpeed = x, swEndValue, 1f).SetEase(Ease.OutQuad);
    }
    
    private void FixedUpdate()
    {
        //if (!IsAlive) return;

        if (Input.touchCount > 0 && !isStoppingSideways)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log($"{touch.deltaPosition.x}");

            if (touch.deltaPosition.x < 0)
            {
                Move(false);
                
            }
            else if (touch.deltaPosition.x > 0)
            {
                Move(true);
            }
        }

    }
    
    private void Move(bool left)
    {
        if (left)
        {
            Vector3 touchMove = -transform.right * (sidewaysSpeed * Time.deltaTime);
            rb.AddExplosionForce(sidewaysSpeed, rb.position + touchMove, 0);
        }
        else
        {
            Vector3 touchMove = transform.right * (sidewaysSpeed * Time.deltaTime);
            rb.AddExplosionForce(sidewaysSpeed, rb.position + touchMove, 0);
        }
    }
        
    

    
}