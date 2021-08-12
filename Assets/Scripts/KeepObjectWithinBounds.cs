using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectWithinBounds : MonoBehaviour
{
    public Transform m_HockeyTable;
    private float m_LeftBoundary;
    private float m_RightBoundary;
    private float m_FrontBoundary;
    private float m_BackBoundary;
    private Bounds m_ObjectBounds;

    // Start is called before the first frame update
    void Start()
    {
        InitializeHockeyTableBoundaries();
        m_ObjectBounds = GetComponent<Collider>().bounds;
    }

    private void InitializeHockeyTableBoundaries()
    {
        Transform leftWallTransform = m_HockeyTable.Find("LeftWall");
        Transform rightWallTransform = m_HockeyTable.Find("RightWall");
        Transform frontWallTransform = m_HockeyTable.Find("FrontWall");
        Transform backWallTransform = m_HockeyTable.Find("BackWall");
        Bounds leftRightWallsBounds = leftWallTransform.GetComponent<MeshRenderer>().bounds;
        Bounds frontBackWallsBounds = frontWallTransform.GetComponent<MeshRenderer>().bounds;

        m_LeftBoundary = leftWallTransform.position.x + leftRightWallsBounds.size.x / 2;
        m_RightBoundary = rightWallTransform.position.x - leftRightWallsBounds.size.x / 2;

        m_BackBoundary = backWallTransform.position.z + frontBackWallsBounds.size.z / 2;
        m_FrontBoundary = frontWallTransform.position.z - frontBackWallsBounds.size.z / 2;
    }

    private void LateUpdate()
    {
        keepWithinBoundaries();
    }

    private void keepWithinBoundaries()
    {
        Vector3 currentObjectPosition = transform.position;

        if (currentObjectPosition.x - m_ObjectBounds.size.x / 2 < m_LeftBoundary)
        {
            currentObjectPosition.x = m_LeftBoundary + m_ObjectBounds.size.x / 2;
        }

        if (currentObjectPosition.x + m_ObjectBounds.size.x / 2 > m_RightBoundary)
        {
            currentObjectPosition.x = m_RightBoundary - m_ObjectBounds.size.x / 2;
        }

        if (currentObjectPosition.z - m_ObjectBounds.size.z / 2 < m_BackBoundary)
        {
            currentObjectPosition.z = m_BackBoundary + m_ObjectBounds.size.z / 2;
        }

        if (currentObjectPosition.z + m_ObjectBounds.size.z / 2 > m_FrontBoundary)
        {
            currentObjectPosition.z = m_FrontBoundary - m_ObjectBounds.size.z / 2;
        }

        transform.position = currentObjectPosition;
    }
}
