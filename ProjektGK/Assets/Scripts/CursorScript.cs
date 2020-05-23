using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public bool visible = true;
    public CursorLockMode mode = CursorLockMode.None;
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
