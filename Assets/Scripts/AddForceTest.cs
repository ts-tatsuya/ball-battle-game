using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    private Vector3 targetPos;
    void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // gameObject.GetComponent<Rigidbody>().AddForce(
        //     Vector3.MoveTowards(gameObject.transform.position, target.transform.position, 1.5f * Time.deltaTime)
        // );
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 1.5f * Time.deltaTime);
    }
}
