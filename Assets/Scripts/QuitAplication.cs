using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAplication : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape del Juego"); 
            Application.Quit(); 
            
        }
    }
}
