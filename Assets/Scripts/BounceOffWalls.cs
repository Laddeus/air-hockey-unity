using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BounceOffWalls : MonoBehaviour
{
    private Rigidbody m_PuckRigidbody;

    private void Start()
    {
        m_PuckRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision i_CollisionInfo)
    {
        Collider i_Collider = i_CollisionInfo.collider;
     
        if(i_Collider.CompareTag("Wall"))
        {
            Vector3 directionToWall = m_PuckRigidbody.velocity.normalized;
            Vector3 collisionPointNormal = i_CollisionInfo.contacts[0].normal;
            Vector3 forceDirection = Vector3.Reflect(directionToWall, i_CollisionInfo.contacts[0].normal).normalized;
            float currentForceStrength = m_PuckRigidbody.velocity.sqrMagnitude;
            float forceStrength = Mathf.Max(currentForceStrength, 0f);

            m_PuckRigidbody.velocity = forceDirection * forceStrength;
        }

    }
}
