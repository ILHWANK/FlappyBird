using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePipe : MonoBehaviour
{

    public GameObject Pipe;

    public float TimeDiff;
    public float DestoryTime;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > TimeDiff)
        {
            GameObject newPipe = Instantiate(Pipe);
            newPipe.transform.position = new Vector3(7, Random.Range(-2.0f, 4.5f), 0);
            timer = 0;
            Destroy(newPipe, DestoryTime);
        }
    }
}
