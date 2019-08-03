using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    private KillZone _killZone;

    private KillZone GetKillzone()
    {
        if (_killZone == null)
        {
            _killZone = KillZone.Instance();
        }

        return _killZone;
    }

#if UNITY_EDITOR
    [SerializeField]
    private Vector2Int _GridSize = new Vector2Int(2, 2);

    [SerializeField] private GameObject PrefabElementGameObject;

    [SerializeField]
    private bool ResetGrid = false;

    [SerializeField, HideInInspector]
    private List<GridElement> MapLayout = new List<GridElement>();

    private void OnValidate()
    {
        if (MapLayout == null)
        {
            MapLayout = new List<GridElement>();
        }

        if (ResetGrid)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                for (int i = MapLayout.Count - 1; i >= 0; i--)
                {
                    if (MapLayout[i] != null)
                    {
                        MapLayout[i].Clear();
                        DestroyImmediate(MapLayout[i].gameObject);
                    }

                    MapLayout.RemoveAt(i);
                }

                ResetGrid = false;

                for (int x = 0; x < _GridSize.x; x++)
                {
                    for (int z = 0; z < _GridSize.y; z++)
                    {

                       GameObject obj=  PrefabUtility.InstantiatePrefab(PrefabElementGameObject) as GameObject;
                       obj.transform.parent = gameObject.transform;
                       obj.transform.position = transform.position + new Vector3(x, 0, z);
                       obj.transform.rotation = Quaternion.identity;
                      
                       GridElement gridElement = obj.GetComponent<GridElement>();


                        if (z == 0)
                        {
                            gridElement.SetBottom(true);
                        }

                        if (z == (_GridSize.y - 1))
                        {
                            gridElement.SetTop(true);
                        }

                        if (x == 0)
                        {
                            gridElement.SetLeft(true);
                        }

                        if (x == (_GridSize.x - 1))
                        {
                            gridElement.SetRight(true);
                        }

                        MapLayout.Add(gridElement);

                    }
                }


                GetKillzone().SetCenter(transform.position + ((Vector3.forward * _GridSize.x + Vector3.right * _GridSize.y) * 0.5f) + Vector3.down * 10);
                GetKillzone().SetSize(_GridSize * 2);

            };
        }
    }
#endif


    private void Start()
    {
        GetKillzone().BindOnDieEvent(ResetLevel);

    }

    private void ResetLevel()
    {

    }

}
