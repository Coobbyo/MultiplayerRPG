using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    private void update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            InputWindow.Show_Static("Say what?", "", "abcdefghijklmnopqrstuvwxyz!", 15);
        }
    }
}
