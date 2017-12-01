using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour
{

    public TileType[] tileTypes;
    public GameObject[] loot;
    public GameObject secret;
    public GameObject eyeEnemy;
    public GameObject enemySpawner;
    public GameObject smileyKey;
    public GameObject alienKey;
    public GameObject pizzaKey;
    public GameObject platform;

    int[,] tiles;

    int mapSizeX = 50;
    int mapSizeZ = 50;

    void Start()
    {
        tiles = new int[mapSizeX, mapSizeZ];

        GenerateMapBorder();
        GenerateMapData();
        GenerateMapVisual();
        GenerateEnemies();
        GenerateKeys();
        GenerateLoot();
    }

    void GenerateMapData()
    {
        int x, z;

        for (x = 1; x < mapSizeX - 1; x++)
        {
            for (z = 1; z < mapSizeX - 1; z++)
            {
                tiles[x, z] = 1;
            }
        }

        //Walls
        for (x = 14; x <= 14; x++)
        {
            for (z = 1; z < 15; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 14; x <= 25; x++)
        {
            for (z = 15; z < 16; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 28; x <= 35; x++)
        {
            for (z = 15; z < 16; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 35; x <= 35; x++)
        {
            for (z = 4; z < 16; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 35; x <= 46; x++)
        {
            for (z = 4; z < 5; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 46; x <= 46; x++)
        {
            for (z = 4; z < 24; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 32; x <= 46; x++)
        {
            for (z = 23; z < 24; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 42; x <= 47; x++)
        {
            for (z = 46; z < 47; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 46; x <= 46; x++)
        {
            for (z = 43; z < 47; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 42; x <= 42; x++)
        {
            for (z = 47; z < 49; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 35; x <= 43; x++)
        {
            for (z = 42; z < 43; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 34; x <= 34; x++)
        {
            for (z = 42; z < 46; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 5; x <= 34; x++)
        {
            for (z = 35; z < 36; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 5; x <= 5; x++)
        {
            for (z = 12; z < 36; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 20; x <= 20; x++)
        {
            for (z = 35; z < 49; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 27; x <= 27; x++)
        {
            for (z = 28; z < 35; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 10; x <= 27; x++)
        {
            for (z = 27; z < 28; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 29; x <= 29; x++)
        {
            for (z = 16; z < 24; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 25; x <= 25; x++)
        {
            for (z = 1; z < 6; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 1; x <= 9; x++)
        {
            for (z = 8; z < 9; z++)
            {
                tiles[x, z] = 0;
            }
        }

        for (x = 9; x <= 9; x++)
        {
            for (z = 3; z < 9; z++)
            {
                tiles[x, z] = 0;
            }
        }

        //Hazards

        for (x = 10; x <= 19; x++)
        {
            for (z = 28; z < 35; z++)
            {
                if (x != 14 || z != 31)
                {
                    tiles[x, z] = 2;
                }
            }
        }


        for (x = 5; x <= 13; x++)
        {
            for (z = 39; z < 47; z++)
            {
                if (x != 9 || z != 42)
                {
                    tiles[x, z] = 2;
                }
            }
        }

        for (x = 33; x <= 35; x++)
        {
            for (z = 17; z < 22; z++)
            {
                if (x != 34 || z != 19)
                {
                    tiles[x, z] = 2;
                }
            }
        }


        for (x = 30; x <= 45; x++)
        {
            for (z = 16; z < 17; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 36; x <= 36; x++)
        {
            for (z = 5; z < 23; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 32; x <= 33; x++)
        {
            for (z = 16; z < 23; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 36; x <= 45; x++)
        {
            for (z = 5; z < 6; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 45; x <= 45; x++)
        {
            for (z = 5; z < 23; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 39; x <= 42; x++)
        {
            for (z = 5; z < 23; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 36; x <= 45; x++)
        {
            for (z = 11; z < 12; z++)
            {
                tiles[x, z] = 2;
            }
        }


        for (x = 32; x <= 45; x++)
        {
            for (z = 22; z < 23; z++)
            {
                tiles[x, z] = 2;
            }
        }


        for (x = 44; x < 49; x++)
        {
            for (z = 47; z < 49; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 47; x < 49; x++)
        {
            for (z = 43; z < 49; z++)
            {
                tiles[x, z] = 2;
            }
        }


        for (x = 3; x <= 5; x++)
        {
            for (z = 1; z < 4; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 1; x <= 5; x++)
        {
            for (z = 3; z < 6; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 10; x <= 13; x++)
        {
            for (z = 8; z < 12; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 1; x <= 20; x++)
        {
            for (z = 9; z < 11; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 19; x <= 25; x++)
        {
            for (z = 9; z < 14; z++)
            {
                tiles[x, z] = 2;
            }
        }

        for (x = 24; x <= 28; x++)
        {
            for (z = 12; z < 24; z++)
            {
                tiles[x, z] = 2;
            }
        }

    }

    void GenerateMapBorder()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                if (x == 0 || x == mapSizeX || z == 0 || z == mapSizeZ)
                {
                    tiles[x, z] = 0;
                }
            }
        }
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeX; z++)
            {

                TileType tt = tileTypes[tiles[x, z]];
                Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity);

            }
        }
    }

    void GenerateEnemies()
    {
        Instantiate(eyeEnemy, new Vector3(11, 0.6f, 4), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(7, 0.6f, 6), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(2, 0.6f, 18), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(2, 0.6f, 30), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(10, 0.6f, 47), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(16, 0.6f, 42), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(27, 0.6f, 2), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(42.5f, 0.6f, 13), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(24, 0.6f, 30), Quaternion.identity);
        Instantiate(eyeEnemy, new Vector3(20, 0.6f, 32.5f), Quaternion.identity);

        Instantiate(enemySpawner, new Vector3(16, 0, 21), Quaternion.identity);
        Instantiate(enemySpawner, new Vector3(43.5f, 0, 7), Quaternion.identity);
        Instantiate(enemySpawner, new Vector3(25, 0, 42), Quaternion.identity);

        Instantiate(platform, new Vector3(41, 1, 33), Quaternion.identity);
    }

    void GenerateKeys()
    {
        Instantiate(pizzaKey, new Vector3(1.7f, 0.5f, 1.7f), Quaternion.identity);
        Instantiate(smileyKey, new Vector3(9, 0.5f, 42.1f), Quaternion.identity);
        Instantiate(alienKey, new Vector3(22.7f, 0.5f, 31.3f), Quaternion.identity);
    }

    void GenerateLoot()
    {
        GameObject lootInstanceOne = loot[Random.Range(0, loot.Length)];
        GameObject lootInstanceTwo = loot[Random.Range(0, loot.Length)];
        GameObject lootInstanceThree = loot[Random.Range(0, loot.Length)];
        GameObject lootInstanceFour = loot[Random.Range(0, loot.Length)];

        Instantiate(lootInstanceOne, new Vector3(16f, 0.6f, 13f), Quaternion.identity);
        Instantiate(lootInstanceTwo, new Vector3(32f, 0.6f, 12f), Quaternion.identity);
        Instantiate(lootInstanceThree, new Vector3(41, 0.6f, 33), Quaternion.identity);
        Instantiate(lootInstanceFour, new Vector3(35.7f, 0.6f, 43.6f), Quaternion.identity);

        Instantiate(secret, new Vector3(43.1f, 0.6f, 47.3f), Quaternion.identity);
    }
}
