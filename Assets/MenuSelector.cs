using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> windows;

    public void CloseAll()
    {
        foreach (var gameObject in windows)
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
