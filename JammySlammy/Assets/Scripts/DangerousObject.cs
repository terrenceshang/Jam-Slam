using UnityEngine;

public class DangerousObject : MonoBehaviour
{

    public GameObject[] vulnerableObjects; // Array of objects that are vulnerable. Assign these in the inspector.
    public float deathRiseAmount = 0.5f; // The amount the object will rise before falling
    public float deathRiseDuration = 0.5f; // Duration of the rise
    public float deathFallDuration = 2f; // Duration of the fall
    public SugarCubeManager cubeManager;

    public SuperCubeManager superCubeManager;

    private void OnTriggerEnter2D(Collider2D other)
    {

        foreach (GameObject vulnerableObj in vulnerableObjects)
        {

            if (other.gameObject == vulnerableObj)
            {


                Die(other.gameObject);
                return; // Exit the loop once a match is found
            }
        }

    }



    private System.Collections.IEnumerator DeathAnimation(GameObject obj)
    {


        // Rise up
        float startTime = Time.time;
        Vector3 initialPos = obj.transform.position;
        Vector3 targetPos = initialPos + new Vector3(0, deathRiseAmount, 0);
        while (Time.time - startTime < deathRiseDuration)
        {
            float t = (Time.time - startTime) / deathRiseDuration;
            obj.transform.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        // Fall down
        startTime = Time.time;
        initialPos = obj.transform.position;
        targetPos = initialPos + new Vector3(0, -10, 0); // Arbitrary large value to ensure it goes off-screen
        while (Time.time - startTime < deathFallDuration)
        {
            float t = (Time.time - startTime) / deathFallDuration;
            obj.transform.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }
        cubeManager.RespawnCubes();
        superCubeManager.RespawnCubes();
        Debug.Log("Nå skal respawn være sendt");
        foreach (Pushable pushable in FindObjectsOfType<Pushable>())
        {
            pushable.ResetToOriginalPosition();
        }
        RespawnAllVulnerableObjects();

    }


    private void Die(GameObject obj)
    {


        obj.GetComponent<Collider2D>().enabled = false; // Disable collider to prevent further interactions
        obj.GetComponent<Rigidbody2D>().isKinematic = true; // Set Rigidbody2D to kinematic to control movement
        StartCoroutine(DeathAnimation(obj));
    }

    private void RespawnIndividualObject(GameObject obj)
    {

        Vector3 respawnPoint = Vector3.zero;

        MovementS strawberry = obj.GetComponent<MovementS>();
        MovementBB blueberry = obj.GetComponent<MovementBB>();







        if (strawberry != null)
        {
            strawberry.setSpecialCubeFalse();
            strawberry.ResetPlayerState();
        }

        if (blueberry != null)
        {
            blueberry.setSpecialCubeFalse();
            blueberry.ResetPlayerState();
        }

        // Determine which object has died and get its respawn point
        if (strawberry != null)
        {
            respawnPoint = strawberry.respawnPoint;
        }
        else if (blueberry != null)
        {
            respawnPoint = blueberry.respawnPoint;
        }



        if (respawnPoint != Vector3.zero) // Check that we have a valid respawn point
        {
            GameObject strawberryObj = FindObjectOfType<MovementS>().gameObject;
            GameObject blueberryObj = FindObjectOfType<MovementBB>().gameObject;

            // Respawn the strawberry
            if (strawberryObj)
            {
                strawberryObj.transform.position = respawnPoint;
                strawberryObj.GetComponent<Collider2D>().enabled = true;
                strawberryObj.GetComponent<Rigidbody2D>().isKinematic = false;
                CameraMovement camScript = FindObjectOfType<CameraMovement>();
                if (camScript)
                {
                    camScript.RegisterPlayer1(strawberryObj.transform);
                }
            }

            // Respawn the blueberry
            if (blueberryObj)
            {
                blueberryObj.transform.position = respawnPoint;
                blueberryObj.GetComponent<Collider2D>().enabled = true;
                blueberryObj.GetComponent<Rigidbody2D>().isKinematic = false;
                CameraMovement camScript = FindObjectOfType<CameraMovement>();
                if (camScript)
                {
                    camScript.RegisterPlayer2(blueberryObj.transform);
                }
            }
        }
        else
        {
            Debug.LogWarning(obj.name + " doesn't have a valid respawn point.");
        }
    }


    private void RespawnAllVulnerableObjects()
    {
        foreach (GameObject vulnerableObj in vulnerableObjects)
        {
            RespawnIndividualObject(vulnerableObj);
        }
    }

}
