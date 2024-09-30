using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework.Startup
{
    public class DragNDrop : MonoBehaviour
    {
        [SerializeField] private Camera topDownCamera;
        [SerializeField] private LayerMask draggableLayer;
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] GameObject[] restrictedAreas;
        
        private bool _isDragging;
        private Vector3 _offset;
        private Transform _draggableTransform;
        
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = topDownCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit) && 
                    draggableLayer == (draggableLayer | (1 << hit.collider.gameObject.layer)))
                {
                    _isDragging = true;
                    // Debug.Log("isDragging : " + isDragging);
                    _offset = hit.point - hit.collider.transform.position;
                    // Debug.Log("offset : " + offset);
                    _draggableTransform = hit.collider.transform;
                };
                // Debug.Log("hit object : " + hit.collider.name);
                // Debug.Log("hitobject layer : " + hit.collider.gameObject.layer);
                // Debug.Log("required layer : " + draggableLayer.value);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }

            if (_isDragging)
            {
                Vector3 mousePosition = GetMouseWorldPosition() - _offset;
                Vector3 newPosition = new Vector3(mousePosition.x, 0, mousePosition.z);
                Vector3 clampedPosition = ClampPosition(newPosition);
                if (!IsInRestrictedArea(clampedPosition))
                {
                    _draggableTransform.position = clampedPosition;
                }
            }
        }
        
        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = topDownCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);
                return worldPosition;
            }
            return Vector3.zero;
        }
        
        Vector3 ClampPosition(Vector3 position)
        {
            float z = Mathf.Clamp(
                position.z, 
                levelBounds.bottomBound.position.z, 
                levelBounds.topBound.position.z);
            float x = Mathf.Clamp(
                position.x, 
                levelBounds.leftBound.position.x, 
                levelBounds.rightBound.position.x);
            return new Vector3(x, transform.position.y, z); 
        }
        
        bool IsInRestrictedArea(Vector3 position)
        {
            foreach (GameObject obj in restrictedAreas)
            {
                Collider[] colliders = obj.GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    if (collider.bounds.Contains(position))
                    {
                        return true;
                    }
                }
               
            }
            return false;
        }
        
    }
}