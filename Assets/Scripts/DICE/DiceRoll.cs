using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{

    public Rigidbody rb;
    public List<GameObject> heightDetector;



    // Start is called before the first frame update
    void Start()
    {
        ThrowDice();
    }

    private void Update()
    {
        if(HasItStooped() == true)
        {
            int indexResult = FindFaceResult();
        }
    }

    private int FindFaceResult()
    {
        int maxIndex = 0;
        for (int i = 0; i < heightDetector.Count; i++)
        {
            if(heightDetector[maxIndex].transform.position.y < heightDetector[i].transform.position.y)
            {
                maxIndex = i;
            }
        }
        Debug.Log(maxIndex + 1);
        return maxIndex;
    }

    public bool HasItStooped()
    {
        if (rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero)
        {
            Debug.Log("Nuh-uh");
            return true;
        }
        else return false;
    }


    void ThrowDice()
    {
        int x = Random.Range(0, 360);
        int y = Random.Range(0, 360);
        int z = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(x, y, z);

        x = Random.Range(0, 5);
        y = Random.Range(0, 5);
        z = Random.Range(0, 5);
        Vector3 force = new Vector3(x, -y, z);

        x = Random.Range(0, 5);
        y = Random.Range(0, 5);
        z = Random.Range(0, 5);
        Vector3 torque = new Vector3(x, y, z);

        transform.rotation = rotation;
        rb.velocity = force;

        this.rb.maxAngularVelocity = 1000;
        rb.AddTorque(torque, ForceMode.VelocityChange);

    }



}
