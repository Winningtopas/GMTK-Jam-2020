using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float currentSpeed;
    public float speed;
    public float maxDistance;

    private Vector3 startPosition;
    private Vector3 maxDistancePosition;

    public string direction;
    public bool canMove = true;

    // Start is called before the first frame update
    private void Start()
    {
        currentSpeed = speed;
        startPosition = this.transform.position;
        if (direction == "leftX")
            maxDistancePosition = new Vector3(startPosition.x + maxDistance, startPosition.y, startPosition.z + maxDistance);
        if (direction == "leftY")
            maxDistancePosition = new Vector3(startPosition.x, startPosition.y + maxDistance, startPosition.z);
        if (direction == "leftZ")
            maxDistancePosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + maxDistance);

        if (direction == "rightX")
            maxDistancePosition = new Vector3(startPosition.x - maxDistance, startPosition.y, startPosition.z + maxDistance);
        if (direction == "rightY")
            maxDistancePosition = new Vector3(startPosition.x, startPosition.y - maxDistance, startPosition.z);
        if (direction == "rightZ")
            maxDistancePosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - maxDistance);
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove == true)
            Move();
    }

    private void Move()
    {
        if (direction == "leftX")
        {
            transform.Translate(Time.deltaTime * currentSpeed, 0f, 0f);

            if (this.transform.position.x <= startPosition.x)
                currentSpeed = speed;
            if (this.transform.position.x > maxDistancePosition.x)
                currentSpeed = -speed;
        }

        if (direction == "rightX")
        {
            transform.Translate(Time.deltaTime * currentSpeed, 0f, 0f);

            if (this.transform.position.x >= startPosition.x)
                currentSpeed = -speed;
            if (this.transform.position.x < maxDistancePosition.x)
                currentSpeed = speed;
        }

        if (direction == "leftY")
        {
            transform.Translate(0f, Time.deltaTime * currentSpeed, 0f);

            if (this.transform.position.y <= startPosition.y)
                currentSpeed = speed;
            if (this.transform.position.y > maxDistancePosition.y)
                currentSpeed = -speed;
        }

        if (direction == "rightY")
        {
            transform.Translate(0f, Time.deltaTime * currentSpeed, 0f);

            if (this.transform.position.y >= startPosition.y)
                currentSpeed = -speed;
            if (this.transform.position.y < maxDistancePosition.y)
                currentSpeed = speed;
        }

        if (direction == "leftZ")
        {
            transform.Translate(0f, 0f, Time.deltaTime * currentSpeed);

            if (this.transform.position.z <= startPosition.z)
                currentSpeed = -currentSpeed;
            if (this.transform.position.z > maxDistancePosition.z)
                currentSpeed = -currentSpeed;
        }

        if (direction == "rightZ")
        {
            transform.Translate(0f, 0f, Time.deltaTime * currentSpeed);

            if (this.transform.position.z >= startPosition.z)
                currentSpeed = -speed;
            if (this.transform.position.z < maxDistancePosition.z)
                currentSpeed = speed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (direction == "leftX")
            Gizmos.DrawCube(new Vector3(this.transform.position.x + maxDistance, this.transform.position.y, this.transform.position.z), new Vector3(8, 1, 8));
        if (direction == "leftY")
            Gizmos.DrawCube(new Vector3(this.transform.position.x, this.transform.position.y + maxDistance, this.transform.position.z - 4), new Vector3(8, 1, 8));
        if (direction == "leftZ")
            Gizmos.DrawCube(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + maxDistance), new Vector3(8, 1, 8));
        if (direction == "rightX")
            Gizmos.DrawCube(new Vector3(this.transform.position.x - maxDistance, this.transform.position.y, this.transform.position.z), new Vector3(8, 1, 8));
        if (direction == "rightY")
            Gizmos.DrawCube(new Vector3(this.transform.position.x, this.transform.position.y - maxDistance, this.transform.position.z - 4), new Vector3(8, 1, 8));
        if (direction == "rightZ")
            Gizmos.DrawCube(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - maxDistance), new Vector3(8, 1, 8));
    }
}