﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private string filePath = "Assets/_MazeMakerAssets/Maps/map1.txt";

    public GameObject[] prefabs; // Mảng prefab tương ứng với các giá trị số trong tệp văn bản
    private string[,] mapData; // Mảng hai chiều để lưu dữ liệu bản đồ
    private Quaternion[] prefabRotations;

    void Start()
    {
        Rotations();
        LoadMapFromFile();
        GenerateMap();
    } 

    public void Rotations()
    {
        prefabRotations = new Quaternion[prefabs.Length];

        prefabRotations[0] = Quaternion.Euler(0f, 0f, 0f);

        prefabRotations[1] = Quaternion.Euler(-90f, 0f, 0f);

        prefabRotations[4] = Quaternion.Euler(-90f, 0f, 90f);

    }

    public void LoadMapFromFile()
    {
        string[] lines = File.ReadAllLines(filePath);
        int rowCount = lines.Length;
        int columnCount = lines[0].Length;

        mapData = new string[rowCount, columnCount];

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < columnCount; col++)
            {
                string value = lines[rowCount- row - 1][col].ToString();
                mapData[row, col] = value;
            }
        }
    }

    public void GenerateMap()
    {
        int rowCount = mapData.GetLength(0);
        int columnCount = mapData.GetLength(1);

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < columnCount; col++)
            {
                string value = mapData[row, col];

                if (int.TryParse(value, out int intValue) && intValue >= 0 && intValue < prefabs.Length)
                {
                    GameObject prefab = prefabs[intValue];
                    

                    if (intValue == 1)
                    {
                        Vector3 position = new Vector3(col, -3f, row);
                        Quaternion rotation = prefabRotations[intValue];
                        GameObject instantiatedPrefab = Instantiate(prefab, position, rotation, transform);

                    }
                    else if (intValue == 4)
                    {
                        Vector3 position = new Vector3(col, -0.5f, row);
                        Quaternion rotation = prefabRotations[intValue];
                        GameObject instantiatedPrefab = Instantiate(prefab, position, rotation, transform);
                    }
                    else if (intValue == 6)
                    {
                        Vector3 position = new Vector3(col, -3f, row);
                        Quaternion rotation = prefabRotations[intValue];
                        GameObject instantiatedPrefab = Instantiate(prefab, position, rotation, transform);
                    }
                    else if (intValue == 5)
                    {
                        continue;
                    }

                    else
                    {
                        Vector3 position = new Vector3(col, 0f, row);
                        Quaternion rotation = prefabRotations[intValue];
                        GameObject instantiatedPrefab = Instantiate(prefab, position, rotation, transform);
                    }

                    if (intValue == 3)
                    {                    
                        GameObject secondPrefab = prefabs[0];
                        Quaternion secondPrefabRotation = prefabRotations[0];
                        Vector3 secondPrefabPosition = new Vector3(col, 0f, row);
                        GameObject instantiatedSecondPrefab = Instantiate(secondPrefab, secondPrefabPosition, secondPrefabRotation, transform);
                    }
                }
            }
        }
    }

}