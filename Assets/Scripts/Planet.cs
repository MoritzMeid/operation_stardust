﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Planet class is responsible for generating a tiny procedural planet. It does this by subdividing an Icosahedron, then
// randomly selecting groups of Polygons to extrude outwards. These become the lowlands and hills of the planet, while the
// unextruded Polygons become the ocean.

public class Planet : MonoBehaviour
{
    // These public parameters can be tweaked to give different styles to your planet.


    public Material m_Material;

    public int m_NumberOfContinents = 5;
    public float m_ContinentSizeMax = 1.0f;
    public float m_ContinentSizeMin = 0.1f;

    public int m_NumberOfHills = 5;
    public float m_HillSizeMax = 1.0f;
    public float m_HillSizeMin = 0.1f;


    // Internally, the Planet object stores its mesh as a child GameObject:
    GameObject m_PlanetMesh;



    // The subdivided icosahedron that we use to generate our planet is represented as a list
    // of Polygons, and a list of Vertices for those Polygons:
    List<Polygon> m_Polygons;
    List<Vector3> m_Vertices;

    public void Start()
    {
        // Create an icosahedron, subdivide it three times so that we have plenty of polys
        // to work with.

        InitAsIcosohedron();
        Subdivide(3);

        // When we begin extruding polygons, we'll need each one to know who its immediate
        //neighbors are. Calculate that now.

        CalculateNeighbors();

        // By default, everything is colored blue. As we extrude land forms, we'll change their colors to match.

        Color32 colorOcean = new Color32(0, 80, 220, 0);
        Color32 colorGrass = new Color32(0, 220, 0, 0);
        Color32 colorDirt = new Color32(180, 140, 20, 0);

        foreach (Polygon p in m_Polygons)
            p.m_Color = colorOcean;

        // Now we build a set of Polygons that will become the land. We do this by generating
        // randomly sized spheres on the surface of the planet, and adding any Polygon that falls
        // inside that sphere.

        PolySet landPolys = new PolySet();

        for (int i = 0; i < m_NumberOfContinents; i++)
        {
            float continentSize = Random.Range(m_ContinentSizeMin, m_ContinentSizeMax);

            PolySet newLand = GetPolysInSphere(Random.onUnitSphere, continentSize, m_Polygons);

            landPolys.UnionWith(newLand);
        }

        // This is our land now, so color it green. =)

        foreach (Polygon landPoly in landPolys)
        {
            landPoly.m_Color = colorGrass;
        }

        // The Extrude function will raise the land Polygons up out of the water.
        // It also generates a strip of new Polygons to connect the newly raised land
        // back down to the water level. We can color this vertical strip of land brown like dirt.

        PolySet sides = Extrude(landPolys, 0.05f);

        foreach (Polygon side in sides)
        {
            side.m_Color = colorDirt;
        }

        // Grab additional polygons to generate hills, but only from the set of polygons that are land.

        PolySet hillPolys = new PolySet();

        for (int i = 0; i < m_NumberOfHills; i++)
        {
            float hillSize = Random.Range(m_HillSizeMin, m_HillSizeMax);

            PolySet newHill = GetPolysInSphere(Random.onUnitSphere, hillSize, landPolys);

            hillPolys.UnionWith(newHill);
        }

        sides = Extrude(hillPolys, 0.05f);

        foreach (Polygon side in sides)
        {
            side.m_Color = colorDirt;
        }

        // Okay, we're done! Let's generate an actual game mesh for this planet.

        GenerateMesh();


    }


    public void InitAsIcosohedron()
    {
        m_Polygons = new List<Polygon>();
        m_Vertices = new List<Vector3>();

        // An icosahedron has 12 vertices, and
        // since they're completely symmetrical the
        // formula for calculating them is kind of
        // symmetrical too:

        float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        m_Vertices.Add(new Vector3(-1, t, 0).normalized);
        m_Vertices.Add(new Vector3(1, t, 0).normalized);
        m_Vertices.Add(new Vector3(-1, -t, 0).normalized);
        m_Vertices.Add(new Vector3(1, -t, 0).normalized);
        m_Vertices.Add(new Vector3(0, -1, t).normalized);
        m_Vertices.Add(new Vector3(0, 1, t).normalized);
        m_Vertices.Add(new Vector3(0, -1, -t).normalized);
        m_Vertices.Add(new Vector3(0, 1, -t).normalized);
        m_Vertices.Add(new Vector3(t, 0, -1).normalized);
        m_Vertices.Add(new Vector3(t, 0, 1).normalized);
        m_Vertices.Add(new Vector3(-t, 0, -1).normalized);
        m_Vertices.Add(new Vector3(-t, 0, 1).normalized);

        // And here's the formula for the 20 sides,
        // referencing the 12 vertices we just created.

        m_Polygons.Add(new Polygon(0, 11, 5));
        m_Polygons.Add(new Polygon(0, 5, 1));
        m_Polygons.Add(new Polygon(0, 1, 7));
        m_Polygons.Add(new Polygon(0, 7, 10));
        m_Polygons.Add(new Polygon(0, 10, 11));
        m_Polygons.Add(new Polygon(1, 5, 9));
        m_Polygons.Add(new Polygon(5, 11, 4));
        m_Polygons.Add(new Polygon(11, 10, 2));
        m_Polygons.Add(new Polygon(10, 7, 6));
        m_Polygons.Add(new Polygon(7, 1, 8));
        m_Polygons.Add(new Polygon(3, 9, 4));
        m_Polygons.Add(new Polygon(3, 4, 2));
        m_Polygons.Add(new Polygon(3, 2, 6));
        m_Polygons.Add(new Polygon(3, 6, 8));
        m_Polygons.Add(new Polygon(3, 8, 9));
        m_Polygons.Add(new Polygon(4, 9, 5));
        m_Polygons.Add(new Polygon(2, 4, 11));
        m_Polygons.Add(new Polygon(6, 2, 10));
        m_Polygons.Add(new Polygon(8, 6, 7));
        m_Polygons.Add(new Polygon(9, 8, 1));
    }

    public void Subdivide(int recursions)
    {
        var midPointCache = new Dictionary<int, int>();

        for (int i = 0; i < recursions; i++)
        {
            var newPolys = new List<Polygon>();
            foreach (var poly in m_Polygons)
            {
                int a = poly.m_Vertices[0];
                int b = poly.m_Vertices[1];
                int c = poly.m_Vertices[2];

                // Use GetMidPointIndex to either create a
                // new vertex between two old vertices, or
                // find the one that was already created.

                int ab = GetMidPointIndex(midPointCache, a, b);
                int bc = GetMidPointIndex(midPointCache, b, c);
                int ca = GetMidPointIndex(midPointCache, c, a);

                // Create the four new polygons using our original
                // three vertices, and the three new midpoints.
                newPolys.Add(new Polygon(a, ab, ca));
                newPolys.Add(new Polygon(b, bc, ab));
                newPolys.Add(new Polygon(c, ca, bc));
                newPolys.Add(new Polygon(ab, bc, ca));
            }
            // Replace all our old polygons with the new set of
            // subdivided ones.
            m_Polygons = newPolys;
        }
    }

    public int GetMidPointIndex(Dictionary<int, int> cache, int indexA, int indexB)
    {
        // We create a key out of the two original indices
        // by storing the smaller index in the upper two bytes
        // of an integer, and the larger index in the lower two
        // bytes. By sorting them according to whichever is smaller
        // we ensure that this function returns the same result
        // whether you call
        // GetMidPointIndex(cache, 5, 9)
        // or...
        // GetMidPointIndex(cache, 9, 5)

        int smallerIndex = Mathf.Min(indexA, indexB);
        int greaterIndex = Mathf.Max(indexA, indexB);
        int key = (smallerIndex << 16) + greaterIndex;

        // If a midpoint is already defined, just return it.

        int ret;
        if (cache.TryGetValue(key, out ret))
            return ret;

        // If we're here, it's because a midpoint for these two
        // vertices hasn't been created yet. Let's do that now!

        Vector3 p1 = m_Vertices[indexA];
        Vector3 p2 = m_Vertices[indexB];
        Vector3 middle = Vector3.Lerp(p1, p2, 0.5f).normalized;

        ret = m_Vertices.Count;
        m_Vertices.Add(middle);

        // Add our new midpoint to the cache so we don't have
        // to do this again. =)

        cache.Add(key, ret);
        return ret;
    }

    public void CalculateNeighbors()
    {
        foreach (Polygon poly in m_Polygons)
        {
            foreach (Polygon other_poly in m_Polygons)
            {
                if (poly == other_poly)
                    continue;

                if (poly.IsNeighborOf(other_poly))
                    poly.m_Neighbors.Add(other_poly);
            }
        }
    }

    public List<int> CloneVertices(List<int> old_verts)
    {
        List<int> new_verts = new List<int>();
        foreach (int old_vert in old_verts)
        {
            Vector3 cloned_vert = m_Vertices[old_vert];
            new_verts.Add(m_Vertices.Count);
            m_Vertices.Add(cloned_vert);
        }
        return new_verts;
    }

    public PolySet StitchPolys(PolySet polys)
    {
        PolySet stichedPolys = new PolySet();

        var edgeSet = polys.CreateEdgeSet();
        var originalVerts = edgeSet.GetUniqueVertices();

        var newVerts = CloneVertices(originalVerts);

        edgeSet.Split(originalVerts, newVerts);

        foreach (Edge edge in edgeSet)
        {
            // Create new polys along the stitched edge. These
            // will connect the original poly to its former
            // neighbor.

            var stitch_poly1 = new Polygon(edge.m_OuterVerts[0],
                                           edge.m_OuterVerts[1],
                                           edge.m_InnerVerts[0]);
            var stitch_poly2 = new Polygon(edge.m_OuterVerts[1],
                                           edge.m_InnerVerts[1],
                                           edge.m_InnerVerts[0]);
            // Add the new stitched faces as neighbors to
            // the original Polys.
            edge.m_InnerPoly.ReplaceNeighbor(edge.m_OuterPoly, stitch_poly2);
            edge.m_OuterPoly.ReplaceNeighbor(edge.m_InnerPoly, stitch_poly1);

            m_Polygons.Add(stitch_poly1);
            m_Polygons.Add(stitch_poly2);

            stichedPolys.Add(stitch_poly1);
            stichedPolys.Add(stitch_poly2);
        }

        //Swap to the new vertices on the inner polys.
        foreach (Polygon poly in polys)
        {
            for (int i = 0; i < 3; i++)
            {
                int vert_id = poly.m_Vertices[i];
                if (!originalVerts.Contains(vert_id))
                    continue;
                int vert_index = originalVerts.IndexOf(vert_id);
                poly.m_Vertices[i] = newVerts[vert_index];
            }
        }

        return stichedPolys;
    }

    public PolySet Extrude(PolySet polys, float height)
    {
        PolySet stitchedPolys = StitchPolys(polys);
        List<int> verts = polys.GetUniqueVertices();

        // Take each vertex in this list of polys, and push it
        // away from the center of the Planet by the height
        // parameter.

        foreach (int vert in verts)
        {
            Vector3 v = m_Vertices[vert];
            v = v.normalized * (v.magnitude + height);
            m_Vertices[vert] = v;
        }

        return stitchedPolys;
    }

    public PolySet Inset(PolySet polys, float interpolation)
    {
        PolySet stitchedPolys = StitchPolys(polys);
        List<int> verts = polys.GetUniqueVertices();

        //Calculate the average center of all the vertices
        //in these Polygons.

        Vector3 center = Vector3.zero;
        foreach (int vert in verts)
            center += m_Vertices[vert];
        center /= verts.Count;

        // Pull each vertex towards the center, then correct
        // it's height so that it's as far from the center of
        // the planet as it was before.

        foreach (int vert in verts)
        {
            Vector3 v = m_Vertices[vert];
            float height = v.magnitude;
            v = Vector3.Lerp(v, center, interpolation);
            v = v.normalized * height;
            m_Vertices[vert] = v;
        }

        return stitchedPolys;
    }

    public PolySet GetPolysInSphere(Vector3 center, float radius, IEnumerable<Polygon> source)
    {
        PolySet newSet = new PolySet();

        foreach (Polygon p in source)
        {
            foreach (int vertexIndex in p.m_Vertices)
            {
                float distanceToSphere = Vector3.Distance(center, m_Vertices[vertexIndex]);

                if (distanceToSphere <= radius)
                {
                    newSet.Add(p);
                    break;
                }
            }
        }

        return newSet;
    }

    public void GenerateMesh()
    {

        if (m_PlanetMesh)
            Destroy(m_PlanetMesh);

        m_PlanetMesh = new GameObject("Planet Mesh");


        MeshRenderer surfaceRenderer = m_PlanetMesh.AddComponent<MeshRenderer>();
        surfaceRenderer.material = m_Material;
        m_PlanetMesh.transform.position = new Vector3(5.0f, 10f, 25f);
        Vector3 newScale = transform.localScale;
        newScale *= 4;
        m_PlanetMesh.transform.localScale = newScale;
        m_PlanetMesh.AddComponent<RotatePlanet>();
        m_PlanetMesh.AddComponent<Rigidbody>();
        m_PlanetMesh.AddComponent<Mover>();
       

        Mesh terrainMesh = new Mesh();

        int vertexCount = m_Polygons.Count * 3;

        int[] indices = new int[vertexCount];

        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];
        Color32[] colors = new Color32[vertexCount];

        for (int i = 0; i < m_Polygons.Count; i++)
        {
            var poly = m_Polygons[i];

            indices[i * 3 + 0] = i * 3 + 0;
            indices[i * 3 + 1] = i * 3 + 1;
            indices[i * 3 + 2] = i * 3 + 2;

            vertices[i * 3 + 0] = m_Vertices[poly.m_Vertices[0]];
            vertices[i * 3 + 1] = m_Vertices[poly.m_Vertices[1]];
            vertices[i * 3 + 2] = m_Vertices[poly.m_Vertices[2]];

            colors[i * 3 + 0] = poly.m_Color;
            colors[i * 3 + 1] = poly.m_Color;
            colors[i * 3 + 2] = poly.m_Color;

            if (poly.m_SmoothNormals)
            {
                normals[i * 3 + 0] = m_Vertices[poly.m_Vertices[0]].normalized;
                normals[i * 3 + 1] = m_Vertices[poly.m_Vertices[1]].normalized;
                normals[i * 3 + 2] = m_Vertices[poly.m_Vertices[2]].normalized;
            }
            else
            {
                Vector3 ab = m_Vertices[poly.m_Vertices[1]] - m_Vertices[poly.m_Vertices[0]];
                Vector3 ac = m_Vertices[poly.m_Vertices[2]] - m_Vertices[poly.m_Vertices[0]];

                Vector3 normal = Vector3.Cross(ab, ac).normalized;

                normals[i * 3 + 0] = normal;
                normals[i * 3 + 1] = normal;
                normals[i * 3 + 2] = normal;
            }
        }

        terrainMesh.vertices = vertices;
        terrainMesh.normals = normals;
        terrainMesh.colors32 = colors;

        terrainMesh.SetTriangles(indices, 0);

        MeshFilter terrainFilter = m_PlanetMesh.AddComponent<MeshFilter>();
        terrainFilter.mesh = terrainMesh;

    }
}

