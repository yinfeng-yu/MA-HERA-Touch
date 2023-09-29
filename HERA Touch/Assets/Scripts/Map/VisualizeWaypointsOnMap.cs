using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualizeWaypointsOnMap : MonoBehaviour
{
    [SerializeField] Transform waypointParent;
    public GameObject waypointPrefab;

    List<Vector2> waypointPositions;

    private void Start()
    {
        waypointPositions = new List<Vector2>();
    }

    public void AddWaypoint(Vector2 position, int index)
    {
        var go = Instantiate(waypointPrefab, waypointParent);
        go.transform.position = position;

        go.GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();

        waypointPositions.Add(position);
    }

    public void ClearWaypoints()
    {
        foreach (Transform wp in waypointParent)
        {
            Destroy(wp.gameObject);
        }
        waypointPositions.Clear();
    }
}
