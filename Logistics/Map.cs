using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    // a map of the route
    private int go;

    // take current level and isLeft as parameters
    public int WhichWay(int i, bool isLeft)
    {

        switch (i)
        {
            case 4:
            {
                if (isLeft)
                    go = 5;
                else
                    go = 6;
                break;
            }
            case 5:
            {
                if (isLeft)
                    go = 6;
                else
                    go = 7;
                break;
            }
            case 6:
            {
                if (isLeft)
                    go = 8;
                else
                    go = 7;
                break;
            }
            case 7:
            {
                if (isLeft)
                    go = 8;
                else    
                    go = 9;
                break;
            }
            case 8:
            {
                if (isLeft)
                    go = 9;
                else    
                    go = 10;
                break;
            }
            case 9:
            {
                if (isLeft)
                    go = 10;
                else    
                    go = 10;
                break;
            }
            case 10:
            {
                go = 11;
                break;
            }
        }

        return go;
    }

}
