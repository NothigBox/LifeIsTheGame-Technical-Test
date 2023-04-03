using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFPVMouse : MonoBehaviour
{
    [SerializeField] private Vector2 limitsX;
    [SerializeField] private Vector2 limitsY;

    [SerializeField] private float speedX;
    [SerializeField] private float speedY;

    Transform orientation;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speedX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedY;

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, limitsX.x, limitsX.y);
        rotationY = Mathf.Clamp(rotationY, limitsY.x, limitsY.y);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}

