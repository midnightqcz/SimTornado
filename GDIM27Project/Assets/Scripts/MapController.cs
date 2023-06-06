using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform player;
    [Header("MapController")]
    public Transform terrParent;
    public Vector2 inputedTerrainSize = new Vector2(10, 10);
    public StructData[] datas;
    public List<GameObject> randomizedTerrainObject;
    public float existTimeBeforeRemoval = 3f;

    private Vector2 terrainSize;
    private Dictionary<Vector2, GameObject> fixedLoadedTerrain;
    private Dictionary<(int x, int y), GameObject> loadedTerrain;
    private Dictionary<(int x, int y), GameObject> tempDict;
    private Dictionary<(int x, int y), GameobjAndCoroutine> unloadedTerrain;
    private List<GameObject> loadedTerrainList;
    private (int x, int y) lastPosition = (0, 0);

    struct GameobjAndCoroutine
    {
        public GameObject Go;
        public Coroutine Cor;
    }

    [System.Serializable]
    public struct StructData
    {
        public Vector2 key;
        public GameObject value;
    }

    private void Awake()
    {
        terrainSize = new Vector2(inputedTerrainSize.x * 10, inputedTerrainSize.y * 10);
        fixedLoadedTerrain = new Dictionary<Vector2, GameObject>();
        loadedTerrain = new Dictionary<(int x, int y), GameObject>();
        tempDict = new Dictionary<(int x, int y), GameObject>();
        unloadedTerrain = new Dictionary<(int x, int y), GameobjAndCoroutine>();
        loadedTerrainList = new List<GameObject>();
        InitData();
    }

    private void Start()
    {
        FirstLoadTerrain();
    }

    private void FixedUpdate()
    {
        LoadTerrain();
    }

    void InitData()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (fixedLoadedTerrain.ContainsKey(datas[i].key))
                return;
            fixedLoadedTerrain.Add(datas[i].key, datas[i].value);
        }
    }
    private void FirstLoadTerrain()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (loadedTerrain.TryGetValue((i, j), out GameObject terr))
                {
                    tempDict.Add((i, j), terr);
                    loadedTerrain.Remove((i, j));
                    terr.transform.position = new Vector3(i * terrainSize.x, 0f, j * terrainSize.y);
                    terr.SetActive(true);

                }
                else
                {
                    if (unloadedTerrain.TryGetValue((i, j), out GameobjAndCoroutine val))
                    {
                        StopCoroutine(val.Cor);
                        tempDict.Add((i, j), val.Go);
                        unloadedTerrain.Remove((i, j));
                        val.Go.transform.position = new Vector3(i * terrainSize.x, 0f, j * terrainSize.y);
                        val.Go.SetActive(true);
                    }
                    else
                    {

                        var newTerr = GetTerrainNew(i, j);
                        tempDict.Add((i, j), newTerr);
                        newTerr.transform.position = new Vector3(i * terrainSize.x, 0f, j * terrainSize.y);
                        newTerr.SetActive(true);
                    }
                }
            }
        }
        (loadedTerrain, tempDict) = (tempDict, loadedTerrain);
    }

    private void LoadTerrain()
    {
        if (player != null)
        {
            (int x, int y) pos = (Mathf.RoundToInt(player.position.x / terrainSize.x), Mathf.RoundToInt(player.position.z / terrainSize.y));
            if (!(pos == lastPosition))
            {
                lastPosition = pos;
                tempDict.Clear();
                for (int i = pos.x - 1; i < pos.x + 2; i++)
                {
                    for (int j = pos.y - 1; j < pos.y + 2; j++)
                    {
                        if (loadedTerrain.TryGetValue((i, j), out GameObject terr))
                        {
                            tempDict.Add((i, j), terr);
                            loadedTerrain.Remove((i, j));
                            terr.transform.position = new Vector3(i * terrainSize.x, 0f, j * terrainSize.y);
                            terr.SetActive(true);
                        }
                        else
                        {
                            if (unloadedTerrain.TryGetValue((i, j), out GameobjAndCoroutine val))
                            {
                                StopCoroutine(val.Cor);
                                tempDict.Add((i, j), val.Go);
                                unloadedTerrain.Remove((i, j));
                                val.Go.transform.position = new Vector3(i * terrainSize.x, 0f, j * terrainSize.y);
                                val.Go.SetActive(true);
                            }
                            else
                            {
                                var newTerr = GetTerrainNew(i, j);
                                tempDict.Add((i, j), newTerr);
                                newTerr.transform.position = new Vector3(i * terrainSize.x, 0f, j * terrainSize.y);
                                newTerr.SetActive(true);
                            }
                        }
                    }
                }

                
                foreach (var item in loadedTerrain)
                {
                    unloadedTerrain.Add(item.Key, new GameobjAndCoroutine
                    {
                        Cor = StartCoroutine(RemoveTerrDelay(item.Key)),
                        Go = item.Value,
                    });
                }
                loadedTerrain.Clear();
                (loadedTerrain, tempDict) = (tempDict, loadedTerrain);
            }
        }
    }

    private IEnumerator RemoveTerrDelay((int x, int y) pos)
    {
        yield return new WaitForSeconds(existTimeBeforeRemoval);
        if (unloadedTerrain.TryGetValue(pos, out var v))
        {
            RecycleTerrain(v.Go);
            unloadedTerrain.Remove(pos);
        }
    }


    private void RecycleTerrain(GameObject t)
    {
        Destroy(t);
        //t.SetActive(false);
    }

    private GameObject GetTerrainNew(int x, int y)
    {
        if (fixedLoadedTerrain.TryGetValue(new Vector2(x, y), out GameObject terr))
        {
            if (!loadedTerrainList.Contains(terr))
            {
                loadedTerrainList.Add(terr);
                return Instantiate(terr, terrParent); ;
            }
            return Instantiate(terr, terrParent); ;
        }

        int randomTer = Random.Range(0, randomizedTerrainObject.Count);
        return Instantiate(randomizedTerrainObject[randomTer], terrParent);
    }
}
