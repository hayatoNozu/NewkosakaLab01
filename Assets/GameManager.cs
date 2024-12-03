using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int whg = 0;
    public static int reg = 0;
    public static int grg = 0;
    public static int blg = 0;
    public static int cyg = 0;
    public static int mag = 0;
    public static int yeg = 0;
    public static int whgD = 0;
    public static int regD = 0;
    public static int grgD = 0;
    public static int blgD = 0;
    public static int cygD = 0;
    public static int magD = 0;
    public static int yegD = 0;
    public static float rate = 0;
    public static int AG = 0;
    public static int DG = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameEnd()
    {
        rate = DG / AG *100;
        if(rate < 60)
        {

        }
        else if(rate < 70)
        {
            //C
        }
        else if(rate < 80)
        {
            //B
        }
        else if(rate < 90)
        {
            //A
        }
        else 
        {
            //S
        }
    }
}
