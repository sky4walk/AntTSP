// André Betz
// http://www.andrebetz.de
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace AntTSP
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class AntTSP : System.Windows.Forms.Form, IAntTSP
	{
		private System.Windows.Forms.Button Start;
		private System.Windows.Forms.PictureBox pictureBox1;
		private Bitmap m_MyImage = null;
		private System.Windows.Forms.Button Clear;
		private ArrayList m_Knoten = new ArrayList();
		private ArrayList m_Edges = new ArrayList();
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label text1;
		private System.Windows.Forms.TextBox AdaptAlpha;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Beta;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox RohBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox CycleBox;
		private System.ComponentModel.Container components = null;
		private bool m_ThreadRun = false;
		private Thread m_CalculationThread;
		/// <summary>
		/// Change this settings for best one
		/// </summary>
		private float m_Alpha				= 0.75f; // 0.5 -> 1
		private float m_Beta				= 3.0f;	 // 1 -> 5
		private float m_Roh					= 0.9f;  // 0.3 -> 0.9
		private float m_MarkierungsStärke	= 0.5f;
		private System.Windows.Forms.Label Counter;
		private int   m_Cycles				= 30;


		public AntTSP()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			ClearBitmap();
			AdaptAlpha.Text = m_Alpha.ToString();
			Beta.Text = m_Beta.ToString();
			RohBox.Text = m_Roh.ToString();
			CycleBox.Text = m_Cycles.ToString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AntTSP));
			this.Start = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.Clear = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.text1 = new System.Windows.Forms.Label();
			this.AdaptAlpha = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Beta = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.RohBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.CycleBox = new System.Windows.Forms.TextBox();
			this.Counter = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Start
			// 
			this.Start.Location = new System.Drawing.Point(8, 344);
			this.Start.Name = "Start";
			this.Start.Size = new System.Drawing.Size(56, 23);
			this.Start.TabIndex = 0;
			this.Start.Text = "Start";
			this.Start.Click += new System.EventHandler(this.Start_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Location = new System.Drawing.Point(16, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(520, 312);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			// 
			// Clear
			// 
			this.Clear.Location = new System.Drawing.Point(64, 344);
			this.Clear.Name = "Clear";
			this.Clear.Size = new System.Drawing.Size(56, 23);
			this.Clear.TabIndex = 2;
			this.Clear.Text = "Clear";
			this.Clear.Click += new System.EventHandler(this.Clear_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(0, 296);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(8, 16);
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Visible = false;
			// 
			// text1
			// 
			this.text1.Location = new System.Drawing.Point(128, 344);
			this.text1.Name = "text1";
			this.text1.Size = new System.Drawing.Size(48, 23);
			this.text1.TabIndex = 4;
			this.text1.Text = "Alpha";
			// 
			// AdaptAlpha
			// 
			this.AdaptAlpha.Location = new System.Drawing.Point(160, 344);
			this.AdaptAlpha.Name = "AdaptAlpha";
			this.AdaptAlpha.Size = new System.Drawing.Size(40, 20);
			this.AdaptAlpha.TabIndex = 5;
			this.AdaptAlpha.Text = "";
			this.AdaptAlpha.TextChanged += new System.EventHandler(this.AdaptAlpha_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(208, 344);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "Beta";
			// 
			// Beta
			// 
			this.Beta.Location = new System.Drawing.Point(240, 344);
			this.Beta.Name = "Beta";
			this.Beta.Size = new System.Drawing.Size(48, 20);
			this.Beta.TabIndex = 7;
			this.Beta.Text = "";
			this.Beta.TextChanged += new System.EventHandler(this.Beta_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(288, 344);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 23);
			this.label2.TabIndex = 8;
			this.label2.Text = "Roh";
			// 
			// RohBox
			// 
			this.RohBox.Location = new System.Drawing.Point(320, 344);
			this.RohBox.Name = "RohBox";
			this.RohBox.Size = new System.Drawing.Size(48, 20);
			this.RohBox.TabIndex = 9;
			this.RohBox.Text = "";
			this.RohBox.TextChanged += new System.EventHandler(this.RohBox_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(368, 344);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 23);
			this.label3.TabIndex = 10;
			this.label3.Text = "Cycles";
			// 
			// CycleBox
			// 
			this.CycleBox.Location = new System.Drawing.Point(408, 344);
			this.CycleBox.Name = "CycleBox";
			this.CycleBox.Size = new System.Drawing.Size(48, 20);
			this.CycleBox.TabIndex = 11;
			this.CycleBox.Text = "";
			this.CycleBox.TextChanged += new System.EventHandler(this.CycleBox_TextChanged);
			// 
			// Counter
			// 
			this.Counter.Location = new System.Drawing.Point(480, 344);
			this.Counter.Name = "Counter";
			this.Counter.Size = new System.Drawing.Size(48, 16);
			this.Counter.TabIndex = 12;
			this.Counter.Text = "0";
			this.Counter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// AntTSP
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(562, 375);
			this.Controls.Add(this.Counter);
			this.Controls.Add(this.CycleBox);
			this.Controls.Add(this.RohBox);
			this.Controls.Add(this.Beta);
			this.Controls.Add(this.AdaptAlpha);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.text1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.Clear);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.Start);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AntTSP";
			this.Text = "AntTSP www.andrebetz.de";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.AntTSP_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new AntTSP());
		}

		private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			AddNode(e.X,e.Y);
			pictureBox1.Invalidate();
		}

		private void AddNode(int x, int y)
		{
			Node n = new Node(x,y,m_Knoten.Count);
			for(int i=0;i<m_Knoten.Count;i++)
			{
				Node nn = (Node)m_Knoten[i];
				Edge e = new Edge(n,nn);
				e.Weight = m_MarkierungsStärke;
				m_Edges.Add(e);
				DrawLine(e);
			}
			CopyPicIntoBmp(n);
			m_Knoten.Add(n);
		}

		private void Clear_Click(object sender, System.EventArgs e)
		{
			ClearAll();
		}
		private void DrawConnections()
		{
			for(int i=0;i<m_Edges.Count;i++)
			{
				Edge e = (Edge)m_Edges[i];
				DrawLine(e);
			}
		}
		private void DrawLine(Edge e)
		{
//			Pen blackPen = new Pen(Color.Black, e.Weight);
			Pen blackPen = new Pen(Color.Black, 1);
			Graphics offScreenDC = Graphics.FromImage(m_MyImage);
			offScreenDC.DrawLine(blackPen,new Point(e.NodeA.PosX,e.NodeA.PosY),new Point(e.NodeB.PosX,e.NodeB.PosY));
		}
		private void CopyPicIntoBmp(Node n)
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AntTSP));
			Bitmap bmp = new Bitmap(((System.Drawing.Image)(resources.GetObject("pictureBox2.Image"))));
			bmp.MakeTransparent(System.Drawing.Color.White);
			for(int sy=0;sy<bmp.Height;sy++)
			{
				for(int sx=0;sx<bmp.Width;sx++)
				{
					int relPosX = n.PosX+sx-bmp.Width/2;
					int relPosY = n.PosY+sy-bmp.Height/2;
					if(relPosX>0&&relPosX<m_MyImage.Width && relPosY>0 && relPosY<m_MyImage.Height)
					{
						m_MyImage.SetPixel(relPosX,relPosY,bmp.GetPixel(sx,sy));
					}
				}
			}
		}

		private void Start_Click(object sender, System.EventArgs e)
		{
			if(m_ThreadRun)
			{
				m_ThreadRun = false;
				Start.Text = "Start";
				Clear.Enabled = true;
			}
			else
			{
				m_ThreadRun = true;
				Clear.Enabled = false;

				for(int i=0;i<m_Knoten.Count;i++)
				{
					Node n = (Node)m_Knoten[i];
					ArrayList conn = GetConnectedEdges(n);
					n.Connected = conn;
				}

				Start.Text = "Stop";
				m_CalculationThread = new Thread(new ThreadStart(Run));
				m_CalculationThread.Name = "AntCycle";
				m_CalculationThread.Start();
			}
		}

		private void Run()
		{
			AntCycle ac = new AntCycle((IAntTSP)this,m_Alpha,m_Beta,m_Roh);	

			ArrayList SpeedRoute = null;

			for(int i=0;i<m_Cycles&&m_ThreadRun;i++)
			{
				Counter.Text = i.ToString();
				if(ac.Cycle() || !m_ThreadRun)
				{
					break;
				}

				SpeedRoute = (ArrayList)ac.SpeedRoute.Clone();
				lock(SpeedRoute)
				{
					DrawRoute(SpeedRoute);
				}
			}
			Start.Text = "Start";
			Clear.Enabled = true;
			m_ThreadRun = false;
		}

		private void DrawNodes()
		{
			for(int i=0;i<m_Knoten.Count;i++)
			{
				Node n = (Node)m_Knoten[i];
				CopyPicIntoBmp(n);
			}
		}
		private void ClearBitmap()
		{
			m_MyImage = new Bitmap(pictureBox1.Bounds.Width,pictureBox1.Bounds.Height);			
		}

		private void ClearAll()
		{
			ClearBitmap();
			m_Knoten.Clear();
			m_Edges.Clear();
			pictureBox1.Image = (Image) m_MyImage ;
			pictureBox1.Invalidate();
		}

		private ArrayList GetConnectedEdges(Node n)
		{
			ArrayList ConnEdges = new ArrayList();
			for(int i=0;i<m_Edges.Count;i++)
			{
				Edge e = (Edge)m_Edges[i];
				if(n.NodeNr==e.NodeA.NodeNr || n.NodeNr==e.NodeB.NodeNr)
				{
					ConnEdges.Add(e);
				}
			}
			return ConnEdges;
		}

		private void AdaptAlpha_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				m_Alpha = (float)Convert.ToDouble(AdaptAlpha.Text);
			}
			catch
			{
			}
		}

		private void Beta_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				m_Beta = (float)Convert.ToDouble(Beta.Text);
			}
			catch
			{
			}		
		}

		public void RedrawAll()
		{
			ClearBitmap();
			DrawConnections();
			DrawNodes();
		}

		public ArrayList Edges
		{
			get
			{
				return m_Edges;
			}
		}
		public ArrayList Nodes
		{
			get
			{
				return m_Knoten;
			}
		}
		public void DrawRoute(ArrayList route)
		{
			lock(m_MyImage)
			{
				ClearBitmap();
				for(int i=0;i<route.Count;i++)
				{
					Edge e = (Edge)route[i];
					DrawLine(e);
				}
				DrawNodes();
				pictureBox1.Image = (Image) m_MyImage ;
			}
			pictureBox1.Invalidate();
		}

		private void RohBox_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				m_Roh = (float)Convert.ToDouble(RohBox.Text);
			}
			catch
			{
			}	
		}

		private void CycleBox_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				m_Cycles = (int)Convert.ToInt32(CycleBox.Text);
			}
			catch
			{
			}	
		}

		private void AntTSP_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			lock(m_MyImage)
			{
				if(m_MyImage!=null)
				{
					pictureBox1.Image = (Image) m_MyImage ;
				}
			}
		}
	}
}
