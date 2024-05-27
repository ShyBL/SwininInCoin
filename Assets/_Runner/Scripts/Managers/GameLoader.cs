using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ER.Managers
{
    public class GameLoader : MyMonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] [Range(0, 10)] private int fillAmountDuration;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

        }

        private void Start()
        {
            
            new MyGameManager(DoLoadBar);
        }

        private void DoLoadBar()
        {
            slider.DOValue(1f, fillAmountDuration).OnComplete(MainScene);
        }

        private void TutorialScene()
        {
            SceneManager.LoadScene("Tutorial");
        }
        
        private void HomeScene()
        {
            SceneManager.LoadScene("Home");
        }
        
        private void MainScene()
        {
            SceneManager.LoadScene("Main");
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main" || scene.name == "Tutorial")
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}