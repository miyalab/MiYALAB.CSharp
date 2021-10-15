/*
 * MIT License
 * 
 * Copyright (c) 2020 MiYA LAB(K.Miyauchi)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;

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
        public GraphicMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画像描画モニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="text">タイトルテキスト</param>
        public GraphicMonitor(string text)
        {
            InitializeComponent();

            this.Text = text;
        }

        /// <summary>
        /// 画像描画モニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="text">タイトルテキスト</param>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        public GraphicMonitor(string text, int positionX, int positionY)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(positionX, positionY);

            InitializeComponent();

            this.Text = text;
        }

        /// <summary>
        /// 画像描画モニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        public GraphicMonitor(int positionX, int positionY)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(positionX, positionY);

            InitializeComponent();
        }

        /// <summary>
        /// 画像描画モニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="text">タイトルテキスト</param>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        /// <param name="sizeX">変更後のウインドウの幅</param>
        /// <param name="sizeY">変更後のウインドウの高さ</param>
        public GraphicMonitor(string text, int positionX, int positionY, int sizeX, int sizeY)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(positionX, positionY);
            
            InitializeComponent();

            ChangeWindowSize(sizeX, sizeY);

            this.Text = text;
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
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(positionX, positionY);

            InitializeComponent();

            ChangeWindowSize(sizeX, sizeY);
        }

        /// <summary>
        /// デザイナーで自動作成されたコード
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicMonitor));
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
            this.pictureBox.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // GraphicMonitor
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GraphicMonitor";
            this.Text = "GraphicMonitor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //----------------------------------------------------------------------------------
        // 以下、イベント処理
        //----------------------------------------------------------------------------------
        /// <summary>
        /// pictureBoxのサイズが変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(pictureBox.Width + 24, pictureBox.Height + 24);
        }

        /// <summary>
        /// PictureBoxクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void pictureBox_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            // フォルダ作成
            if (!Directory.Exists(this.Text))
            {
                Directory.CreateDirectory(this.Text);
            }

            DateTime dt = DateTime.Now;
            string fileName = this.Text + "/"
                + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString()
                + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + ".jpg";

            pictureBox.Image.Save(fileName, ImageFormat.Jpeg);
        }

        //----------------------------------------------------------------------------------
        // 以下、自作関数（イベント処理ではない）
        //----------------------------------------------------------------------------------
        /// <summary>
        /// モニタのサイズを変更します．
        /// </summary>
        /// <param name="x">変更後のウインドウの幅</param>
        /// <param name="y">変更後のウインドウの高さ</param>
        public void ChangeWindowSize(int x, int y)
        {
            this.ClientSize = new System.Drawing.Size(x, y);
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
            this.ClientSize = new System.Drawing.Size(bmp.Width + 24, bmp.Height + 24);
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
