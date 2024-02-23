using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    public void GoBack()
    {
        var player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, 0, -8);
        player.GetComponent<AutoPlayerMotion>().enabled = true;
        player.GetComponent<PlayerMotionControl>().enabled = true;
        GameObject.Find("SCREEN_Restart").SetActive(false);
    }
}
