using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public void GoAhead()
    {
        var player = GameObject.Find("Player");
        player.GetComponent<AutoPlayerMotion>().enabled = true;
        player.GetComponent<PlayerMotionControl>().enabled = true;
        GameObject.Find("SCREEN_Start").SetActive(false);
    }
}
