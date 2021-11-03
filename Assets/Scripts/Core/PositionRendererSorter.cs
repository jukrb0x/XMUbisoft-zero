using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// This only works for mesh renderer and sprite renderer
    /// automatically adjust the sorting layer order
    /// </summary>
    public class PositionRendererSorter : MonoBehaviour
    {
        [SerializeField] private int _sortingOrderBase = 5;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = gameObject.GetComponent<Renderer>();
        }

        private void LateUpdate()
        {
            _renderer.sortingOrder = (int) (_sortingOrderBase - transform.position.y);
        }
    }
    
}