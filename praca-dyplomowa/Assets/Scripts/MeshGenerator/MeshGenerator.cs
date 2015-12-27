using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MeshGenerator : MonoBehaviour {

    public float height;
    public float nCurveRes;
    public float baseLength;

    public void Generate()
    {
        Stopwatch.StartTimer();

        //Generating MeshFilter component (if not exists) and applying new mesh to it
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        if(meshFilter==null) meshFilter = this.gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        //Generating MeshRenderer component (if not exists) and applying standard diffuse material to it
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        if (meshRenderer == null) meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Diffuse"));
        
        Stopwatch.RegisterTime("Managing object components");

        //Base
        ushort level = 0;
        List<Vector3> newVerticles = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();
        List<int> newTriangles = new List<int>();

        newVerticles.Add(new Vector3(-baseLength / 2, 0f, -baseLength / 2));
        newVerticles.Add(new Vector3(baseLength / 2, 0f, -baseLength / 2));
        newVerticles.Add(new Vector3(baseLength / 2, 0f, baseLength / 2));
        newVerticles.Add(new Vector3(-baseLength / 2, 0f, baseLength / 2));

        newTriangles.Add(0);
        newTriangles.Add(1);
        newTriangles.Add(2);

        newTriangles.Add(2);
        newTriangles.Add(3);
        newTriangles.Add(0);

        level++;

        float length = baseLength;

        while (level != height)
        {
            length -= length / nCurveRes;
            newVerticles.Add(new Vector3(-length / 2, level, -length / 2));
            newVerticles.Add(new Vector3(length / 2, level, -length / 2));
            newVerticles.Add(new Vector3(length / 2, level, length / 2));
            newVerticles.Add(new Vector3(-length / 2, level, length / 2));

            int lastIndex = newVerticles.Count-1;

            newTriangles.Add(lastIndex-3);
            newTriangles.Add(lastIndex-6);
            newTriangles.Add(lastIndex-7);

            newTriangles.Add(lastIndex - 3);
            newTriangles.Add(lastIndex - 2);
            newTriangles.Add(lastIndex - 6);

            newTriangles.Add(lastIndex - 2);
            newTriangles.Add(lastIndex - 1);
            newTriangles.Add(lastIndex - 6);


            newTriangles.Add(lastIndex - 1);
            newTriangles.Add(lastIndex - 5);
            newTriangles.Add(lastIndex - 6);


            newTriangles.Add(lastIndex);
            newTriangles.Add(lastIndex - 3);
            newTriangles.Add(lastIndex- 4);

            newTriangles.Add(lastIndex);
            newTriangles.Add(lastIndex - 4);
            newTriangles.Add(lastIndex - 5);

            newTriangles.Add(lastIndex);
            newTriangles.Add(lastIndex - 5);
            newTriangles.Add(lastIndex - 1);

            newTriangles.Add(lastIndex - 4);
            newTriangles.Add(lastIndex - 3);
            newTriangles.Add(lastIndex - 7 );



            level++;
        }

        newVerticles.Add(new Vector3(0f, height, 0f)); //Cone tip
        int tipIndex = newVerticles.Count-1;

        newTriangles.Add(tipIndex);
        newTriangles.Add(tipIndex - 3);
        newTriangles.Add(tipIndex - 4);

        newTriangles.Add(tipIndex);
        newTriangles.Add(tipIndex - 2);
        newTriangles.Add(tipIndex - 3);

        newTriangles.Add(tipIndex);
        newTriangles.Add(tipIndex - 1);
        newTriangles.Add(tipIndex - 2);

        newTriangles.Add(tipIndex);
        newTriangles.Add(tipIndex - 4);
        newTriangles.Add(tipIndex - 1);
 
        foreach(Vector3 vt in newVerticles)
        {
            newUVs.Add(new Vector2(vt.x, vt.z));
        }

        Stopwatch.RegisterTime("Mesh verticles generation");

        mesh.SetVertices(newVerticles);
        mesh.SetUVs(0, newUVs);
        mesh.SetTriangles(newTriangles.ToArray(),0);
        mesh.RecalculateNormals();

        Stopwatch.StopTimer();
        UnityEngine.Debug.Log("Generation completed");
        Stopwatch.DebugTimes();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
