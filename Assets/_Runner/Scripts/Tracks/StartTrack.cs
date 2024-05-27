using System;
using UnityEngine;

public class StartTrack : Track
{
    [SerializeField] [TextArea] private string textToDisplay;
    private bool _doOnce;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_doOnce)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.DoDialogue(textToDisplay, 1f);
                _doOnce = true;
            }
        }
    }
}