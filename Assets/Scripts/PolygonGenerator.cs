using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonGenerator : MonoBehaviour
{

    public List<Vector3> newVertices = new List<Vector3>();
    public List<int> newTriangles = new List<int>();
    public List<Vector2> newUV = new List<Vector2>();

    public List<Vector3> colVertices = new List<Vector3>();
    public List<int> colTriangles = new List<int>();
    private int colCount;

    private Mesh mesh;
    private MeshCollider col;

    private float tUnit = 0.125f;
    private Vector2 tDirt = new Vector2(2, 1);
    private Vector2 tBedRock = new Vector2(3, 4);
    public List<Vector2> tGrasses = new List<Vector2>();

    public byte[,] blocks;

    private int squareCount;

    public float blockWidth;

    public ChunkGen chunkGen;

    public int extraChoose;

    // Use this for initialization
    void Start()
    {
        chunkGen = GameObject.Find("Terrain").GetComponent<ChunkGen>();
        blockWidth = chunkGen.blockWidth;
        
        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<MeshCollider>();

        for (int i = 0; i < 8; i++)
        {
            tGrasses.Add(new Vector2(i, 7));
        }

        GenTerrain();
        AddExtras();
        BuildMesh();
        UpdateMesh();
    }

    void GenTerrain()
    {
        blocks = new byte[chunkGen.chunkWidth, chunkGen.chunkHeight];

        for (int px = 0; px < blocks.GetLength(0); px++)
        {
            int grass = Noise((int)(transform.position.x / blockWidth) + px, Random.Range(0, 5), 1f, chunkGen.chunkHeight, 1);
            //grass += (Noise(px, 0, 0, 30, 1) - 15);
            //grass += (Noise(px, 0, 0, 10, 1) - 5);
            int bedrock = Noise((int)(transform.position.x / blockWidth) + px, Random.Range(0, 5), 100f, 10, 1) + 10;
            for (int py = 0; py < blocks.GetLength(1); py++)
            {
                if (py < grass)
                {
                    blocks[px, py] = 1;
                }
                if (py == grass)
                {
                    blocks[px, py] = 2;
                }
                /*if (Noise(px, py * 2, 25, 14, 1) > 7)
                { //Caves
                    blocks[px, py] = 0;
                }
                if (Noise(px, 0, 16, 14, 1) > 2)
                { //Caves
                    if (py == (int)Noise(px, 2, 30, 30, 1) || py == (int)Noise(px, 2, 30, 30, 1) + (int)(Noise(px, 12, 10, 2, 1) - 1) || py == (int)Noise(px, 2, 30, 30, 1) + (int)(Noise(px, 2, 10, 2, 1) - 1))
                    {
                        blocks[px, py] = 0;
                        for (int i = 0; i < 2; i++)
                        {
                            blocks[px, py + (int)Random.Range(-1, 1)] = 0;
                        }
                    }
                }
                if (Noise(px, 0, 16, 14, 1) > 2)
                { //Caves
                    if (py == (int)Noise(px, 9, 20, 50, 1) || py == (int)Noise(px, 9, 20, 50, 1) + (int)(Noise(px, -7, 10, 2, 1) - 1) || py == (int)Noise(px, 9, 20, 50, 1) + (int)(Noise(px, 7, 10, 2, 1) - 1))
                    {
                        blocks[px, py] = 0;
                        for (int i = 0; i < 2; i++)
                        {
                            blocks[px, py + (int)Random.Range(-1, 1)] = 0;
                        }
                    }
                }*/
                if (py < bedrock)
                {
                    blocks[px, py] = 3;
                }
            }
            /*int stone = Noise(px, 0, 80, 15, 1);
            stone += Noise(px, 0, 50, 30, 1);
            stone += Noise(px, 0, 10, 10, 1);
            stone += 75;

            print(stone);

            int dirt = Noise(px, 0, 100f, 35, 1);
            dirt += Noise(px, 100, 50, 30, 1);
            dirt += 75;


            for (int py = 0; py < blocks.GetLength(1); py++)
            {
                if (py < stone)
                {
                    blocks[px, py] = 1;

                    //The next three lines make dirt spots in random places
                    if (Noise(px, py, 12, 16, 1) > 10)
                    {
                        blocks[px, py] = 2;

                    }

                    //The next three lines remove dirt and rock to make caves in certain places
                    if (Noise(px, py * 2, 16, 14, 1) > 10)
                    { //Caves
                        blocks[px, py] = 0;

                    }

                }
                else if (py < dirt)
                {
                    blocks[px, py] = 2;
                }

            }*/
        }
    }


    void AddExtras()
    {
        for (int px = 0; px < blocks.GetLength(0); px++)
        {
            for (int py = 0; py < blocks.GetLength(1); py++)
            {
                if ((blocks[px, py] == 1 || blocks[px, py] == 1) && blocks[px, py + 1] == 0 && blocks[px, py + 2] == 0)
                {
                    extraChoose = (int)Random.Range(0, 5);
                    //continue adding trees etc here
                }
            }
        }
    }

    void BuildMesh()
    {
        for (int px = 0; px < blocks.GetLength(0); px++)
        {
            for (int py = 0; py < blocks.GetLength(1); py++)
            {
                if (blocks[px, py] != 0)
                {

                    GenCollider(px, py);

                    if (blocks[px, py] == 1)
                    {
                        GenSquare(px, py, tDirt);
                    }
                    else if (blocks[px, py] == 2 || (blocks[px, py] == 1 && py == chunkGen.chunkHeight))
                    {
                        GenSquare(px, py, tGrasses[Random.Range(0, 8)]);
                    }
                    else if (blocks[px, py] == 3)
                    {
                        GenSquare(px, py, tBedRock);
                    }
                }
            }
        }
    }

    byte Block(int x, int y)
    {

        if (x == -1 || x == blocks.GetLength(0) || y == -1 || y == blocks.GetLength(1))
        {
            return (byte)1;
        }

        return blocks[x, y];
    }

    void GenCollider(int x, int y)
    {

        //Top
        if (Block(x, y + 1) == 0 || y == chunkGen.chunkHeight)
        {
            colVertices.Add(new Vector3(x * blockWidth, y * blockWidth, 15));
            colVertices.Add(new Vector3((x + 1) * blockWidth, y * blockWidth, 15));
            colVertices.Add(new Vector3((x + 1) * blockWidth, y * blockWidth, 0));
            colVertices.Add(new Vector3(x * blockWidth, y * blockWidth, 0));

            ColliderTriangles();

            colCount++;
        }

        //bot
        if (Block(x, y - 1) == 0)
        {
            colVertices.Add(new Vector3(x * blockWidth, (y - 1) * blockWidth, 0));
            colVertices.Add(new Vector3((x + 1) * blockWidth, (y - 1) * blockWidth, 0));
            colVertices.Add(new Vector3((x + 1) * blockWidth, (y - 1) * blockWidth, 15));
            colVertices.Add(new Vector3(x * blockWidth, (y - 1) * blockWidth, 15));

            ColliderTriangles();
            colCount++;
        }

        //left
        if (Block(x - 1, y) == 0)
        {
            colVertices.Add(new Vector3(x * blockWidth, (y - 1) * blockWidth, 15));
            colVertices.Add(new Vector3(x * blockWidth, y * blockWidth, 15));
            colVertices.Add(new Vector3(x * blockWidth, y * blockWidth, 0));
            colVertices.Add(new Vector3(x * blockWidth, (y - 1) * blockWidth, 0));

            ColliderTriangles();

            colCount++;
        }

        //right
        if (Block(x + 1, y) == 0)
        {
            colVertices.Add(new Vector3((x + 1) * blockWidth, y * blockWidth, 15));
            colVertices.Add(new Vector3((x + 1) * blockWidth, (y - 1) * blockWidth, 15));
            colVertices.Add(new Vector3((x + 1) * blockWidth, (y - 1) * blockWidth, 0));
            colVertices.Add(new Vector3((x + 1) * blockWidth, y * blockWidth, 0));

            ColliderTriangles();

            colCount++;
        }

    }

    void ColliderTriangles()
    {
        colTriangles.Add(colCount * 4);
        colTriangles.Add((colCount * 4) + 1);
        colTriangles.Add((colCount * 4) + 3);
        colTriangles.Add((colCount * 4) + 1);
        colTriangles.Add((colCount * 4) + 2);
        colTriangles.Add((colCount * 4) + 3);
    }


    void GenSquare(int x, int y, Vector2 texture)
    {
        newVertices.Add(new Vector3(x * blockWidth, y * blockWidth, 0));
        newVertices.Add(new Vector3((x + 1) * blockWidth, y * blockWidth, 0));
        newVertices.Add(new Vector3((x + 1) * blockWidth, (y - 1) * blockWidth, 0));
        newVertices.Add(new Vector3(x * blockWidth, (y - 1) * blockWidth, 0));

        newTriangles.Add(squareCount * 4);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 3);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 2);
        newTriangles.Add((squareCount * 4) + 3);

        newUV.Add(new Vector2(tUnit * texture.x, (tUnit * texture.y) + tUnit));
        newUV.Add(new Vector2(((tUnit * texture.x) + tUnit), (tUnit * texture.y) + tUnit));
        newUV.Add(new Vector2(((tUnit * texture.x) + tUnit), tUnit * texture.y));
        newUV.Add(new Vector2(tUnit * texture.x, tUnit * texture.y));

        squareCount++;

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.RecalculateNormals();

        newVertices.Clear();
        newTriangles.Clear();
        newUV.Clear();
        squareCount = 0;

        Mesh newMesh = new Mesh();
        newMesh.vertices = colVertices.ToArray();
        newMesh.triangles = colTriangles.ToArray();
        col.sharedMesh = newMesh;

        colVertices.Clear();
        colTriangles.Clear();
        colCount = 0;
    }

    int Noise(int x, int y, float scale, float mag, float exp)
    {

        return (int)(Mathf.Pow((Mathf.PerlinNoise(x / scale, y / scale) * mag), (exp)));

    }
}