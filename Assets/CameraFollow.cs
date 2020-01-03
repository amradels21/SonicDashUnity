using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;


    public GameObject CameraMain;
    public GameObject CameraTwo;

    public GameObject CameraMainTmp;
    public GameObject CameraTwoTmp;



    bool falshe;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;


        



    }

    void FixedUpdate()
    {


        if(Input.GetKey("c"))
        {

            SwitchCamera();
            falshe = false;

        }


    }


    public void SwitchCamera()
    {
        CameraMain.SetActive(falshe);
        CameraTwo.SetActive(falshe);

        CameraMainTmp.SetActive(!falshe);
        CameraTwoTmp.SetActive(!falshe);
        falshe = true;
    }
}
