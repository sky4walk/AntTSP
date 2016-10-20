// André Betz
// http://www.andrebetz.de
using System;
using System.Collections;

namespace AntTSP
{
	/// <summary>
	/// Summary description for Edge.
	/// </summary>
	public class Edge
	{
		private Node m_Node1 = null;
		private Node m_Node2 = null;
		private float m_Weight = 0;
		private float m_DeltaWeight = 0;
		private float m_Distance = 0;
		private float m_EdgeWkt = 0;

		public Edge(Node n1,Node n2)
		{
			m_Node1 = n1;
			m_Node2 = n2;
			CalculateDistance();
		}

		private void CalculateDistance()
		{
			double dX = m_Node1.PosX - m_Node2.PosX;
			double dY = m_Node1.PosY - m_Node2.PosY;
			m_Distance = (float)Math.Sqrt(dX*dX + dY*dY);
		}

		public float EdgeWkt
		{
			get
			{
				return m_EdgeWkt;
			}
			set
			{
				m_EdgeWkt = value;
			}
		}
		public float Distance
		{
			get
			{
				return m_Distance;
			}
		}

		public float Weight
		{
			get
			{
				return m_Weight;
			}
			set
			{
				m_Weight = value;
			}
		}

		public float DeltaWeight
		{
			get
			{
				return m_DeltaWeight;
			}
			set
			{
				m_DeltaWeight = value;
			}
		}

		public Node NodeA
		{
			get
			{
				return m_Node1;
			}
		}
		public Node NodeB
		{
			get
			{
				return m_Node2;
			}
		}
	}
}
