using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    public Gun gun;
    public MountPoint[] mountPoints;
    public float detectionRadius = 10f;  // Radius for SphereCast detection
    public float sphereCastDistance = 15f; // Distance for SphereCast
    public int id = 0;
    
    private Transform nearestEnemy = null;  // The nearest enemy target
    void Start(){
        detectionRadius = (int)GameData.gameDataList[id].gunRadius/2;
    }

    // void OnDrawGizmos()
    // {
    //     #if UNITY_EDITOR
    //     if (nearestEnemy == null) return; // Skip drawing if no nearest enemy is selected

    //     var dashLineSize = 2f;

    //     foreach (var mountPoint in mountPoints)
    //     {
    //         var hardpoint = mountPoint.transform;
    //         var from = Quaternion.AngleAxis(-mountPoint.angleLimit / 2, hardpoint.up) * hardpoint.forward;
    //         var projection = Vector3.ProjectOnPlane(nearestEnemy.position - hardpoint.position, hardpoint.up);

    //         // projection line
    //         Handles.color = Color.white;
    //         Handles.DrawDottedLine(nearestEnemy.position, hardpoint.position + projection, dashLineSize);

    //         // do not draw target indicator when out of angle
    //         if (Vector3.Angle(hardpoint.forward, projection) > mountPoint.angleLimit / 2) return;

    //         // target line
    //         Handles.color = Color.red;
    //         Handles.DrawLine(hardpoint.position, hardpoint.position + projection);

    //         // range line
    //         Handles.color = Color.green;
    //         Handles.DrawWireArc(hardpoint.position, hardpoint.up, from, mountPoint.angleLimit, projection.magnitude);
    //         Handles.DrawSolidDisc(hardpoint.position + projection, hardpoint.up, .5f);
    //     #endif
    //     }
    // }

    void Update()
    {
        // Perform a SphereCast to detect enemies
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, detectionRadius, transform.forward, sphereCastDistance);

        // Filter out enemies that are not in range
        List<Transform> enemiesInRange = new List<Transform>();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                enemiesInRange.Add(hit.transform);
            }
        }

        // Do nothing when no enemies are in range
        if (enemiesInRange.Count == 0) return;

        // Select the nearest enemy if available
        nearestEnemy = GetNearestEnemy(enemiesInRange);

        // Aim at the nearest enemy if it's valid
        if (nearestEnemy)
        {
            var aimed = true;
            foreach (var mountPoint in mountPoints)
            {
                if (!mountPoint.Aim(nearestEnemy.position))
                {
                    aimed = false;
                }
            }

            // Shoot when aimed
            if (aimed)
            {
                gun.Fire();
            }
        }
    }

    // Helper method to get the nearest enemy from the detected list
    public Transform GetNearestEnemy(List<Transform> enemiesInRange)
    {
        Transform closest = null;
        float closestDistance = float.MaxValue;

        foreach (Transform enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
