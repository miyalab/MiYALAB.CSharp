using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiYALAB.CSharp.Monitor
{
    /// <summary>
    /// 画像描画モニタを表示するフォームクラスです．
    /// </summary>
    public partial class GraphicMonitor : System.Windows.Forms.Form
    {
        private System.Windows.Forms.PictureBox pictureBox;

        /// <summary>
        /// 画像描画モニタを表示するフォームクラスです．
        /// </summary>
        GraphicMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画像描画モニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        public GraphicMonitor(int positionX, int positionY)
        {
            InitializeComponent();

            this.Show();
            ChangeLocationWindow(positionX, positionY);
        }

        /// <summary>
        /// 画像描画モニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        /// <param name="sizeX">変更後のウインドウの幅</param>
        /// <param name="sizeY">変更後のウインドウの高さ</param>
        public GraphicMonitor(int positionX, int positionY, int sizeX, int sizeY)
        {
            InitializeComponent();

            this.Show();
            ChangeLocationWindow(positionX, positionY);
            ChangeWindowSize(sizeX, sizeY);
        }

        /// <summary>
        /// デザイナーで自動作成されたコード
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(13, 13);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // GraphicMonitor
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GraphicMonitor";
            this.Text = "GraphicMonitor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// モニタのサイズを変更します．
        /// </summary>
        /// <param name="x">変更後のウインドウの幅</param>
        /// <param name="y">変更後のウインドウの高さ</param>
        public void ChangeWindowSize(int x, int y)
        {
            this.Size = new System.Drawing.Size(x, y);
        }

        /// <summary>
        /// モニタの表示位置を変更します．
        /// </summary>
        /// <param name="x">変更後のウインドウのx座標</param>
        /// <param name="y">変更後のウインドウのy座標</param>
        public void ChangeLocationWindow(int x, int y)
        {
            this.Location = new System.Drawing.Point(x, y);
        }

        /// <summary>
        /// 描画画像を取得します．
        /// </summary>
        /// <returns>描画画像</returns>
        public Bitmap GetGraphic()
        {
            return new Bitmap(pictureBox.Image);
        }

        /// <summary>
        /// 画像を描画します．
        /// </summary>
        /// <param name="bmp">描画画像</param>
        public void DrawGraphic(Bitmap bmp)
        {
            this.Location = new Point(bmp.Width + 24, bmp.Height + 24);
            pictureBox.Image = bmp;
        }

        /// <summary>
        /// 描画画像を削除します．
        /// </summary>
        public void Clear()
        {
            pictureBox.Image = null;
        }
    }
}
