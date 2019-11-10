using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingLine : MonoBehaviour
{
    private LineRenderer _line;
    [SerializeField] private float _reflectDistance = 2f;

    [System.Obsolete]
    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.SetVertexCount(3);
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 reflectAngle = Vector3.Reflect(ray.direction, hit.normal) * _reflectDistance;
            _line.SetPositions(new[] { transform.position, hit.point, reflectAngle });
        }
    }
}
