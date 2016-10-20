// André Betz
// http://www.andrebetz.de
using System;
using System.Collections;

namespace AntTSP
{
	/// <summary>
	/// Summary description for AntCycle.
	/// </summary>
	public class AntCycle
	{
		private float m_Alpha = 0;
		private float m_Beta = 0;
		private float m_Roh = 0;
		private float ConstQ = 100;
		private float m_ShortestPath = -1;
		private int m_FastestAnt = 0;
		private IAntTSP m_iftcAntTsp = null;
		private ArrayList m_Ants = new ArrayList();
		private ArrayList m_SpeedRoute = null;

		public AntCycle(IAntTSP iftcAntTsp, float Alpha, float Beta, float Roh)
		{
			m_Alpha = Alpha;
			m_Beta = Beta;
			m_Roh = Roh;
			m_iftcAntTsp = iftcAntTsp;
			for(int i=0;i<iftcAntTsp.Nodes.Count;i++)
			{
				m_Ants.Add(new Ant((Node)iftcAntTsp.Nodes[i]));
			}
		}

		public ArrayList SpeedRoute
		{
			get
			{
				lock(m_SpeedRoute)
				{
					return m_SpeedRoute;
				}
			}
		}
		public bool Cycle()
		{
			CalcEdgesWkt();

			for(int i=0;i<m_Ants.Count;i++)
			{
				Ant a = (Ant)m_Ants[i];
				a.SetToStart();
			}

			for(int j=0;j<m_iftcAntTsp.Nodes.Count-1;j++)
			{
				for(int i=0;i<m_Ants.Count;i++)
				{
					Ant a = (Ant)m_Ants[i];
					a.MoveToNextNode();
				}
			}

			for(int i=0;i<m_Ants.Count;i++)
			{
				Ant a = (Ant)m_Ants[i];
				a.CalcAntRoute();
				a.UpDateEdges(ConstQ);
			}

			UpdateEdgeWeights();

			Ant Speedy = FindFastestAnt();
			m_SpeedRoute = Speedy.GetAntRoute;

			return TestEnd();
		}

		private bool TestEnd()
		{
			bool End = true;
			for(int i=1;i<m_Ants.Count;i++)
			{
				Ant a = (Ant)m_Ants[i-1];
				Ant b = (Ant)m_Ants[i];
				if((int)(a.Length*10)!=(int)(b.Length*10))
				{
					End = false;
					break;
				}
			}
			return End;
		}

		private Ant FindFastestAnt()
		{
			int AntNr = 0;
			float WaLen = 0;
			for(int i=0;i<m_Ants.Count;i++)
			{
				Ant a = (Ant)m_Ants[i];
				a.CalcAntRoute();
				float tmpRoute = a.Length;
				if(i>0)
				{
					if(tmpRoute<WaLen)
					{
						WaLen = tmpRoute;
						AntNr = i;
					}
				}
				else
				{
					WaLen = tmpRoute;
					AntNr = i;
				}
			}

			if(m_ShortestPath<0)
			{
				m_ShortestPath = WaLen;
			}
			else
			{
				if(WaLen<m_ShortestPath)
				{
					m_ShortestPath = WaLen;
					m_FastestAnt = AntNr;
				}
			}
			return (Ant)m_Ants[m_FastestAnt];
		}

		private float CalcEdgeSum()
		{
			float Sum = 0;
			for(int i=0;i<m_iftcAntTsp.Edges.Count;i++)
			{
				Edge e = (Edge)m_iftcAntTsp.Edges[i];
				Sum += (float)(	Math.Pow((double)e.Weight,  (double)m_Alpha) / 
					Math.Pow((double)e.Distance,(double)m_Beta));
			}
			return Sum;
		}

		private void CalcEdgesWkt()
		{
			float Sum = CalcEdgeSum();

			for(int i=0;i<m_iftcAntTsp.Edges.Count;i++)
			{
				Edge e = (Edge)m_iftcAntTsp.Edges[i];
				float tmpWkt =	(float)(Math.Pow((double)e.Weight,(double)m_Alpha) / Math.Pow((double)e.Distance,(double)m_Beta) / Sum);
				e.EdgeWkt = tmpWkt;
			}
		}
		private void UpdateEdgeWeights()
		{
			for(int i=0;i<m_iftcAntTsp.Edges.Count;i++)
			{
				Edge e = (Edge)m_iftcAntTsp.Edges[i];
				e.Weight = e.Weight * m_Roh + e.DeltaWeight;
				e.DeltaWeight = 0;
			}
		}
	}
}
