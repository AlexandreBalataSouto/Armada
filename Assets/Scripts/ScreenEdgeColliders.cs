using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEdgeColliders : MonoBehaviour
{
    //Add a collider around the camera view

    private Camera _cam;
    private Vector2 _bottomLeft, _topLeft, _topRight, _bottomRight;
    private Vector2[] _edgePoints;
    private EdgeCollider2D _edge;

    void Update()
    {
        AddCollider();
    }

    void AddCollider()
    {
        if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

        _cam = Camera.main;
        if (!_cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

        _bottomLeft = (Vector2)_cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
        _topLeft = (Vector2)_cam.ScreenToWorldPoint(new Vector3(0, _cam.pixelHeight, _cam.nearClipPlane));
        _topRight = (Vector2)_cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth, _cam.pixelHeight, _cam.nearClipPlane));
        _bottomRight = (Vector2)_cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth, 0, _cam.nearClipPlane));

        // add or use existing EdgeCollider2D
        _edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

        _edgePoints = new[] { _bottomLeft, _topLeft, _topRight, _bottomRight, _bottomLeft };
        _edge.points = _edgePoints;
    }
}
