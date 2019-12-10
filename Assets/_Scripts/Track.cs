using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    private List<GameObject> _platforms = new List<GameObject>();

    GameObject currentPlatform;


    [SerializeField] private bool isUseRan = true;
    [SerializeField] private int rand = 123456;

    public GameObject GetCurrentPlatform()
    {
        return currentPlatform;
    }

    

    private void Start()
    {
        _platforms.Add(_platform);

        if (isUseRan)
        {
            Random.InitState(rand);
        }

        for (int i = 0; i < 25; i++)
        {
            GameObject obj = Instantiate(_platform, transform);
            Vector3 pos = Vector3.zero;
            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);
            obj.transform.position = pos;
            obj.name = $"Platform{i + 1}";
            _platforms.Add(obj);
        }
    }

    public bool IsBallOnPlatform(Vector3 pos)
    {
        pos.y = 0;
        var nearestPlatform = _platforms[0];

        for (int i = 1; i < _platforms.Count; i++)
        {
            var platformZ = _platforms[i].transform.position.z;
            if (platformZ + 0.5f < pos.z)
            {
                continue;
            }
            if (platformZ - pos.z > 0.5f)
            {
                continue;
            }
            nearestPlatform = _platforms[i];
            break;
        }

        float minX = nearestPlatform.transform.position.x - 0.5f;
        float maxX = nearestPlatform.transform.position.x + 0.5f;

        currentPlatform = nearestPlatform;

        return pos.x > minX && pos.x < maxX;
    }
}
