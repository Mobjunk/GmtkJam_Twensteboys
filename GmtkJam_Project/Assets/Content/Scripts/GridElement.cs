using UnityEditor;
using UnityEngine;

public class GridElement : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private bool _Top, _Right, _Bottom, _Left;
    [SerializeField] private GameObject WallPrefab;

    [SerializeField, HideInInspector] GameObject[] walls = new GameObject[4];


    public GameObject ground => transform.GetChild(0).gameObject;


    public void Clear()
    {
        SetTop(false);
        SetRight(false);
        SetBottom(false);
        SetLeft(false);
    }

    float GetBoundX => ground.transform.lossyScale.x / 2;
    float GetBoundZ => ground.transform.lossyScale.z / 2;
    float GetBoundY => ground.transform.lossyScale.y / 2;

    float GetWallBoundY => WallPrefab.transform.lossyScale.y / 2;
    float GetWallBoundZ => WallPrefab.transform.lossyScale.z / 2;

    public void SetTop(bool active)
    {
        _Top = active;
        if (_Top)
        {
            if (walls[0] == null)
            {
                CreateWall(0,
                    (Vector3.forward * (GetBoundZ - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.identity);
            }
        }
        else
        {
            if (walls[0] != null)
            {
                DestroyImmediate(walls[0]);
            }
        }
    }

    public void SetRight(bool active)
    {
        _Right = active;
        if (_Right)
        {

            if (walls[1] == null)
            {
                CreateWall(1,
                    (Vector3.right * (GetBoundX - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.Euler(0, 90, 0));

            }
        }
        else
        {
            if (walls[1] != null)
            {
                DestroyImmediate(walls[1]);
            }
        }
    }

    public void SetBottom(bool active)
    {
        _Bottom = active;
        if (_Bottom)
        {
            if (walls[2] == null)
            {
                CreateWall(2,
                    (Vector3.back * (GetBoundZ - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.identity);
            }
        }
        else
        {
            if (walls[2] != null)
            {
                DestroyImmediate(walls[2]);
            }
        }
    }

    public void SetLeft(bool Active)
    {
        _Left = Active;

        if (_Left)
        {
            if (walls[3] == null)
            {
                CreateWall(3,
                    (Vector3.left * (GetBoundZ - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.Euler(0, 90, 0));
            }
        }
        else
        {

            if (walls[3] != null)
            {
                DestroyImmediate(walls[3]);
            }
        }
    }

    GameObject CreateWall(int index, Vector3 location, Quaternion rotation)
    {
        GameObject obj = PrefabUtility.InstantiatePrefab(WallPrefab) as GameObject;
        obj.transform.parent = gameObject.transform;
        obj.transform.position = transform.position + location;
        obj.transform.rotation = rotation;

        walls[index] = obj;

        return obj;
    }

    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            SetTop(_Top);
            SetRight(_Right);
            SetBottom((_Bottom));
            SetLeft(_Left);
        };


    }
#endif
}
