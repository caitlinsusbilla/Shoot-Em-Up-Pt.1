using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInvaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    private Vector3 _direction = Vector2.right;

    void Start()
    {
        SpawnInvaders();
    }


    void SpawnInvaders()
    {
        Vector3 centering = new Vector2(-columns / 2, -rows / 2);
        Vector3 rowPosition = new Vector3(centering.x, centering.y, 0.0f);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Invader invader = Instantiate(prefabs[row % prefabs.Length], transform);
                Vector3 position = rowPosition + new Vector3(col * 2.0f, row * 2.0f, 0.0f);
                invader.transform.localPosition = position;
            }
        }
    }
}
