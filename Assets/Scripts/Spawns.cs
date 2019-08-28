using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour
{
    public int size;
    public List<GameObject> list;
    public GameObject originalBee;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size; i++) {
            GameObject bee = Instantiate(originalBee, new Vector3(Random.value*10,Random.value*10,Random.value*10), Quaternion.identity);
            list.Add(bee);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
