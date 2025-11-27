using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {
    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 130;
    
    // References
    public GameObject BodyPrefab;

    // Lists
    private List<GameObject> BodyParts = new();
    private List<Vector3> PositionsHistory = new();

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(ExampleCoroutine());
        GrowSnake();        
        GrowSnake();
        GrowSnake();      
    }

    // Update is called once per frame
    void Update() {        

        // Move forward
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Steer
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(steerDirection * SteerSpeed * Time.deltaTime * Vector3.up);

        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += BodySpeed * Time.deltaTime * moveDirection;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }
    }

    //Collision on points to add to tail and if it hit wall/tail dies and goes to gameover
    public void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Point")
        {
            GrowSnake();
            Score.instance.AddPoint();
        }

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Tail")
        {
            Time.timeScale = 0;
            Score.instance.GameOver();
        }

    }

    //Create/add tail to snake
    public void GrowSnake() {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    //ExampleCoroutine created to avoid the snake of dying in the first second of the game, collider is disabled for 1 sec and is activated after 1 sec
    IEnumerator ExampleCoroutine()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;

        //yield on a new YieldInstruction that waits for 1 seconds.
        yield return new WaitForSeconds(1);

        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
}