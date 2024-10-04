using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemy : MonoBehaviour
{
    public List<GameObject> wayPointsList = new List<GameObject>();
    public float speed;
    int wayPointCounter = 0;
    int wayPointNumber;
    Vector3 displacementVector;
    GameObject wayPointGoal;

    // Start is called before the first frame update
    void Start()
    {
        wayPointNumber = wayPointsList.Count;
        wayPointGoal = wayPointsList[wayPointCounter % wayPointNumber];
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        displacementVector = (wayPointGoal.transform.position - transform.position).normalized;
        gameObject.transform.Translate(displacementVector * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, wayPointGoal.transform.position) < 0.05f)
        {
            wayPointCounter++;
            wayPointGoal = wayPointsList[wayPointCounter % wayPointNumber];
            if (wayPointGoal.transform.position.x > transform.position.x) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
    }
}
