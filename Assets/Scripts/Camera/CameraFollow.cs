using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float dampTime = 0.3f; // predefined but can be changed
    private Vector3 _cameraPos;
    private Vector3 _velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (!player) return;
        Vector3 position = player.position;
        _cameraPos = new Vector3(position.x, position.y, -10f);
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, _cameraPos, ref _velocity, dampTime);
    }
}