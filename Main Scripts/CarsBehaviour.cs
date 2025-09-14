using UnityEngine;

public class CarsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject fastCar;
    [SerializeField] GameObject greenCar;
    [SerializeField] GameObject ambulance;
    [SerializeField] GameObject redCar;
    [SerializeField] GameObject taxi;

    // Update is called once per frame
    void Update()
    {
        if (fastCar.transform.position.z > 100)
        {
            fastCar.transform.position -= new Vector3(0, 0, 100);
        }
        fastCar.transform.position += new Vector3(0, 0, 5 * Time.deltaTime);

        if (taxi.transform.position.z > 100)
        {
            taxi.transform.position -= new Vector3(0, 0, 100);
        }
        taxi.transform.position += new Vector3(0, 0, 4 * Time.deltaTime);

        if (greenCar.transform.position.z > 100)
        {
            greenCar.transform.position -= new Vector3(0, 0, 100);
        }
        greenCar.transform.position += new Vector3(0, 0, 4 * Time.deltaTime);

        if (redCar.transform.position.z < 0)
        {
            redCar.transform.position += new Vector3(0, 0, 100);
        }
        redCar.transform.position -= new Vector3(0, 0, 3 * Time.deltaTime);

        if (ambulance.transform.position.z < 0)
        {
            ambulance.transform.position += new Vector3(0, 0, 100);
        }
        ambulance.transform.position -= new Vector3(0, 0, 3.5f * Time.deltaTime);
    }
}
