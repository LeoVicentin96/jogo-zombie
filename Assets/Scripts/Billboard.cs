using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward); // o billboard faz a o canvas sempre olhar para a nossa main camera
        
    }
}
