// André Betz
// http://www.andrebetz.de
using System;
using System.Collections;

namespace AntTSP
{
	/// <summary>
	/// Summary description for Ant.
	/// </summary>
	public class Ant
	{
		private Node m_StartNode = null;
		private float m_ShortestLength = -1;
		private float m_Length;
		private ArrayList m_TabuList = new ArrayList();
		private ArrayList m_EdgeList = new ArrayList();

		public Ant(Node StartNode)
		{
			m_StartNode = StartNode;
			SetToStart();
		}
		public float ShortestLength
		{
			get
			{
				return m_ShortestLength;
			}
		}
		public float Length
		{
			get
			{
				return m_Length;
			}
		}
		public ArrayList GetAntRoute
		{
			get
			{
				return m_EdgeList;
			}
		}
		public void SetToStart()
		{
			m_Length = 0;
			m_TabuList.Clear();
			m_EdgeList.Clear();
			m_TabuList.Add(m_StartNode);
		}
		public bool MoveToNextNode()
		{
			Node act = GetActualNode();
			Edge useEdge = null;
			Node connNode = null;
			float greatestWkt = 0;

			for(int i=0;i<act.Connected.Count;i++)
			{
				Edge e = (Edge)act.Connected[i];
				Node cn = GetConnectedNode(e,act);
				if(IsAllowed(cn))
				{
					if(e.EdgeWkt>greatestWkt)
					{
						greatestWkt = e.EdgeWkt;
						useEdge = e;
						connNode = cn;
					}
				}
			}
			if(useEdge!=null)
			{
				m_EdgeList.Add(useEdge);
				m_TabuList.Add(connNode);
				return true;
			}
			else
			{
				return false;
			}
		}

		public Node GetActualNode()
		{
			int count = m_TabuList.Count;
			if(count>0)
			{
				return (Node)m_TabuList[count-1];
			}
			else
			{
				return null;
			}
		}

		private Node GetConnectedNode(Edge e,Node n)
		{
			if(e.NodeA.NodeNr == n.NodeNr)
				return e.NodeB;
			else if(e.NodeB.NodeNr == n.NodeNr)
				return e.NodeA;
			else
				return null;
		}

		private bool IsAllowed(Node n)
		{
			for(int i=0;i<m_TabuList.Count;i++)
			{
				Node VisitedNode = (Node)m_TabuList[i];
				if(VisitedNode.NodeNr==n.NodeNr)
				{
					return false;
				}
			}
			return true;
		}

		public void CalcAntRoute()
		{
			float RouteDist = 0;
			for(int j=0;j<m_EdgeList.Count;j++)
			{
				Edge e = (Edge)m_EdgeList[j];
				RouteDist += e.Distance;
			}
			m_Length = RouteDist;
			if(m_ShortestLength<0)
			{
				m_ShortestLength = m_Length;
			}
			else
			{
				if(m_Length<m_ShortestLength)
				{
					m_ShortestLength = m_Length;
				}
			}
		}

		public void UpDateEdges(float ConstQ)
		{
			for(int i=0;i<m_EdgeList.Count;i++)
			{
				Edge e = (Edge)m_EdgeList[i];
				e.DeltaWeight += ConstQ / m_Length;
			}
		}
	}
}
