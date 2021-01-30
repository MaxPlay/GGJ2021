using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Procedural
{
    public class MeshGenerator
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();
        private string name;

        public MeshGenerator(string name) => this.name = name;

        public void AddVertex(Vector3 position, Vector3 normal, Vector2 uvCoordinate)
        {
            vertices.Add(position);
            normals.Add(normal);
            uv.Add(uvCoordinate);
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangles.Add(a);
            triangles.Add(b);
            triangles.Add(c);
        }

        public void AddQuad(int a, int b, int c, int d)
        {
            AddTriangle(a, b, c);
            AddTriangle(b, d, c);
        }

        public Mesh BuildMesh()
        {
            return new Mesh
            {
                name = name,
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
                normals = normals.ToArray(),
                uv = uv.ToArray()
            };
        }
    }
}
