using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCollider : MonoBehaviour
{
    public string triggerID;
    public bool activateOnce;
    private bool activated;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !activated)
        {
            GameManager.one.ActivateTrigger(triggerID);
            if (activateOnce)
            {
                activated = true;
            }
        }
    }
}
