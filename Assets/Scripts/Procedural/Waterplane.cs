using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Procedural
{
    public class Waterplane : MonoBehaviour
    {
        [SerializeField]
        private Rect dimensions;

        [SerializeField]
        private Vector2Int subdivisions;

        private void Start()
        {
            Debug.Assert(subdivisions.x >= 0);
            Debug.Assert(subdivisions.y >= 0);

            MeshGenerator meshGenerator = new MeshGenerator("Waterplane");

            int verticesX = subdivisions.x + 2;
            int verticesY = subdivisions.y + 2;

            float lengthX = dimensions.width / (verticesX - 1f);
            float lengthY = dimensions.height / (verticesY - 1f);

            for (int y = 0; y < verticesY; y++)
            {
                for (int x = 0; x < verticesX; x++)
                {
                    meshGenerator.AddVertex(new Vector3(dimensions.x + x * lengthX, dimensions.y + y * lengthY, 0), Vector3.left, new Vector2(x / (float)verticesX, y / (float)verticesY));
                }
            }

            for (int y = 0; y < verticesY - 1; y++)
            {
                for (int x = 0; x < verticesX - 1; x++)
                {
                    int upperIndex = x + y * verticesX;
                    int lowerIndex = x + verticesX + y * verticesX;
                    meshGenerator.AddQuad(upperIndex, upperIndex + 1, lowerIndex, lowerIndex + 1);
                }
            }

            GetComponent<MeshFilter>().mesh = meshGenerator.BuildMesh();
        }

        private void OnDisable()
        {
            // This could prevent a memory leak
            Destroy(GetComponent<MeshFilter>().mesh);
            GetComponent<MeshFilter>().mesh = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector2 center = dimensions.center;
            Vector2 size = dimensions.size;
            Gizmos.DrawWireCube(transform.position + new Vector3(center.x, 0, -center.y), new Vector3(size.x, 0, size.y));
        }
    }
}
