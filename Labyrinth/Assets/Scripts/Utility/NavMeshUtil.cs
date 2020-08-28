﻿
using UnityEngine.AI;
using UnityEngine;

public static class NavMeshUtil
{
    public static Vector3 GetRandomPoint(Vector3 center)
    {
        float distance = Random.Range(50, 100);
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);
        return hit.position;
    }
}