// André Betz
// http://www.andrebetz.de
using System;
using System.Collections;

namespace AntTSP
{
	/// <summary>
	/// Summary description for Node.
	/// </summary>
	public class Node
	{
		private int m_PosX;
		private int m_PosY;
		private int m_NodeNr;
		private ArrayList m_Connected = null;
		public ArrayList Connected
		{
			get
			{
				return m_Connected;
			}
			set
			{
				m_Connected = value;
			}
		}
		public int PosX
		{
			get
			{
				return m_PosX;
			}
		}
		public int PosY
		{
			get
			{
				return m_PosY;
			}
		}
		public int NodeNr
		{
			get
			{
				return m_NodeNr;
			}
		}
		public Node(int PosX,int PosY,int NodeNr)
		{
			m_PosY = PosY;
			m_PosX = PosX;
			m_NodeNr = NodeNr;
		}
	}
}
