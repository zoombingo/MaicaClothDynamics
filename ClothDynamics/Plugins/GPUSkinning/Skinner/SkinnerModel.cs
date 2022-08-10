using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ClothDynamics
{
    /// A serializable class holding a preprocessed vertex array.
    public class SkinnerModel : ScriptableObject
    {
        // This is basically a skinned mesh object without topological
        // infomation. The vertex array in the mesh is optimized for Skinner
        // renderers (e.g. overlapped vertices removal).

        #region Public properties

        /// Number of vertices (read only).
        public int vertexCount
        {
            get { return _vertexCount; }
        }

        [SerializeField] int _vertexCount;

        /// Preprocessed vertex array as a mesh (read only).
        public Mesh mesh
        {
            get { return _mesh; }
        }

        [SerializeField] Mesh _mesh;

        [SerializeField]
        public List<int> _mapVertsBack;

        #endregion

        #region Public methods



        /// Asset initialization
        public void Initialize(Mesh source)
        {
            // Input vertices
            var inVertices = source.vertices;
            var inNormals = source.normals;
            var inTangents = source.tangents;
            var inBoneWeights = source.boneWeights;

            // Enumerate unique vertices.
            var outVertices = new List<Vector3>();
            var outNormals = new List<Vector3>();
            var outTangents = new List<Vector4>();
            var outBoneWeights = new List<BoneWeight>();

            int globali = 0;
            int offset = 0;

            var dictVertsIndex = new Dictionary<Vector3, int>();
            _mapVertsBack = new List<int>();

            for (int i = 0; i < inVertices.Length; i++)
            {
                if (dictVertsIndex.TryGetValue(inVertices[i], out int index))
                {
                    _mapVertsBack.Add(index);
                    offset++;
                }
                else
                //if (!outVertices.Any(_ => _ == inVertices[i]))
                {
                    dictVertsIndex.Add(inVertices[i], outVertices.Count);
                    outVertices.Add(inVertices[i]);
                    outNormals.Add(inNormals[i]);
                    outTangents.Add(inTangents[i]);
                    outBoneWeights.Add(inBoneWeights[i]);
                    _mapVertsBack.Add(globali - offset);
                }
                globali++;
            }

            // Assign unique UVs to the vertices.
            var outUVs = Enumerable.Range(0, outVertices.Count).Select(i => Vector2.right * (i + 0.5f) / outVertices.Count).ToList();

            // Enumerate vertex indices.
            var indices = Enumerable.Range(0, outVertices.Count).ToArray();

            // Make a clone of the source mesh to avoid
            // the SMR internal caching problem - https://goo.gl/mORHCR
            _mesh = Instantiate<Mesh>(source);
            _mesh.name = _mesh.name.Substring(0, _mesh.name.Length - 7);

            // Clear the unused attributes.
            _mesh.colors = null;
            _mesh.uv2 = null;
            _mesh.uv3 = null;
            _mesh.uv4 = null;

            // Overwrite the vertices.
            _mesh.subMeshCount = 0;
            _mesh.SetVertices(outVertices);
            _mesh.SetNormals(outNormals);
            _mesh.SetTangents(outTangents);
            _mesh.SetUVs(0, outUVs);
            _mesh.bindposes = source.bindposes;
            _mesh.boneWeights = outBoneWeights.ToArray();

            // Add point primitives.
            _mesh.subMeshCount = 1;
            _mesh.SetIndices(indices, MeshTopology.Points, 0);

            // Finishing up.
            _mesh.UploadMeshData(true);

            // Store the vertex count.
            _vertexCount = outVertices.Count;
        }



        #endregion

        #region ScriptableObject functions

        void OnEnable()
        {
        }

        #endregion
    }
}
