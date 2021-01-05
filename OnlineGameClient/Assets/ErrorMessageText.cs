using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessageText : MonoBehaviour
{

    public static string currentErrorMessage = "This is an error message. Uh oh. you broke something";

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = currentErrorMessage;
    }
}
