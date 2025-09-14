using UnityEngine;
//using UnityEngine.AI;

public class MapAndVarInitialization : MonoBehaviour
{
    [SerializeField] Terrain terrain;
    [SerializeField] GameObject coins;
    [SerializeField] GameObject potions;
    //[SerializeField] NavMeshSurface NavMeshSurface;

    private int goldSize1;
    private int goldSize2;
    private int potionsSize1;
    private int potionsSize2;

    private int randPositionX;
    private int randPositionZ;

    private void Awake()
    {
        //NavMeshSurface.BuildNavMesh();
    }

    // Start is called before the first frame update
    void Start()
    {
        terrain.terrainData.size = new Vector3(PlayButtonBehaviour.mapWidthSize, 0, PlayButtonBehaviour.mapHeightSize);

        if (PlayButtonBehaviour.goldSize % 2 == 1) // this means that goldSize is odd number
        {
            goldSize1 = (int)Mathf.Round(PlayButtonBehaviour.goldSize / 2) + 1;
            goldSize2 = (int)Mathf.Round(PlayButtonBehaviour.goldSize / 2);
        }
        else
        {
            goldSize1 = PlayButtonBehaviour.goldSize / 2;
            goldSize2 = PlayButtonBehaviour.goldSize / 2;
        }

        if (PlayButtonBehaviour.potionsSize % 2 == 1) // this means that potionsSize is odd number
        {
            potionsSize1 = (int)Mathf.Round(PlayButtonBehaviour.potionsSize / 2) + 1;
            potionsSize2 = (int)Mathf.Round(PlayButtonBehaviour.potionsSize / 2);
        }
        else
        {
            potionsSize1 = PlayButtonBehaviour.potionsSize / 2;
            potionsSize2 = PlayButtonBehaviour.potionsSize / 2;
        }

        for (int i = 0; i < goldSize1; i++)
        {
            Instantiate(coins, randPositionRight(randPositionX, randPositionZ), Quaternion.identity); // x between [61-99], y = 0, z between [1-99] || gold right on the map
        }

        for (int i = 0; i < goldSize2; i++)
        {
            Instantiate(coins, randPositionLeft(randPositionX, randPositionZ), Quaternion.identity); // gold left on the map
        }

        for (int i = 0; i < potionsSize1; i++)
        {
            Instantiate(potions, randPositionRight(randPositionX, randPositionZ), Quaternion.identity); // x between [61-99], y = 0, z between [1-99] || potion right on the map
        }

        for (int i = 0; i < potionsSize2; i++)
        {
            Instantiate(potions, randPositionLeft(randPositionX, randPositionZ), Quaternion.identity); //potion left on the map
        }
    }
    bool isPositionInBuildings(int posX, int posZ)
    {
        if (posX >= 20 && posX <= 40 && posZ >= 0 && posZ <= 20)
        {
            return true;
        }
        else if (posX >= 15 && posX <= 20 && posZ >= 20 && posZ <= 30)
        {
            return true;
        }
        else if (posX >= 15 && posX <= 20 && posZ >= 65 && posZ <= 70)
        {
            return true;
        }
        else if (posX >= 0 && posX <= 5 && posZ >= 70 && posZ <= 75)
        {
            return true;
        }
        else if (posX >= 25 && posX <= 40 && posZ >= 85 && posZ <= 100)
        {
            return true;
        }
        else if (posX >= 60 && posX <= 75 && posZ >= 85 && posZ <= 100)
        {
            return true;
        }
        else if (posX >= 95 && posX <= 100 && posZ >= 95 && posZ <= 100)
        {
            return true;
        }
        else if (posX >= 80 && posX <= 85 && posZ >= 65 && posZ <= 80)
        {
            return true;
        }
        else if (posX >= 60 && posX <= 80 && posZ >= 0 && posZ <= 20)
        {
            return true;
        }
        else return false;
    }

    Vector3 randPositionRight(int randPositionX, int randPositionZ ) //returns position of coins or potions that are not inside buildings
    {
        randPositionX = Random.Range(61, PlayButtonBehaviour.mapWidthSize);
        randPositionZ = Random.Range(1, PlayButtonBehaviour.mapHeightSize);
        while (isPositionInBuildings(randPositionX, randPositionZ))
        {
            randPositionX = Random.Range(61, PlayButtonBehaviour.mapWidthSize);
            randPositionZ = Random.Range(1, PlayButtonBehaviour.mapHeightSize);
        }

        return new Vector3(randPositionX, 0, randPositionZ);
    }

    Vector3 randPositionLeft(int randPositionX, int randPositionZ)
    {
        randPositionX = Random.Range(1, 39);
        randPositionZ = Random.Range(1, PlayButtonBehaviour.mapHeightSize);
        while (isPositionInBuildings(randPositionX, randPositionZ))
        {
            randPositionX = Random.Range(1, 39);
            randPositionZ = Random.Range(1, PlayButtonBehaviour.mapHeightSize);
        }

        return new Vector3(randPositionX, 0, randPositionZ);
    }
}
