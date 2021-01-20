using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabCloseButton : MonoBehaviour
{

    public GameObject tab;

    public void CloseTab()
    {
        
        //TODO Play animation or something
        if (IsOpened())tab.SetActive(false);
        
    }

    public void OpenTab()
    {
        
        //TODO Play anim or smth
        if (!IsOpened())tab.SetActive(true);
        
    }

    public bool IsOpened()
    {

        return tab.activeInHierarchy;

    }
}
