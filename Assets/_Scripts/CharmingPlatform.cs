using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmingPlatform : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;    
    [SerializeField] float _verticalSpeed = 0.5f;

    public bool isJumping = true;
    private float _iteration = 0f;

    void Update()
    {     
        if (isJumping)
        {
            Vector3 pos = transform.position;
            pos.y = _animationCurve.Evaluate(_iteration); // можно домножить на множитель, тогда значения кривой тоже умножатся
            transform.position = pos;
            _iteration += Time.deltaTime * _verticalSpeed; // скорость анимации
            if (_iteration < 1f) return;
            isJumping = false;
            _iteration = 0;
            transform.position = new Vector3(pos.x, 0f, pos.z);
        }
    }
}
