using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PusherController : MonoBehaviour
{
    private Vector3 m_Offset;
    private float m_ZPosition;
    private bool m_IsMouseDown = false;
    private const float k_MouseMovementSpeedMagnifier = 1000f;
    private Rigidbody m_PusherRigidbody;

    public float MouseMovementSpeed { get; private set; }

    private void Start()
    {
        m_PusherRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(m_IsMouseDown)
        {
            Vector2 mouseMovementDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            MouseMovementSpeed = mouseMovementDelta.magnitude * Time.deltaTime * k_MouseMovementSpeedMagnifier;

            //Debug.Log(MouseMovementSpeed);

            Debug.Log($"Magnitude: {m_PusherRigidbody}");
        }
    }

    private void OnMouseDown()
    {
        Vector3 pusherPosition = transform.position;
        Vector3 currentMousePosition = Input.mousePosition;

        m_ZPosition = Camera.main.WorldToScreenPoint(pusherPosition).z;

        m_Offset = pusherPosition - getMouseWorldPosition();
        m_IsMouseDown = true;
    }

    private void OnMouseUp()
    {
        m_IsMouseDown = false;
    }

    private Vector3 getMouseWorldPosition()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        currentMousePosition.z = m_ZPosition;

        return Camera.main.ScreenToWorldPoint(currentMousePosition);
    }

    private void OnMouseDrag()
    {
        m_PusherRigidbody.MovePosition(getMouseWorldPosition() + m_Offset);
    }
}
