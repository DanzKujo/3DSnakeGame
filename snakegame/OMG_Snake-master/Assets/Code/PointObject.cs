using UnityEngine;

public class PointObject : MonoBehaviour
{
    //Point when hit snake destroys and creates another randomly on the map
    public void OnTriggerEnter(Collider other)
    {
        var position = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
        if (other.gameObject.tag == "Player")
        {     
            Destroy(gameObject);           
            Instantiate(gameObject, position, Quaternion.identity);
        }
    }
}
