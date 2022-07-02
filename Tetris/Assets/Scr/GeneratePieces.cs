using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePieces : MonoBehaviour
{
    public GameObject[] pieces;

    private GameObject nextPiece;
    private GameObject previewPiece;

    private bool gameStart = false;
    private Vector2 previewPiecePosition = new Vector2(-7.79f, 14.89f);

    // Start is called before the first frame update
    void Start()
    {
        NewPiece();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPiece()
    {
        if (!gameStart)
        {
            gameStart = true;
            nextPiece = Instantiate(pieces[Random.Range(0, pieces.Length)], transform.position, Quaternion.identity);
            previewPiece = Instantiate(pieces[Random.Range(0, pieces.Length)], previewPiecePosition, Quaternion.identity);
            previewPiece.GetComponent<PieceLogic>().enabled = false;
        }

        else
        {
            previewPiece.transform.localPosition = new Vector2(4.0f, 17.0f);
            nextPiece = previewPiece;
            nextPiece.GetComponent<PieceLogic>().enabled = true;

            previewPiece = Instantiate(pieces[Random.Range(0, pieces.Length)], previewPiecePosition, Quaternion.identity);
            previewPiece.GetComponent<PieceLogic>().enabled = false;

        }

    }
}
