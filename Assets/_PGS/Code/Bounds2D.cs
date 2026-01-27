using UnityEngine;

namespace PGS
{
	/// <summary>
	/// a 2D (XZ-Axis) rectangle that defines a boundary of something
	/// </summary>
    [System.Serializable]
    public struct Bounds2D
    {
		[SerializeField] private Rect m_Bounds;
		public float XMin { get { return m_Bounds.x; } }
		public float XMax { get { return m_Bounds.size.x; } }
		public float ZMin { get { return m_Bounds.y; } }
		public float ZMax { get { return m_Bounds.size.y; } }

		public Vector3 Center
		{
			get { return new Vector3(m_Bounds.center.x, 0f, m_Bounds.center.y); }
		}

		/// <summary>
		/// Is the give point within the bounds on the XZ-axis? (Ignores Y position)
		/// </summary>
		/// <param name="point">The position of the point to test</param>
		/// <returns></returns>
		public bool IsPointWithinBounds(Vector3 point)
		{
			return point.x >= XMin && point.x <= XMax && point.z >= ZMin && point.z <= ZMax;
		}

		/// <summary>
		/// Given a point in space, returns a clamped version that does not exceed the bounds
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public Vector3 ClampToBounds(Vector3 point)
		{
			float clampX = Mathf.Clamp(point.x, XMin, XMax);
			float clampZ = Mathf.Clamp(point.z, ZMin, ZMax);
			Vector3 clamped = new Vector3(clampX, point.y, clampZ);

			Debug.Log($"Intended Position: {point} | Clamped Position: {clamped}");
			return clamped;
		}

		public override string ToString()
		{
			return $"X: {XMin} > {XMax}\nZ: {ZMin} > {ZMax}";
		}
	}
}
