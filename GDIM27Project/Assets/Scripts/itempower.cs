/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itempower : MonoBehaviour
{
    double itempowerlvl = 1.0;

    private void OnTriggerStay(Collider other)
    {
        if (oter.gameObject.name == "Tornado")
        {
            // Calculate the direction and distance between the tornado and the other collider
            Vector3 direction = transform.position - other.transform.position;
            float distance = direction.magnitude;

            // Check if the other collider is within the suction range of the tornado
            if (distance <= other.gameObject.radius)
            {
                // Calculate the strength of the suction force based on the distance between the tornado and the other collider
                float magnitude = force * (1 - distance / radius);

                // Apply the suction force to the other collider in the direction of the tornado
                other.transform.position += direction.normalized * magnitude * Time.deltaTime;

                // If the other collider is close enough to the tornado, destroy it
                if (distance <= 0.5f)
                {
                    Destroy(other.gameObject);
                    levelUp();
                }
            }
        }
        // Increase the level of the tornado if it destroys objects
    }

    double itemCpower()
    {
        return itempowerlvl;
    }
}
*/