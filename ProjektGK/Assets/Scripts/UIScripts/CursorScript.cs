//author: Paweł Salicki, Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public bool visible = true;
    public CursorLockMode mode = CursorLockMode.None;

    public void Apply()
    {
        Cursor.lockState = mode;
        Cursor.visible = visible;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = mode;
        Cursor.visible = visible;
    }

    private void Update()
    {
        Cursor.lockState = mode;
        Cursor.visible = visible;
    }
}
