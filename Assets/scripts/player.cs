using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    public GameObject slider;
    public GameObject stars;
    //private float timer = 0.0f;
    private float seconds;
    private bool finish=false;

    [SerializeField] private LevelGenerator levelGenerator;

    void Update()
    {
        /*if(finish==true)
        {
            timer += Time.deltaTime;
            seconds = timer % 60;
        }

        if(seconds>1)
        {
            int scenecount= int.Parse(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene( (scenecount+1).ToString() );
        } */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="finish")
        {
            TinySauce.OnGameFinished(PlayerPrefs.GetInt("Level").ToString(),true,0);
            finish=true;
            move.finish_bool=true;
            GameObject.Find("Computer").GetComponent<move>().PlaySound ("sound2", 0.7f);
            stars.SetActive(true);
            //PlayerPrefs.SetInt("scene", int.Parse(SceneManager.GetActiveScene().name));

            int currentLevel = PlayerPrefs.GetInt("Level");
            currentLevel++;
            PlayerPrefs.SetInt("Level", currentLevel);
            Invoke("LoadNewLevel", 2f);
        }
        else
        {
        move.score+=1;
        slider.GetComponent<Slider>().value = move.score;
        GameObject.Find("Score_New").GetComponent<Text>().text = move.score.ToString();
        GameObject.Find("Computer").GetComponent<move>().PlaySound ("sound1", 0.7f);

        int random = Random.Range(0, 2);
        if(random==1){other.transform.parent.gameObject.GetComponent<Animator>().Play("cube_break");}
        else{other.transform.parent.gameObject.GetComponent<Animator>().Play("cube_break2");}
        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="enemy" || collision.gameObject.tag=="sea")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            TinySauce.OnGameFinished(PlayerPrefs.GetInt("Level").ToString(),false,0);
            levelGenerator.LoadSameLevel();
            GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>().value=0;
        }
        
    }

    private void LoadNewLevel()
    {
        SceneManager.LoadScene("Dynamic Levels");
    }
}
