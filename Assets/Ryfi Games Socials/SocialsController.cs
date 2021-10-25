using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialsController : MonoBehaviour
{

    public string websiteLink;
    public string youtubeLink;
    public string discordLink;
    public string instagramLink;
    public string facebookLink;
    public string twitterLink;
    public string snapchatLink;

    public bool randomRabbit;
    public Animator animator;
    private float rabbitTimer;

    private List<string> linknames;
    private string[] links;

    // Start is called before the first frame update
    void Start()
    {
        linknames = new List<string> { "website", "youtube", "discord", "instagram", "facebook", "twitter", "snapchat" };
        links = new string[] { websiteLink, youtubeLink, discordLink, instagramLink, facebookLink, twitterLink, snapchatLink };
    }

    public void ClickButton(string linkname)
    {
        if (linknames.Contains(linkname))
        {
            if (links[linknames.IndexOf(linkname)] != "")
            {
                Application.OpenURL(links[linknames.IndexOf(linkname)]);
            }
            else
            {
                print(linkname + " link is missing");
            }
        }
        else
        {
            print(linkname + " is not a link");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (randomRabbit)
        {
            if (Random.Range(0, 3000) == 0)
            {
                animator.SetTrigger("up");
                rabbitTimer = 1f;
                randomRabbit = false;
            }
        }

        if (rabbitTimer > 0f)
        {
            rabbitTimer -= Time.deltaTime;
            if (rabbitTimer <= 0f)
            {
                animator.SetTrigger("down");
                rabbitTimer = 0f;
                randomRabbit = true;
            }
        }
    }
}
