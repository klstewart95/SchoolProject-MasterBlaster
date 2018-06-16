using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

    public Transform Enemy;
    public bool SpawnEnemy;
    public float Access;
    // Use this for initialization
    void Start () {
        Access = 0;
		if (SpawnEnemy)
        {
            SpawnEnemy = false;
        }
	}
	
	// Update is called once per frame
	void Update () {

         Access = Random.Range(1, 100);

        if (!SpawnEnemy && Access == 1)
        {
            SpawnEnemy = true;
            Spawn(-0.34f,Random.Range(-0.3f,-1.1f));
        }
		
	}


    void Spawn(float x , float y)
    {
       int Spawner = Random.Range(1, 3);

        if (Spawner == 1)
        {
            Instantiate(Enemy, new Vector2(x, y), Quaternion.identity);
            SpawnEnemy = false;
        }
        else
        {
            SpawnEnemy = false;
        }
    }

}
