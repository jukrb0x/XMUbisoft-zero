using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private float _vertical;
    private const float VelocityFraction = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 45;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody2D.position;
        position.x += VelocityFraction * _horizontal * Time.deltaTime;
        position.y += VelocityFraction * _vertical * Time.deltaTime;
        _rigidbody2D.MovePosition(position);
    }

}
