
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    bool onGround;


    public Text txt;

    public float forwardForce = 200f;
    public float sidewaysForce = 500f;
    public float jump = 10f;

    float timer = 60.0f;
    int seconds;
    public Text TimeLeft;

    public Text Boost;
    int Boostmeter;

    //Time Invince
    float timeInvince = 5f;



    //Panels
    public GameObject PanelPause;
    public GameObject GameOverPanel;
    bool paused;


    bool IncreaseTime;
    bool DecreaseTime;

    //Prefabs
    public GameObject testPrefab;
    public GameObject BlueSpherePrefab;
    public GameObject IronBallPrefab;
    public GameObject BombPrefab;
    public GameObject GroundPrefab;



    //Sound
    public GameObject RunningSound;
    public GameObject PauseSound;




    GameObject obj;
    float[] ItemPlace = {-4.5f, 0.25f, 4.5f };

    bool InvincibleModeActivated;






    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        InvokeRepeating("Spawn",2f,2.9f);
        InvokeRepeating("SpawnBlueSphere", 2f, 3.1f);
        InvokeRepeating("SpawnIronBall", 2f, 3.5f);
        InvokeRepeating("SpawnBomb", 2f, 3.3f);

        InvokeRepeating("SpawnGround", 5f, 1f);




    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);

            if (Input.GetKeyDown("right"))
            {
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.Impulse);


            }


            if (Input.GetKeyDown("left"))
            {
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.Impulse);

            }


         if (Input.GetKeyDown("space")  )
            {
                 Jump();

            }

        //Android Movement
        float LeftRight = 0;

        if (Input.touchCount > 0)
        {
            // touch x position is bigger than half of the screen, moving right
            if (Input.GetTouch(0).position.x > Screen.width / 2)
                LeftRight = 1;
            // touch x position is smaller than half of the screen, moving left
            else if (Input.GetTouch(0).position.x < Screen.width / 2)
                LeftRight = -1;
        }

        Vector3 Movement = new Vector3(LeftRight, 0, 0);

        //rb.AddForce(Movement * 10f);


        transform.Translate(Movement * 10f * Time.deltaTime);



    }

    private void Update()
    {


        timer -= Time.deltaTime;

        seconds = (int)timer % 120;

        TimeLeft.text = "Time Left:" + seconds;
        txt.text = "Score: " + (int)rb.position.z;
        Boost.text = "Boost: " + Boostmeter;


        //Time InvinsibleMode
        timeInvince -= 1 * Time.deltaTime;


        InvincibleMode();

        if (IncreaseTime == true)
        {
            timer += 2.0f;
            IncreaseTime = false;

        }
        if (DecreaseTime == true)
        {
            timer -= 10.0f;
            DecreaseTime = false;
        
        }

        //Time Out
        if (seconds <= 0)
        {
            Destroy(gameObject);
            RunningSound.SetActive(false);
            GameOverPanel.SetActive(true);

        }

        

        //Paused Game
        if (Input.GetKeyDown("escape"))
        {
            PauseG();
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }



    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log("Collision Happened");
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

            //Add 2 seconds in Time
            IncreaseTime = true;
            SoundManager.PlaySound("coin");



            //if (CounterCoin == 5)
            //{
            //    Destroy(GameObject.Find("Obstacle (1)"));
            //    Destroy(GameObject.Find("Obstacle (2)"));
            //    Destroy(GameObject.Find("Obstacle"));


            //}

        }

        //Blue Spheres
        if (collision.gameObject.CompareTag("BlueSphere"))
        {

                Destroy(collision.gameObject);

            if (InvincibleModeActivated == false)
            {
                //Increase player Boost Meter by 10points
                forwardForce += 10;
                Boostmeter += 10;
            }

            SoundManager.PlaySound("coin");




        }


        //Bomb
        if (collision.gameObject.CompareTag("Bomb"))
        {


            SoundManager.PlaySound("bomb");


            if (InvincibleModeActivated == false)
            {
                //Destroy Player
                Destroy(gameObject);

                RunningSound.SetActive(false);

                GameOverPanel.SetActive(true);
            }
            else
            {
                //Destroy Player
                Destroy(collision.gameObject);


            }





        }

        //Iron Ball
        if (collision.gameObject.CompareTag("IronBall"))
        {
            //Decrease Time Limit by 10seconds
            SoundManager.PlaySound("bomb");

            if (InvincibleModeActivated == false)
            {
                Destroy(collision.gameObject);

                DecreaseTime = true;

            }
            else
            {
                Destroy(collision.gameObject);

            }

        }


    }


    //Coin Prefab
    void Spawn()
    {
        float x = ItemPlace[Random.Range(0, ItemPlace.Length)];
        float z = GameObject.Find("Player").transform.position.z + 50;

         obj = Instantiate(testPrefab);
         obj.transform.position = new Vector3(x, 2f, z);

        //Delete Object After 5 seconds from being created
         Destroy(obj, 6);
    }

    void SpawnBlueSphere()
    {
        float x = ItemPlace[Random.Range(0, ItemPlace.Length)];
        float z = GameObject.Find("Player").transform.position.z + 40;

        obj = Instantiate(BlueSpherePrefab);
        obj.transform.position = new Vector3(x, 2f, z);

        Destroy(obj, 7);

    }

    void SpawnBomb()
    {
        float x = ItemPlace[Random.Range(0, ItemPlace.Length)];
        float z = GameObject.Find("Player").transform.position.z + 20;

         obj = Instantiate(BombPrefab);
         obj.transform.position = new Vector3(x, 2f, z);
        Destroy(obj, 5);

    }

    void SpawnIronBall()
    {
        float x = ItemPlace[Random.Range(0, ItemPlace.Length)];
        float z = GameObject.Find("Player").transform.position.z + 60;

        obj = Instantiate(IronBallPrefab);
        obj.transform.position = new Vector3(x, 2f, z);
        Destroy(obj, 8);

    }

    void SpawnGround()
    {
        float z = GameObject.Find("Player").transform.position.z;
        obj = Instantiate(GroundPrefab);
        obj.transform.position = new Vector3(0f, 0f, z+ 200f);
        Destroy(obj, 20);
        
    }


    //Game Settings
    public void onResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        PanelPause.SetActive(false);

        RunningSound.SetActive(true);
        PauseSound.SetActive(false);


    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); //Load scene called Game
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }



    //InvincibleMode
    public void InvincibleMode()
    {
        if (Boostmeter == 50 && InvincibleModeActivated == false)
        {
            InvincibleModeActivated = true;
            forwardForce = forwardForce * 2;
            timeInvince = 5f;


        }
        if (InvincibleModeActivated && timeInvince <= 0)
        {

            InvincibleModeActivated = false;
            forwardForce = 200f;
            Boostmeter = 0;
        }

    }



    //Movements
    public void Jump()
    {
        // rb.AddForce(0, jump * Time.deltaTime, 0, ForceMode.VelocityChange);
        if (onGround)
        {
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
            onGround = false;
        }
        // rb.transform.Translate(0,  jump * Time.deltaTime, 0);
    }

    public void PauseG()
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            paused = true;

            PanelPause.SetActive(true);
            RunningSound.SetActive(false);
            PauseSound.SetActive(true);

        }
        else
        {
            onResumeGame();

        }
    }





}
