using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour
{
    public GameObject cube1;
    public GameObject player;
    public GameObject floor;
    public GameObject terrains;
    public Joystick joystick;
    static public int score;
    private bool start;
    static public bool finish_bool;

    public List<AudioClip> Sounds;
    private AudioSource SoundSource;

    void Start()
    {
        finish_bool=false;
        start=false;
        score=0;
        
        SoundSource = this.gameObject.GetComponent<AudioSource> ();
        GameObject.Find("Score").GetComponent<Text>().text= PlayerPrefs.GetInt("Level").ToString();
    }

    void FixedUpdate()
    {
        Camera.main.transform.position= player.transform.position + new Vector3(0,15,-7);
        
        if(player.transform.position.x<=15.5f && player.transform.position.x>=-15.5f && start==true &&finish_bool==false)
        {
        Vector3 player_v = new Vector3(joystick.Horizontal*1000f,player.GetComponent<Rigidbody>().velocity.y,2000f) *Time.deltaTime;
        player.GetComponent<Rigidbody>().velocity= player_v;
        }

        if (Input.GetMouseButtonDown(1))
    {
        ScreenCapture.CaptureScreenshot("SomeLevel.png");
    }
    /*}
    
    void Update()
    {*/

#if UNITY_EDITOR 
        //Unity Editor Inputs
        if (Input.GetMouseButton(0))
        {
            GetButtonMovement();
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetButtonUp();
        }
#endif

        //Mobile Inputs
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                GetButtonMovement();
            }
            /*if(touch.phase == TouchPhase.Ended)
            {
                GetButtonUp();
            }*/
        }
        if(Input.touchCount == 0)
        {
            GetButtonUp();
        }
    }

    private void GetButtonMovement()
    {
        if(start==false)
        {
            start=true;
            Destroy(GameObject.Find("Tutorial"));
        }
        Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, joystick.Horizontal*20f);
        terrains.transform.rotation = Quaternion.Euler(-5,0,Camera.main.transform.eulerAngles.z);
    }

    private void GetButtonUp()
    {
        Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Mathf.LerpAngle( Camera.main.transform.eulerAngles.z, 0f, 5f*Time.deltaTime ));
        terrains.transform.rotation = Quaternion.Euler(-5,0,Camera.main.transform.eulerAngles.z);
    }

    public void PlaySound(string girdi,float girdi2)
{
	foreach (AudioClip Clip in Sounds) 
	{
		if (Clip.name == girdi) 
		{
			SoundSource.PlayOneShot (Clip, girdi2);
		}
	}
}
}
