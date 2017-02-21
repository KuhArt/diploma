using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//
using System.Drawing.Imaging;
//
using System.IO;
//
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InputPostfixRegex
{
    public partial class FormDiagramm : Form
    {
        public List<TreeChart> listTreeChart    = null;
        public TabControl tabControlDiagram     = null;

        public FormDiagramm()
        {
            InitializeComponent();
            //
            tabControlDiagram = tabControl1;
            tabControlDiagram.TabPages.Clear();       //Clear
            listTreeChart = new List<TreeChart>();  //Clear

        }

        //private void FormDiagramm_Paint(object sender, PaintEventArgs e)
        //{

        //    if (this.listTreeChart == null)
        //    {
        //        this.Text = "this.treeChart == null";
        //        return;
        //    }
        //    //-------this.treeChart != null -----
        //    TreeChart.g = e.Graphics;
        //    Rectangle clientRec =this.ClientRectangle;

        //    //1)==== Get <<atPoint>> for <<startVertex>> of Diagram
        //    Point atPoint = new Point(20, 20+ clientRec.Top);

        //    Font fontSymbol = new Font(FontFamily.GenericMonospace, 14.0F, FontStyle.Bold);
        //    SizeF sizeFDiaName = TreeChart.g.MeasureString(TreeChart.DiagramName, fontSymbol);

        //    //2)----- Draw <<DiagramName>>
        //    TreeChart.g.DrawString(TreeChart.DiagramName, fontSymbol, Brushes.Red,
        //        atPoint.X , treeChart.rectSize.Height + 50);

        //    //3)----- Draw <<RegExp>> for Diagram
        //    TreeChart.g.DrawString(treeChart.RegExp, fontSymbol, Brushes.Black,
        //        atPoint.X, treeChart.rectSize.Height + 80);

        //    //4) =========Draw  Diagram as <<treeChart>> starting at <<atPoint>> =========
        //    treeChart.DrawTree(atPoint);

        //    //5.0)====Draw <<startVertex>> and  its  nVertex == 0
        //    Point startVertex = new Point(atPoint.X, atPoint.Y + treeChart.rectSize.Height / 2);
        //    TreeChart.drawVertex(startVertex, Brushes.Red );

        //    //------nVertex == 0
        //    TreeChart.drawNumberVertex(startVertex, 0);

        //    //5.1)====Draw <<endVertex>> and  its  nVertex + 1 and input Arrow
        //    Point endVertex = new Point(atPoint.X + treeChart.rectSize.Width , startVertex.Y);
        //    TreeChart.drawVertex(endVertex, Brushes.Red);
        //    //------nVertex
        //    TreeChart.drawNumberVertex(endVertex, TreeChart.nVertex);
        //    //------input Arrow
        //    TreeChart.drawArrow(new Point(endVertex.X - TreeChart.radiusVertex, endVertex.Y));
        //    // nVertex+1 -- number vertexes!!!


        //}

        public void currTabPage_Paint(object sender, PaintEventArgs e)
        {
            //throw new NotImplementedException();

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            TabPage tabPage = sender as TabPage;
            Graphics g = e.Graphics;
            Rectangle clientRec = tabPage.ClientRectangle;
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{
            Point atPoint = new Point(20, 20 + clientRec.Top);

            ////////////////////////////////////////////////
            int indexTabPage = tabControlDiagram.TabPages.IndexOf(tabPage);
            TreeChart treeChart = listTreeChart[indexTabPage];

            //4) =========Draw  Diagram as <<treeChart>> starting at <<atPoint>> =========
            treeChart.g = g;//Only for ROOT

            int nVertex = 1;

            treeChart.DrawTree(atPoint,ref nVertex);

            //5.0)====Draw <<startVertex>> and  its  nVertex == 0

            Point startVertex = new Point(atPoint.X, atPoint.Y + treeChart.rectSize.Height / 2);
            treeChart.drawVertex(startVertex, Brushes.Red);

            //------nVertex == 0

            treeChart.drawNumberVertex(startVertex, 0);

            //5.1)====Draw <<endVertex>> and  its  nVertex + 1 and input Arrow
            Point endVertex = new Point(atPoint.X + treeChart.rectSize.Width, startVertex.Y);

            treeChart.drawVertex(endVertex, Brushes.Red);
            //------nVertex
            //nVertex++;
            treeChart.drawNumberVertex(endVertex, nVertex);
            //------input Arrow
            treeChart.drawArrow(new Point(endVertex.X - TreeChart.radiusVertex, endVertex.Y));
            // nVertex+1 -- number vertexes!!!

//////////////////////////////////////////////////////////

            Font fontSymbol1 = new Font(FontFamily.GenericMonospace, 14.0F, FontStyle.Bold);
            SizeF sizeFDiaName = g.MeasureString(treeChart.DiagramName, fontSymbol1);

            //2)----- Draw <<DiagramName>>
            g.DrawString(treeChart.DiagramName, fontSymbol1, Brushes.Red,
                atPoint.X, treeChart.rectSize.Height + 50);

            //3)----- Draw <<RegExp>> for Diagram
            g.DrawString(treeChart.RegExp, fontSymbol1, Brushes.Black,
                atPoint.X, treeChart.rectSize.Height + 80);

            //4)----- Draw <<namedPostfixExp>> for Diagram

            string str_polish = TreeChart.PolishListToString(treeChart.namedPostfixExp.arrPolish);

            if (str_polish != "")
            {
                g.DrawString("Polish Expression:", fontSymbol1, Brushes.Black,
                    atPoint.X, treeChart.rectSize.Height + 120);
                g.DrawString(str_polish, fontSymbol1, Brushes.Black,
                    atPoint.X, treeChart.rectSize.Height + 140);
            }
            //}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}

            Font fontSymbol2 = new Font(FontFamily.GenericMonospace, 10.0F, FontStyle.Bold);
            SizeF size_tabPage_Text = g.MeasureString(tabPage.Text, fontSymbol2);

            //2)----- Draw <<tabPage.Text>>
            g.DrawString(tabPage.Text, fontSymbol2, Brushes.Green,
                atPoint.X, clientRec.Height - 50);


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //sender as ToolStripButton

            string wmfSelectedTab = tabControlDiagram.SelectedTab.Text + ".wmf";
            MessageBox.Show(wmfSelectedTab);

            //int indexCurrent = tabControlDiagram.SelectedIndex;
            //MessageBox.Show(string.Format("Curr:{0}",indexCurrent));

            string currDir = Directory.GetCurrentDirectory();
            string pathWMF = currDir + "\\" + wmfSelectedTab;
            if (File.Exists(pathWMF))
            {
                MessageBox.Show("Exists, not deleting\n" + pathWMF);
                //File.Delete(wmfSelectedTab);//pathWMF
                return;
            }
            else
            {
                MessageBox.Show("Saving\n"+pathWMF);

            };

            //Draw Metafile: tabControlDiagram.SelectedTab.Text + ".wmf"

            Metafile curMetafile = null;

            Graphics gSelTab = tabControlDiagram.SelectedTab.CreateGraphics();

            IntPtr hdc = gSelTab.GetHdc();

            try
            {

                //    path63 = Directory.GetCurrentDirectory();
                //path63 = path63 + "\\" + "File1163.wmf";
                curMetafile = new Metafile(wmfSelectedTab, hdc);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                gSelTab.ReleaseHdc(hdc);
                gSelTab.Dispose();
                return;
            }

            // Create a Graphics object from the Metafile object
            Graphics g = Graphics.FromImage(curMetafile);

            //Draw metafile like figure on  TabPage tabPage=tabControlDiagram.SelectedTab
            //================

            //throw new NotImplementedException();
            ////////TabPage tabPage = sender as TabPage;
            TabPage tabPage = tabControlDiagram.SelectedTab;
            ///////Graphics g = e.Graphics;
            //
            Rectangle clientRec = tabPage.ClientRectangle;

            Point atPoint = new Point(20, 20 + clientRec.Top);

            ////////////////////////////////////////////////
            //------indexTabPage <---> treeChart
            int indexTabPage = tabControlDiagram.TabPages.IndexOf(tabPage);
            TreeChart treeChart = listTreeChart[indexTabPage];

            //4) =========Draw  Diagram as <<treeChart>> starting at <<atPoint>> =========
            treeChart.g = g;//Only for ROOT
            int nVertex = 1;
            treeChart.DrawTree(atPoint,ref nVertex );

            //5.0)====Draw <<startVertex>> and  its  nVertex == 0

            Point startVertex = new Point(atPoint.X, atPoint.Y + treeChart.rectSize.Height / 2);
            treeChart.drawVertex(startVertex, Brushes.Red);

            //------nVertex == 0
            treeChart.drawNumberVertex(startVertex, 0);

            //5.1)====Draw <<endVertex>> and  its  nVertex + 1 and input Arrow
            Point endVertex = new Point(atPoint.X + treeChart.rectSize.Width, startVertex.Y);
            treeChart.drawVertex(endVertex, Brushes.Red);
            //------nVertex
            treeChart.drawNumberVertex(endVertex, nVertex);
            //------input Arrow
            treeChart.drawArrow(new Point(endVertex.X - TreeChart.radiusVertex, endVertex.Y));
            // nVertex+1 -- number vertexes!!!

            //////////////////////////////////////////////////////////

            Font fontSymbol1 = new Font(FontFamily.GenericMonospace, 14.0F, FontStyle.Bold);
            SizeF sizeFDiaName = g.MeasureString(treeChart.DiagramName, fontSymbol1);

            //2)----- Draw <<DiagramName>>
            g.DrawString(treeChart.DiagramName, fontSymbol1, Brushes.Red,
                atPoint.X, treeChart.rectSize.Height + 50);

            //3)----- Draw <<RegExp>> for Diagram
            g.DrawString(treeChart.RegExp, fontSymbol1, Brushes.Black,
                atPoint.X, treeChart.rectSize.Height + 80);

            ////////////////////////////////////////////////

            Font fontSymbol2 = new Font(FontFamily.GenericMonospace, 10.0F, FontStyle.Bold);
            SizeF size_tabPage_Text = g.MeasureString(tabPage.Text, fontSymbol2);

            //2)----- Draw <<tabPage.Text>>
            g.DrawString(tabPage.Text, fontSymbol2, Brushes.Green,
                atPoint.X, clientRec.Height - 50);

            //================

            gSelTab.ReleaseHdc(hdc);
            g.Dispose();
            gSelTab.Dispose();


        }

        private void toolStripButton1_MouseEnter(object sender, EventArgs e)
        {
            (sender as ToolStripButton).ToolTipText = "Save the diagramm as "
                + tabControlDiagram.SelectedTab.Text + ".wmf";
        }
    }
}
