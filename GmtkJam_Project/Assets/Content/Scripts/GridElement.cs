using UnityEditor;
using UnityEngine;

enum Direction
{
    Top, Right, Bottom, Left, None
}

public class GridElement : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private bool _Top, _Right, _Bottom, _Left;
    [SerializeField] private GameObject WallPrefab;

    [SerializeField] private GameObject SpecialWalllPrefab;
    [SerializeField] private Direction SpecialWallPrefabLocation = Direction.None;

    [SerializeField, HideInInspector] GameObject[] walls = new GameObject[4];

    [SerializeField] private GameObject RoofPrefab;
    [SerializeField, HideInInspector] private GameObject RoofInstance;



    [SerializeField] private bool HasFloor = true;
    [SerializeField] private GameObject FloorPrefab;
    [SerializeField,HideInInspector] private GameObject FloorInstance;





    public GameObject ground => transform.GetChild(0).gameObject;

    public GameObject GetWall(int Index)
    {
        if (walls[Index] != null)
        {
            return walls[Index];
        }

        if (this != null)
        {
            Transform hierarchy = transform.Find("Wall_" + GetNameForIndex(Index));
            if (hierarchy != null)
            {
                walls[Index] = hierarchy.gameObject;
            }

            return walls[Index];
        }

        return null;
    }

    public void Clear()
    {
        SetTop(false);
        SetRight(false);
        SetBottom(false);
        SetLeft(false);
    }

    float GetBoundX => ground.transform.lossyScale.x / 2;
    float GetBoundZ => ground.transform.lossyScale.z;
    float GetBoundY => ground.transform.lossyScale.y / 2;

    float GetWallBoundY => WallPrefab.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size.y / 2;
    float GetWallBoundZ => WallPrefab.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size.x * 0.5f;

    public void SetTop(bool active)
    {
        _Top = active;
        if (_Top)
        {
            if (GetWall(0) == null)
            {
                CreateWall(0,
                    (Vector3.forward * (GetBoundZ - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.identity);
            }
        }
        else
        {
            if (GetWall(0) != null)
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
            if (GetWall(1) == null)
            {

                CreateWall(1,
                    (Vector3.right * (GetBoundZ - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.Euler(0, 90, 0));

            }
        }
        else
        {
            if (GetWall(1) != null)
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
            if (GetWall(2) == null)
            {
                CreateWall(2,
                    (Vector3.back * (GetBoundZ - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.identity);
            }
        }
        else
        {
            if (GetWall(2) != null)
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
            if (GetWall(3) == null)
            {
                CreateWall(3,
                    (Vector3.left * (GetBoundZ - GetWallBoundZ)) + (Vector3.up * (GetBoundY + GetWallBoundY)),
                    Quaternion.Euler(0, 90, 0));
            }
        }
        else
        {

            if (GetWall(3) != null)
            {
                DestroyImmediate(walls[3]);
            }
        }
    }

    GameObject CreateWall(int index, Vector3 location, Quaternion rotation)
    {
        bool SpawnSpecialPrefab = index == (int) SpecialWallPrefabLocation && SpecialWalllPrefab != null;

        GameObject obj = PrefabUtility.InstantiatePrefab(SpawnSpecialPrefab ? SpecialWalllPrefab : WallPrefab) as GameObject;
        obj.name = "Wall_" + GetNameForIndex(index) + (SpawnSpecialPrefab ? "_Special" : "");
        obj.transform.parent = gameObject.transform;
        obj.transform.position = transform.position + location;
        obj.transform.rotation = rotation;

        walls[index] = obj;

        return obj;
    }

    string GetNameForIndex(int index)
    {
        if (index == 0)
        {
            return "Top";
        }

        if (index == 1)
        {
            return "Right";
        }

        if (index == 2)
        {
            return "Bottom";
        }

        if (index == 3)
        {
            return "Left";
        }

        return "INVALID";
    }

    void CreateFloor()
    {
        if (HasFloor)
        {
            if (FloorInstance == null)
            {
                Transform foundObj;
                while ((foundObj = transform.Find("Floor")) != null)
                {
                    DestroyImmediate(foundObj.gameObject);
                }

                GameObject obj = PrefabUtility.InstantiatePrefab(FloorPrefab) as GameObject;
                if (obj != null && gameObject != null)
                {
                    obj.name = "Floor";
                    obj.transform.parent = gameObject.transform;
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.rotation = Quaternion.identity;
                    FloorInstance = obj;
                }
            }
        }
        else
        {
            if (FloorInstance != null)
            {
                DestroyImmediate(FloorInstance);
                FloorInstance = null;
            }
        }
    }

    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            CreateFloor();

            SetTop(_Top);
            SetRight(_Right);
            SetBottom(_Bottom);
            SetLeft(_Left);


        };


    }
#endif
}
