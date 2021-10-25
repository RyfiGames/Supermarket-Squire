using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class TransitionState
{
    public static bool showTransition = false;
}

public class SceneTransition : MonoBehaviour
{

    public static SceneTransition main;

    public GameObject transitionObject;
    public Vector3 intoTransition;
    public Vector3 outofTransition;
    public float speed;
    public float transTime;
    public Camera mcamera;
    public float centerDistance;
    public Vector3 gotoPos;

    public float timer;
    private bool outof;
    private string toScene;

    private void Awake()
    {
        main = this;
        mcamera = Camera.main;
        if (TransitionState.showTransition)
        {
            Vector3 cpos = mcamera.transform.position;
            transitionObject.transform.position = new Vector3(0, 0, cpos.z + centerDistance);

            transitionObject.SetActive(true);
            gotoPos = outofTransition;
            outof = false;
        }
        else
        {
            gotoPos = intoTransition;
            transitionObject.SetActive(false);
        }
    }

    public void TransitionTo(string sceneName)
    {
        transitionObject.SetActive(true);
        Vector3 cpos = mcamera.transform.position;
        timer = 0;
        outof = true;
        toScene = sceneName;
        transitionObject.transform.position = cpos + intoTransition;
        gotoPos = new Vector3(cpos.x, cpos.y, centerDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < transTime)
        {
            Vector3 cpos = mcamera.transform.position;
            timer += Time.deltaTime;
            transitionObject.transform.position = Vector3.MoveTowards(transitionObject.transform.position, cpos + gotoPos, speed * Time.deltaTime);
            if (timer >= transTime)
            {
                if (outof)
                {
                    TransitionState.showTransition = true;
                    SceneManager.LoadScene(toScene);
                }
                else
                {
                    transitionObject.SetActive(false);
                }
            }
        }
    }
}
