using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Mesh CreateBeam(float startWidth, float endWidth, float length, float distance)
        {
            Mesh mesh = new Mesh
            {
                name = "Light Beam",
                vertices = new Vector3[]
                {
                    new Vector3(startWidth / 2, 0, distance),        // BL
                    new Vector3(-startWidth / 2, 0, distance),       // BR
                    new Vector3(endWidth / 2, 0, distance + length), // TL
                    new Vector3(-endWidth / 2, 0, distance + length) // TR
                },
                uv = new Vector2[]
                {
                    new Vector2(0, 0), // BL
                    new Vector2(1, 0), // BR
                    new Vector2(0, 1), // TL
                    new Vector2(1, 1)  // TR
                },
                triangles = new int[]
                {
                    0, 1, 2, // Triangle A
                    1, 3, 2  // Triangle B
                }
            };

            return mesh;
        }

        public void Start()
        {
            if (meshRenderer == null)
                meshRenderer = GetComponent<MeshRenderer>();
            Debug.Assert(meshRenderer != null, "No mesh renderer assigned", this);

            if (mesh == null)
                mesh = CreateBeam(lightBeamConfiguration.StartWidth, lightBeamConfiguration.EndWidth, lightBeamConfiguration.Length, lightBeamConfiguration.Distance);
            MeshFilter meshFilter = meshRenderer.GetComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;
            meshRenderer.sharedMaterial = beamMaterial;
        }
    }
}
