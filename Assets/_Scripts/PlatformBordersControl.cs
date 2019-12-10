using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBordersControl : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private List<GameObject> platforms;
    public List<GameObject> Platforms => platforms;

    private List<CharmingPlatform> _charPl;
    private float startX = 0;

    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private bool moving = true;
    public bool Moving { set { moving = value; } }



    void Start()
    {
        startX = transform.position.x - 4f;
        _prefab = (GameObject)Resources.Load("PlatformPrefab");
        platforms = new List<GameObject>();
        _charPl = new List<CharmingPlatform>();

        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate<GameObject>(_prefab);
            obj.transform.position = new Vector3(startX + i * 2, transform.position.y, transform.position.z);
            obj.transform.SetParent(transform);
            platforms.Add(obj);
            _charPl.Add(obj.GetComponent<CharmingPlatform>());
        }
    }

    private void Update()
    {
        if (moving)
        {
            for (int i = 0; i < platforms.Count; i++)
            {
                Vector3 pos = platforms[i].transform.position;
                pos.x += speed * Time.deltaTime;
                platforms[i].transform.position = pos;

                if (speed < 0)
                {
                    if (platforms[i].transform.position.x <= -6)
                    {
                        platforms[i].transform.position = new Vector3(4f, transform.position.y, transform.position.z);
                    }
                }

                if (speed > 0)
                {
                    if (platforms[i].transform.position.x >= 6f)
                    {
                        platforms[i].transform.position = new Vector3(-4f, transform.position.y, transform.position.z);
                    }
                }
            }
        }
        
    }

    public void ShakeUpDownPlatform() 
    {
        foreach (var item in _charPl)
        {
            item.isJumping = true;
        }
    }

    

}
