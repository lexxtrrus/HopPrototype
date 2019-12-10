using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesGenerator : MonoBehaviour
{
    [SerializeField] GameObject _linesParent;
    [SerializeField] Player _player;

    [SerializeField] private List<GameObject> _lines;
    private List<PlatformBordersControl> _lineControls; //ссылка на линии, внурти каждой есть ссылка на платформы в этой линии
    public List<PlatformBordersControl> LINES => _lineControls;

    [SerializeField] private float speed = 1f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private bool moving = true;
    public bool Moving { set { moving = value; } }

    PlatformBordersControl currentLine;
    GameObject currentPlatform;
    public GameObject CurrentPlatform => currentPlatform;

    private void Awake()
    {
        float x = 0;
        _lineControls = new List<PlatformBordersControl>();
        _lines = new List<GameObject>();
        for (int i = 0; i < 6; i++)
        {
            GameObject line = new GameObject($"Line: {i}");
            line.transform.position = new Vector3(x, 0f, (float)i * 2f);
            line.transform.SetParent(_linesParent.transform);
            line.AddComponent<PlatformBordersControl>();
            _lineControls.Add(line.GetComponent<PlatformBordersControl>()); // 
            _lines.Add(line);

            if(x == 0)
            {
                x = 1f;
                int speed = Random.Range(-3, 4);
                if (speed == 0) speed += 1;
                _lineControls[i].Speed = speed;
            }
            else
            {
                x = 0;
                int speed = Random.Range(-3, 4);
                if (speed == 0) speed += 1;
                _lineControls[i].Speed = -1f;
            }            
        }
        currentLine = _lineControls[1];
    }

    private void Update()
    {
        if (moving)
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                Vector3 pos = _lines[i].transform.position;
                pos.z -= speed * Time.deltaTime;
                _lines[i].transform.position = pos;

                if (_lines[i].transform.position.z < -2f)
                {
                    _lines[i].transform.position = new Vector3(_lines[i].transform.position.x, 0f, 10f);
                    int speed = Random.Range(-3, 4);
                    if (speed == 0) speed += 1;
                    _lineControls[i].Speed = speed;
                    _lineControls[i].ShakeUpDownPlatform();
                }
            }
        }        
    }

    public void Stabilize()
    {
        foreach (var item in _lines)
        {
            Vector3 l = item.transform.position;
            l.z = Mathf.Round(l.z);
            item.transform.position = l;
        }
    }


    public bool IsBallOnPlatform(Vector3 pos) // буду бегать только в нужных линиях
    {
        var nearestPlatform = currentLine.Platforms[0];

        for (int i = 1; i < currentLine.Platforms.Count; i++)
        {
            var platformX = currentLine.Platforms[i].transform.position.x;
            if (platformX + 1 < pos.x)
            {
                continue;
            }
            if (platformX - 1 > pos.x)
            {
                continue;
            }
            nearestPlatform = currentLine.Platforms[i];
            break;
        }

        float minX = nearestPlatform.transform.position.x - 0.5f;
        float maxX = nearestPlatform.transform.position.x + 0.5f;

        currentPlatform = nearestPlatform;

        int index = _lineControls.IndexOf(currentLine);
        index += 1;
        if (index == 6) index = 0;
        currentLine = _lineControls[index];

        return pos.x > minX && pos.x < maxX;
    }




}
