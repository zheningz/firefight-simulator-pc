using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{
    public float MouseSpeed = 1000.0f;
    private float Xmove;
    public Transform Player;

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && GameUI.isPause == false)
        {
            float x, y;
            x = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;
            y = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;
            Xmove = Xmove - y;
            Xmove = Mathf.Clamp(Xmove, -13, 15);

            this.transform.localRotation = Quaternion.Euler(Xmove, 0, 0);
            Player.Rotate(Vector3.up * x);
        }
    }
}
