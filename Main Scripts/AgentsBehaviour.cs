using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AgentsBehaviour : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator agentAnimator;
    [SerializeField] float tick = 2.5f;

    [SerializeField] TMP_Text agentWentToTheBankText;
    [SerializeField] TMP_Text agentWentToTheShopsText;
    [SerializeField] TMP_Text agentWentToTheTownHallText;
    [SerializeField] TMP_Text agentWentToTheHouseText;
    [SerializeField] TMP_Text agentKnowsBankPositionText;
    [SerializeField] TMP_Text agentKnowsShopsPositionText;
    [SerializeField] TMP_Text agentKnowsTownHallPositionText;
    [SerializeField] TMP_Text coinsText;
    [SerializeField] TMP_Text EnergyPotionsText;
    [SerializeField] TMP_Text agentsEnergyText;
    [SerializeField] TMP_Text agentsTask;

    //[SerializeField] TMP_Text test;

    private int agentsEnergy = 100;
    private int energyPotions;
    private int coins;
    private float timeSinceLastTick;
    private float timeAgentStandsStill;
    private Vector3 coinsPosition;
    private Vector3 energyPosition;
    private readonly Vector3 bankPosition = new (70, 0, 22);
    private readonly Vector3 shopsPosition = new(78.5f, 1, 72.5f);
    private readonly Vector3 townHallPosition = new(72.5f, 1, 84);
    private readonly Vector3 parkPosition = new(94, 1, 50);
    private readonly Vector3 homePosition = new(97.5f, 0, 93);
    private bool agentKnowsEnergyPosition;
    private bool agentKnowsCoinsPosition;
    private bool agentKnowsBankPosition;
    private bool agentKnowsShopsPosition;
    private bool agentKnowsTownHallPosition;
    private bool agentWentToTheBank;
    private bool agentWentToTheShops;
    private bool agentWentToTheTownHall;
    private bool agentWentToTheHouse;
    private bool agentWentToThePark;
    private bool agentGoesDownwardsTheMap = true;



    // Start is called before the first frame update
    void Start()
    {
        agent.destination = new Vector3(90, 0, 0);
        timeSinceLastTick = -2.5f; // -5 is for the first tick in the start of the app
    }

    // Update is called once per frame
    void Update()
    {
        //test.text = agentGoesDownwardsTheMap.ToString();
        //Debug.Log(timeAgentStandsStill);

        if (agent.velocity.magnitude < 0.15f)                            //
        {                                                                //
            agentAnimator.SetBool("hasAgentStoppedWalking", true);
            timeAgentStandsStill += 1f * Time.deltaTime;
        }                                                                //     when agent reaches his destination stops walking
        else                                                             //
        {                                                                //
            agentAnimator.SetBool("hasAgentStoppedWalking", false);
            timeAgentStandsStill = 0;
        }



        if (Time.fixedTime - timeSinceLastTick >= tick)   //
        {                                               //  clock tick. Here agents do one task per tick                                                       
                                                        //

            //---------------------------------------------------------------------------------
            // Debug.Log(timeAgentStandsStill);





               
            agent.isStopped = false;
            //--------------------------------------------------------------------------------
            if (transform.position.z > 95)
            {
                agentGoesDownwardsTheMap = !agentGoesDownwardsTheMap;
                transform.Rotate(0, 180, 0);
                agent.destination = transform.position - new Vector3(0, 0, 10);
                agentsTask.text = "Agent Explores";
            }
            //-----------------------------------------------------------------------------------   edge of map
            else
            {

                if (timeAgentStandsStill >= 2 * tick && !agentWentToTheTownHall)
                {
                    agentGoesDownwardsTheMap = !agentGoesDownwardsTheMap;
                    transform.Rotate(0, 180, 0);
                    if (agentGoesDownwardsTheMap)
                    {
                        agent.destination = transform.position - new Vector3(0, 0, 10);
                    }
                    else
                    {
                        agent.destination = transform.position + new Vector3(0, 0, 10);
                    }
                    agentsTask.text = "Agent Explores";
                }
                //--------------------------------------------------------------------------------      turn 180 if no path
                else
                {


                    //-------------------------------------------------------------------------------------
                    if (agentKnowsEnergyPosition)
                    {
                        agent.destination = energyPosition;
                        if (Vector3.Distance(transform.position, energyPosition) < 1)
                        {
                            //agentEnergy += 20;
                            //Debug.Log(agentEnergy);
                            agentKnowsEnergyPosition = false;
                            agentsTask.text = "Agent collects Energy Potion";
                        }
                        else
                        {
                            agentsTask.text = "Agent Goes to collect Energy Potion";

                        }
                    }
                    //---------------------------------------------------------------------------------     energy
                    else
                    {

                        if (agentsEnergy < 40 && energyPotions > 0)
                        {

                            agent.isStopped = true;
                            energyPotions--;
                            agentsEnergy += 20;
                            EnergyPotionsText.text = "Energy Potions = " + energyPotions;
                            agentsTask.text = "Agent Drinks Energy Potion";

                        }
                        //---------------------------------------------------------------------------------     drink energy potion if energy lower than 50 and if you have it
                        else
                        {

                            if (agentsEnergy < 40 && energyPotions == 0)
                            {
                                agentsTask.text = "Agent Explores";

                                if (!agentWentToThePark)
                                {
                                    agent.destination = parkPosition;
                                    if (Vector3.Distance(transform.position, parkPosition) < 1)
                                    {
                                        agentWentToThePark = true;
                                    }
                                }
                                else
                                {
                                    if (agentGoesDownwardsTheMap)
                                    {
                                        agent.destination = transform.position - new Vector3(0, 0, 10);
                                    }
                                    else
                                    {
                                        agent.destination = transform.position + new Vector3(0, 0, 10);
                                    }
                                }
                            }
                            //-------------------------------------------------------------------------     agent leaves everything and explores to find Energy Pots
                            else
                            {



                                //----------------------------------------------------------------------
                                if (agentKnowsCoinsPosition)
                                {
                                    agent.destination = coinsPosition;
                                    if (Vector3.Distance(transform.position, coinsPosition) < 1)
                                    {
                                        agentKnowsCoinsPosition = false;
                                        agentsTask.text = "Agent Collects Coins";
                                    }
                                    else
                                    {
                                        agentsTask.text = "Agent Goes to collect Coins";

                                    }
                                    //--------------------------------------------------------------------  coins
                                }
                                else
                                {

                                    //----------------------------------------------------------------
                                    if (agentKnowsBankPosition && !agentWentToTheBank)
                                    {
                                        agent.destination = bankPosition;
                                        if (Vector3.Distance(transform.position, bankPosition) < 1)
                                        {
                                            agentWentToTheBank = true;
                                            agentWentToTheBankText.text = "Went to the Bank = True";
                                            agentsTask.text = "Agent interacts with the Bank";
                                        }
                                        else
                                        {
                                            agentsTask.text = "Agent Goes to the Bank";

                                        }
                                        //-----------------------------------------------------------       bank
                                    }
                                    else
                                    {
                                        if (agentKnowsShopsPosition && !agentWentToTheShops && agentWentToTheBank)
                                        {
                                            agent.destination = shopsPosition;
                                            if (Vector3.Distance(transform.position, shopsPosition) < 1)
                                            {
                                                agentWentToTheShops = true;
                                                agentWentToTheShopsText.text = "Went to the Shops = True";
                                                agentGoesDownwardsTheMap = !agentGoesDownwardsTheMap; //its not correct cause its hardcoded
                                                agentsTask.text = "Agent interacts with the Shops";
                                            }
                                            else
                                            {
                                                agentsTask.text = "Agent Goes to the Shops";

                                            }
                                            //------------------------------------------------------------    shops
                                        }
                                        else
                                        {
                                            if (agentKnowsTownHallPosition && !agentWentToTheTownHall && agentWentToTheShops)
                                            {
                                                agent.destination = townHallPosition;
                                                if (Vector3.Distance(transform.position, townHallPosition) < 1)
                                                {
                                                    agentWentToTheTownHall = true;
                                                    agentWentToTheTownHallText.text = "Went to the Town Hall = True";
                                                    agentsTask.text = "Agent interacts with the Town Hall";
                                                }
                                                else
                                                {
                                                    agentsTask.text = "Agent Goes to the Town Hall";

                                                }
                                                //---------------------------------------------------       town hall
                                            }
                                            else
                                            {
                                                if (agentWentToTheTownHall)
                                                {
                                                    agent.destination = homePosition;
                                                    if (Vector3.Distance(transform.position, homePosition) < 1)
                                                    {
                                                        agentWentToTheHouseText.text = "Went to the House = True";
                                                        agentsTask.text = "Agent is at the House";
                                                        agentWentToTheHouse = true;
                                                    }
                                                    else
                                                    {
                                                        agentsTask.text = "Agent Goes to the House";

                                                    }
                                                }
                                                //--------------------------------------------------        home
                                                else
                                                {
                                                    agentsTask.text = "Agent Explores";

                                                    //---------------------------------------------------------------------------
                                                    if (agentGoesDownwardsTheMap)
                                                    {
                                                        agent.destination = transform.position - new Vector3(0, 0, 10);
                                                    }
                                                    else
                                                    {
                                                        agent.destination = transform.position + new Vector3(0, 0, 10);
                                                    }
                                                }
                                            }
                                        }
                                        //-----------------------------------------------------------------------   explore
                                    }
                                }

                            }
                        }
                    }
                }
            }

            if (agentWentToTheHouse)
            {
                
            }
            else
            {
                agentsEnergy--;
            }
            
            agentsEnergyText.text = "Agents Energy = " + agentsEnergy;
            //Debug.Log(agentEnergy);
            timeSinceLastTick = Time.fixedTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coins"))
        {
            coinsPosition = other.transform.position;
            agentKnowsCoinsPosition = true;
        }
        if (other.CompareTag("Bank"))
        {
            agentKnowsBankPosition = true;
            agentKnowsBankPositionText.text = "Agent knows Bank position = True";
        }
        if (other.CompareTag("Shop"))
        {
            agentKnowsShopsPosition = true;
            agentKnowsShopsPositionText.text = "Agent knows Shops position = True";
        }
        if (other.CompareTag("Town Hall"))
        {
            agentKnowsTownHallPosition = true;
            agentKnowsTownHallPositionText.text = "Agent knows Town Hall position = True";
        }
        if (other.CompareTag("Energy Pot"))
        {
            energyPosition = other.transform.position;
            agentKnowsEnergyPosition = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Coins"))
        {
            if (Vector3.Distance(transform.position, coinsPosition) < 1)
            {
                Destroy(other.gameObject);
                coins++;
                coinsText.text = "Coins = " + coins;
            }
        }

        if (other.CompareTag("Energy Pot"))
        {
            if (Vector3.Distance(transform.position, energyPosition) < 1)
            {
                Destroy(other.gameObject);
                energyPotions++;
                EnergyPotionsText.text = "Energy Potions = " + energyPotions;
            }
        }
    }
}
