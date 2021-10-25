using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCollider : MonoBehaviour
{
    public string triggerID;
    public bool activateOnce;
    public bool activated;

    void Awake()
    {
        GameManager.one.RegisterTrigger(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activateOnce && activated)
        {
            return;
        }
        if (other.tag == "Player")
        {
            GameManager.one.ActivateTrigger(triggerID);
            if (activateOnce)
            {
                activated = true;
            }
        }
    }
}
