using UnityEngine;

public class Map2D : MonoBehaviour
{
    GameObject prefab;
    void Awake()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Field");
    }
    void Start()
    {
        Instantiate(prefab);
    }
}
