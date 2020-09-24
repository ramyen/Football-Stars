
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RealMan;


public class FieldManager : Singleton<FieldManager>, IManager
{
    const float fTotalFieldX = 75.0f;
    const float fTotalFieldZ = 105.0f;
    const float GRID_SIZE = 3.0f; //1.5f;

    const float GridX = fTotalFieldX / GRID_SIZE;
    const float GridZ = fTotalFieldZ / GRID_SIZE;
    const float fHalfGridX = GridX * 0.5f;
    const float fHalfGridZ = GridZ * 0.5f;

    const float fHalfFieldSizeX = fHalfGridX * GRID_SIZE;
    const float fHalfFieldSizeZ = fHalfGridZ * GRID_SIZE;

    const float fAreaGridSize = 7.5f;
    const float AreaGridX = fTotalFieldX / fAreaGridSize;
    const float AreaGridZ = fTotalFieldZ / fAreaGridSize;
    const float fHalfAreaGridX = AreaGridX * 0.5f;
    const float fHalfAreaGridZ = AreaGridZ * 0.5f;

    Quaternion AwayRot = new Quaternion();
    Vector3 HomeOrigin = new Vector3(-fHalfGridX, 0.0f, -fHalfGridZ);
    Vector3 AwayOrigin = new Vector3(fHalfGridX, 0.0f, fHalfGridZ);
    Vector3 FieldOrigin = new Vector3(-fHalfGridX, 0.0f, -fHalfGridZ);
    Vector3 AreaOrigin = new Vector3(-fHalfAreaGridX, 0.0f, -fHalfAreaGridZ);
    Vector3 GridCenter = new Vector3(0.5f, 0.0f, 0.5f);
    Vector3 CenterField = new Vector3(fHalfGridX, 0.0f, fHalfGridZ);

    bool IManager.Initialize()
	{
		return true;
	}

    public Vector3 GetWorldPos(TeamType teamType, float fieldPosX, float fieldPosZ, bool isSecondPeriod = false)
    {
        Vector3 fieldPos = new Vector3(fieldPosX, 0.0f, fieldPosZ);
        fieldPos = fieldPos + FieldOrigin;

        if ((teamType == TeamType.Home && isSecondPeriod) || (teamType == TeamType.Away && isSecondPeriod == false))
            fieldPos = AwayRot * fieldPos;

        fieldPos *= GRID_SIZE;
        return fieldPos;
    }

    public Vector3 GetFieldPosFromWorldPos(TeamType teamType, Vector3 worldPos, bool isSecondPeriod)
    {
        //Vector3 FieldPos = worldPos / GRID_SIZE;
        Vector3 fieldPos = worldPos * 0.333f;

        if ((teamType == TeamType.Home && isSecondPeriod) || (teamType == TeamType.Away && isSecondPeriod == false))
            fieldPos = AwayRot * fieldPos;

        fieldPos = fieldPos - FieldOrigin;
        return fieldPos;
    }

    public int GetAreaIndexFromWorldPos(TeamType teamType, Vector3 worldPos, bool isSecondPeriod)
    {
        Vector3 areaPos = worldPos * 0.13333f;

        if ((teamType == TeamType.Home && isSecondPeriod) || (teamType == TeamType.Away && isSecondPeriod == false))
            areaPos = AwayRot * areaPos;

        areaPos = areaPos - AreaOrigin;

        if (areaPos.x < 0.0f)
            areaPos.x = 0.5f;
        else if (areaPos.x > AreaGridX)
            areaPos.x = AreaGridX - 0.5f;

        if (areaPos.z < 0.0f)
            areaPos.z = 0.5f;
        else if (areaPos.z > AreaGridZ)
            areaPos.z = AreaGridZ - 0.5f;

        int nAreaIndex = (int)(areaPos.z * AreaGridX) + (int)(areaPos.x);
        return nAreaIndex;
    }

    [SerializeField]
    private bool m_IsDrawFieldLine = true;
    [SerializeField]
    private bool m_IsDrawAreaLine = true;

#if UNITY_EDITOR
    void FixedUpdate()
    {
        if (m_IsDrawFieldLine)
            _DrawFieldLine();

        if (m_IsDrawAreaLine)
            _DrawAreaLine();
    }
#endif//~UNITY_EDITOR
    Vector3 StartPos = Vector3.zero;
    Vector3 EndPos = Vector3.zero;
    Color col = Color.white;
    float fDefaultY = 0.1f;
    void _DrawFieldLine()
    {
        col = Color.gray;

        for (int nX = 0; nX < GridX + 1; nX++)
        {
            StartPos = GetWorldPos(TeamType.Home, nX, 0);
            EndPos = GetWorldPos(TeamType.Home, nX, GridZ);

            StartPos.y = fDefaultY;
            EndPos.y = fDefaultY;
            Debug.DrawLine(StartPos, EndPos, col);
        }

        for (int nZ = 0; nZ < GridZ + 1; nZ++)
        {
            StartPos = GetWorldPos(TeamType.Home, 0, nZ);
            EndPos = GetWorldPos(TeamType.Home, GridX, nZ);

            StartPos.y = fDefaultY;
            EndPos.y = fDefaultY;
            Debug.DrawLine(StartPos, EndPos, col);
        }
    }

    void _DrawAreaLine()
    {
        col = Color.magenta;

        // x
        for (int nX = 0; nX < AreaGridX + 1; nX++)
        {
            StartPos = AreaOrigin;
            StartPos.x += nX;
            StartPos *= fAreaGridSize;

            EndPos.x = StartPos.x;
            EndPos.z = fHalfAreaGridZ * fAreaGridSize;

            StartPos.y = fDefaultY;
            EndPos.y = fDefaultY;
            Debug.DrawLine(StartPos, EndPos, col);
        }

        // z
        for (int nZ = 0; nZ < AreaGridZ + 1; nZ++)
        {
            StartPos = AreaOrigin;
            StartPos.z += nZ;
            StartPos *= fAreaGridSize;

            EndPos.z = StartPos.z;
            EndPos.x = fHalfAreaGridX * fAreaGridSize;

            StartPos.y = fDefaultY;
            EndPos.y = fDefaultY;
            Debug.DrawLine(StartPos, EndPos, col);
        }
    }
}