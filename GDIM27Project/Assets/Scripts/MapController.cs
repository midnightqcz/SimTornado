using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform terrParrent;
    public GameObject terrObject;
    public GameObject tornado;

    private Dictionary<(int x, int y), GameObject> loadedTerrain;
    private Dictionary<(int x, int y), GameObject> temp;
    private Dictionary<(int x, int y), GameobjAndCoroutine> unloadedTerrain;
    private Stack<GameObject> terrainPool;
    private (int x, int y) lastPosition = (0, 0);

    struct GameobjAndCoroutine
    {
        public GameObject Go;
        public Coroutine Cor;
    }
    private void Awake()
    {
        loadedTerrain = new Dictionary<(int x, int y), GameObject>();
        temp = new Dictionary<(int x, int y), GameObject>();
        unloadedTerrain = new Dictionary<(int x, int y), GameobjAndCoroutine>();
        terrainPool = new Stack<GameObject>();
    }

    private void Start()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (loadedTerrain.TryGetValue((i, j), out GameObject terr))
                {
                    temp.Add((i, j), terr);//
                    loadedTerrain.Remove((i, j));
                    terr.transform.position = new Vector3(i * 100f, 0f, j * 100f);
                    terr.SetActive(true);
                }
                else
                {
                    if (unloadedTerrain.TryGetValue((i, j), out GameobjAndCoroutine val))
                    {
                        StopCoroutine(val.Cor);
                        temp.Add((i, j), val.Go);
                        unloadedTerrain.Remove((i, j));
                        val.Go.transform.position = new Vector3(i * 100f, 0f, j * 100f);
                        val.Go.SetActive(true);
                    }
                    else
                    {

                        var newTerr = GetTerrain();
                        temp.Add((i, j), newTerr);
                        newTerr.transform.position = new Vector3(i * 100f, 0f, j * 100f);
                        newTerr.SetActive(true);
                    }
                }
            }
        }
        (loadedTerrain, temp) = (temp, loadedTerrain);
    }

    private void FixedUpdate()
    {
        if (tornado != null)
        {
            (int x, int y) pos = (Mathf.RoundToInt(tornado.transform.position.x / 100f), Mathf.RoundToInt(tornado.transform.position.z / 100f));
            if (!(pos == lastPosition))
            {
                lastPosition = pos;
                temp.Clear();

                for (int i = pos.x - 1; i < pos.x + 2; i++)
                {
                    for (int j = pos.y - 1; j < pos.y + 2; j++)
                    {
                        if (loadedTerrain.TryGetValue((i, j), out GameObject terr))
                        {
                            temp.Add((i, j), terr);
                            loadedTerrain.Remove((i, j));
                            terr.transform.position = new Vector3(i * 100f, 0f, j * 100f);
                            terr.SetActive(true);
                        }
                        else
                        {
                            if (unloadedTerrain.TryGetValue((i, j), out GameobjAndCoroutine val))
                            {
                                StopCoroutine(val.Cor);
                                temp.Add((i, j), val.Go);
                                unloadedTerrain.Remove((i, j));
                                val.Go.transform.position = new Vector3(i * 100f, 0f, j * 100f);
                                val.Go.SetActive(true);
                            }
                            else
                            {
                                var newTerr = GetTerrain();
                                temp.Add((i, j), newTerr);
                                newTerr.transform.position = new Vector3(i * 100f, 0f, j * 100f);
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
                (loadedTerrain, temp) = (temp, loadedTerrain);
            }
        }
    }

    private IEnumerator RemoveTerrDelay((int x, int y) pos)
    {
        yield return new WaitForSeconds(1f);
        if (unloadedTerrain.TryGetValue(pos, out var v))
        {
            RecycleTerrain(v.Go);
            unloadedTerrain.Remove(pos);
        }
    }

    
    private GameObject GetTerrain()
    {
        if (terrainPool.Count > 0)
        {
            return terrainPool.Pop();
        }
        return Instantiate(terrObject, terrParrent);

    }

  
    private void RecycleTerrain(GameObject t)
    {
        t.SetActive(false);
        terrainPool.Push(t);
    }
}
