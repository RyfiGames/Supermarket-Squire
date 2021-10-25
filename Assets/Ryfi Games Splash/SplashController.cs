using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{

    public string mainScene = "";
    public AudioSource bam;
    public AudioSource whoosh;
    public float sceneSwitchTime = 5f;
    private float sceneSwitchTimer;
    private bool sent;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayBam()
    {
        bam.Play();
    }

    public void PlayWhoosh()
    {
        whoosh.Play();
    }

    // Update is called once per frame
    void Update()
    {
        sceneSwitchTimer += Time.deltaTime;
        if (sceneSwitchTimer > sceneSwitchTime && mainScene != "" && !sent)
        {
            //SceneTransition.main.TransitionTo(mainScene);
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainScene);
            sent = true;
        }
    }
}
