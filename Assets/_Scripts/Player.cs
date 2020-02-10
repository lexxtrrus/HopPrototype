using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _jumpHeight =1;
    [SerializeField] private float _speedBall = 1f;
    [SerializeField] private InputPosition _input;
    [SerializeField] private LinesGenerator _lineGeneretor;
    [SerializeField] Text text;

    private bool moving = true;
    [SerializeField ]private float iteration;
    private float startY = 0.2f;
    void Update()
    {
        if (moving)
        {
            var pos = transform.position;
            pos.x = Mathf.Lerp(pos.x, _input.Strafe, Time.deltaTime * 5f);
            
            pos.y = startY + _animationCurve.Evaluate(iteration) * _jumpHeight; // можно увеличить высоту прыжка домножением
            transform.position = pos;
            iteration += Time.deltaTime * _speedBall; // скорость анимации прыжка

            if (iteration < 1f)
            {
                return;
            }

            iteration = 0;
            //transform.position = new Vector3(0f, startY, 0f); // сильно дёргается зараза, надо делать по другому


            _lineGeneretor.Stabilize();
            
            if (_lineGeneretor.IsBallOnPlatform(transform.position))
            {
                GameObject go = _lineGeneretor.CurrentPlatform;
                go.AddComponent<Scaler>();
                return;
            }
            else
            {
                moving = false;
                _lineGeneretor.Moving = false;
                foreach (var item in _lineGeneretor.LINES)
                {
                    item.Moving = false;
                }
                GameObject go = _lineGeneretor.CurrentPlatform;
                go.GetComponent<Renderer>().material.color = Color.red;
                Invoke("Reload", 1.3f);
            }
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
