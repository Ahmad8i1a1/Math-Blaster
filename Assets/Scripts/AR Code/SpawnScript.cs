using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] MathSymbol;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(4);
        //for (int i = 0; i < SpawnPoints.Length; i++)
        //{
        //    Instantiate(MathSymbol[i],SpawnPoints[i].position,Quaternion.identity);
        //}
        int i= Random.Range(0, MathSymbol.Length);
        int j= Random.Range(0, SpawnPoints.Length);
        Instantiate(MathSymbol[i], SpawnPoints[j].position, Quaternion.identity);
        StartCoroutine(StartSpawn());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
