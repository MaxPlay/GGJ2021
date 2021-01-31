using GGJ.Procedural;
using UnityEngine;

namespace GGJ.Gameplay
{
    public class LightBeam : MonoBehaviour
    {
        private static Mesh mesh;

        private void OnApplicationQuit()
        {
            if (mesh != null)
            {
                Destroy(mesh);
                mesh = null;
            }
        }

        [SerializeField]
        [Tooltip("If this field has nothing assigned, the component tries to get a mesh renderer from the Game Object")]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private Material beamMaterial;

        [SerializeField]
        private LightBeamConfiguration lightBeamConfiguration;

        struct TrapezoidEdge
        {
            public float StartX;
            public float EdgeLength;

            public TrapezoidEdge(float width, int vertexCount)
            {
                EdgeLength = width / vertexCount;
                StartX = -width / 2;
            }
        }

        public Mesh CreateBeam(float startWidth, float endWidth, int subdivisions, float length, float distance)
        {
            MeshGenerator meshGenerator = new MeshGenerator("Light Beam");

            int verticesX = subdivisions + 2;

            TrapezoidEdge upperEdge = new TrapezoidEdge(endWidth, verticesX);
            TrapezoidEdge lowerEdge = new TrapezoidEdge(startWidth, verticesX);

            for (int x = 0; x < verticesX; x++)
            {
                meshGenerator.AddVertex(new Vector3(lowerEdge.StartX + x * lowerEdge.EdgeLength, 0, distance), Vector3.up, new Vector2(x / (float)(verticesX - 1), 0));
            }

            for (int x = 0; x < verticesX; x++)
            {
                meshGenerator.AddVertex(new Vector3(upperEdge.StartX + x * upperEdge.EdgeLength, 0, distance + length), Vector3.up, new Vector2(x / (float)(verticesX - 1), 1));
            }

            for (int x = 0; x < verticesX - 1; x++)
            {
                meshGenerator.AddQuad(x, x + 1, x + verticesX, x + 1 + verticesX);
            }

            return meshGenerator.BuildMesh();
        }

        public void Start()
        {
            if (meshRenderer == null)
                meshRenderer = GetComponent<MeshRenderer>();
            Debug.Assert(meshRenderer != null, "No mesh renderer assigned", this);

            if (mesh == null)
                mesh = CreateBeam(
                    lightBeamConfiguration.StartWidth,
                    lightBeamConfiguration.EndWidth,
                    lightBeamConfiguration.Subdivisions,
                    lightBeamConfiguration.Length,
                    lightBeamConfiguration.Distance);
            MeshFilter meshFilter = meshRenderer.GetComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;
            meshRenderer.sharedMaterial = beamMaterial;
        }
    }
}
