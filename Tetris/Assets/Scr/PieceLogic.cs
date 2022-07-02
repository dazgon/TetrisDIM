using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PieceLogic : MonoBehaviour
{
    private float tiempoAnterior;
    public float tiempoCaida = 0.8f;

    public static int alto = 20;
    public static int ancho = 10;

    public Vector3 puntoRotacion;

    private static Transform[,] grid = new Transform[ancho, alto];

    public static int score = 0;
    public static event Action <int> updateScore; 
    public static int dificultad = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            transform.position += new Vector3(-1, 0, 0); 
            if (!Limites())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }

        if (Time.time-tiempoAnterior > (Input.GetKey(KeyCode.DownArrow)?tiempoCaida/20: tiempoCaida))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(0, -1, 0);

                AñadirAGrid();
                RevisarLineas();

                this.enabled = false;
                FindObjectOfType<GeneratePieces>().NewPiece();
            }

            tiempoAnterior = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3(0, 0, 1), -90);
            if (!Limites())
            {
                transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3(0, 0, 1), 90);
            }
        }
        SubirNivel();
        SubirDificultad();
        
    }

    bool Limites() 
    {
        foreach (Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            if (enteroX < 0 || enteroX >= ancho || enteroY < 0 || enteroY >= alto)
            {
                return false;
            }

            if (grid[enteroX,enteroY]!=null)
            {
                return false;
            }
        }

        
        return true;
    }

    void AñadirAGrid()
    {
        foreach(Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            grid[enteroX, enteroY] = hijo;

            if (enteroY >= 19)
            {
                score = 0;
                dificultad = 0;
                tiempoCaida = 0.8f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }


        }
    }

    void RevisarLineas()
    {
        for (int i = alto - 1 ; i >= 0; i--)
        {
            if (TieneLinea(i))
            {
                BorrarLinea(i);
                BajarLinea(i);
            }
        }
    }

    bool TieneLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            if (grid[j,i] == null)
            {
                return false;
            }
        }

        score += 100;
        updateScore?.Invoke(score);

        return true;
    }

    void BorrarLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void BajarLinea(int i)
    {
        for (int y = i; y < alto; y++)
        {
            for (int j = 0; j < ancho; j++)
            {
                if (grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void SubirNivel()
    {
        switch (score)
        {
            case 500:
                dificultad = 1;
                break;

            case 600:
                dificultad = 1;
                break;

            case 700:
                dificultad = 1;
                break;

            case 800:
                dificultad = 1;
                break;

            case 900:
                dificultad = 1;
                break;

            case 1000:
                dificultad = 2;
                break;

            case 1100:
                dificultad = 2;
                break;

            case 1200:
                dificultad = 2;
                break;

            case 1300:
                dificultad = 2;
                break;

            case 1400:
                dificultad = 2;
                break;

            case 1500:
                dificultad = 2;
                break;

        }
    }

    void SubirDificultad()
    {
        switch (dificultad)
        {
            case 1:
                tiempoCaida = 0.6f;
                break;

            case 2:
                tiempoCaida = 0.4f;
                break;
        } 
    }

}
