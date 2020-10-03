using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject CubePrefab;
    [SerializeField] private GameObject FirstCube;
    public Transform playerBall;
    private Vector3 playerInitialPos;
    private float lastPosX;

    private float[] CubePositionsX;
    private GameObject[] CubesOnScene;
    private Transform firstCubeTransform;

    
    public int max_score;

    private int Level;
    private void Start()
    {
        Level = PlayerPrefs.GetInt("Level");
        if(Level == 0)
        {
            Level = 1;
            PlayerPrefs.SetInt("Level", Level);
        }

        lastPosX = FirstCube.transform.position.x;

        CubePositionsX = new float[19];
        CubesOnScene = new GameObject[20];
        //firstCubeTransform = FirstCube.transform;
        firstCubeTransform = gameObject.transform;
        firstCubeTransform.position = new Vector3(-4f, -8.553177f, 0f);
        firstCubeTransform.rotation = new Quaternion(-90f, 0, 0, 0);


        //playerInitialTransform.position = playerBall.position;
        Vector3 pos = new Vector3(0f, -8.51f, -43f);
        playerInitialPos = pos;

        GenerateLevel();
    }

    private void GenerateLevel()
    {
        TinySauce.OnGameStarted(PlayerPrefs.GetInt("Level").ToString());
        CubesOnScene[0] = FirstCube;

        for(int i = 1; i <= 19; i++)
        {
            Vector3 tempPos = SetNewCubePos(i);
            CubesOnScene[i] = Instantiate(CubePrefab, tempPos, FirstCube.transform.rotation);

            CubePositionsX[i - 1] = tempPos.x;
        }
    }

    private Vector3 SetNewCubePos(int i)
    {
        //Possible Ranges for the path, x: -11.5 to 3.5 && z: +25
        Vector3 tempPos = FirstCube.transform.position;

        tempPos.z = tempPos.z + (25 * i);
        if(PlayerPrefs.GetInt("Level")<20)
        {
            tempPos.x = lastPosX + Random.Range(-(i),(i));
        }
        else
        {
            tempPos.x = lastPosX + Random.Range(-(i*i),(i*i));
        }
        tempPos.x = Mathf.Clamp(tempPos.x, -11.5f, 3.5f); //Clamp values if out of path range

        lastPosX = tempPos.x;
        max_score = 20;//GameObject.FindGameObjectsWithTag("point").Length;
        GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>().maxValue = max_score;

        return tempPos;
    }

    public void LoadSameLevel()
    {
        TinySauce.OnGameStarted(PlayerPrefs.GetInt("Level").ToString());
        playerBall.GetComponent<Rigidbody>().MovePosition(playerInitialPos);
        for(int i = 0; i < 19; i++)
        {
            Destroy(CubesOnScene[i]); //Destroy all cubes, broken or unbroken
        }

        CubesOnScene[0] = Instantiate(CubePrefab, firstCubeTransform.position, firstCubeTransform.rotation);
        Vector3 pos = firstCubeTransform.position;

        move.score=0;
        GameObject.Find("Score_New").GetComponent<Text>().text ="";

        for(int i = 0; i < 18; i++)
        {
            pos.x = CubePositionsX[i];
            pos.z += 25f;
            CubesOnScene[i+1] = Instantiate(CubePrefab, pos, firstCubeTransform.rotation);
        }
    }
}
