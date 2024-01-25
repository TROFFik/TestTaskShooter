using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }

    [SerializeField] private Bullet _poolObject = null;
    [SerializeField] private int _minObjectCount = 0;
    [SerializeField] private int _maxObjectCount = 0;

    private int _currentObjectCount = 0;
    private List<Bullet> _listObjects = new List<Bullet>();

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        CreatePool();
    }
    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreatePool()
    {
        _currentObjectCount = _minObjectCount;
        _listObjects = new List<Bullet>(_minObjectCount);

        for (int i = 0; i < _minObjectCount; i++)
        {
            CreateObject();
        }
    }

    private Bullet CreateObject()
    {
        Bullet TempObject;
        TempObject = Instantiate(_poolObject.gameObject, transform).GetComponent<Bullet>();
        _listObjects.Add(TempObject);
        TempObject.gameObject.SetActive(false);
        TempObject.collisionAction += ReturnPoolObject;
        return TempObject;
    }

    public Bullet GetPoolObject()
    {
        foreach (var item in _listObjects)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }
        if (_currentObjectCount < _maxObjectCount)
        {
            _currentObjectCount++;
            return CreateObject();
        }

        return null;
    }

    private void ReturnPoolObject(Bullet projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.gameObject.transform.position = transform.position;
    }

    private void OnDestroy()
    {
        foreach (var projectile in _listObjects)
        {
            projectile.collisionAction -= ReturnPoolObject;
        }
    }
}
