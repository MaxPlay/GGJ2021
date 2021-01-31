using UnityEngine;

namespace GGJ.Gameplay
{
    [CreateAssetMenu(menuName = "GGJ/Light Beam Configuration")]
    public class LightBeamConfiguration : ScriptableObject
    {
        public float StartWidth;

        public float EndWidth;

        public int Subdivisions;

        public float Distance;

        public float Length;
    }
}