using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

#if UNITY_EDITOR
    [SerializeField]
    private Vector2Int _GridSize = new Vector2Int(2,2);

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
                        GridElement gridElement = Instantiate(PrefabElementGameObject, transform.position + new Vector3(x, 0, z), Quaternion.identity,gameObject.transform).GetComponent<GridElement>();
                        if (z == 0)
                        {
                            gridElement.SetBottom(true) ;
                        }

                        if (z == (_GridSize.y-1))
                        {
                            gridElement.SetTop(true);
                        }

                        if (x == 0)
                        {
                            gridElement.SetLeft(true);
                        }

                        if (x == (_GridSize.x-1))
                        {
                            gridElement.SetRight(true);
                        }

                        MapLayout.Add(gridElement);

                    }
                }
            };
        }
    }
#endif
}
