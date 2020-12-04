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

/// <summary>
/// MiYALABで公開しているC#用ライブラリです。
/// </summary>
namespace MiYALAB.CSharp
{
    /// <summary>
    /// グラフモニタを表示するフォームクラスです。
    /// </summary>
    public partial class GraphMonitor : System.Windows.Forms.Form
    {
        // グラフデザイン用
        private string textTitle = "グラフタイトル";
        private string textAxisX = "X軸";
        private string textAxisY = "Y軸";
        private int positionTextTile = 0;
        private int positionTextAxisX = 0;
        private int positionTextAxisY = 0;

        // フォームコンポーネント
        private System.Windows.Forms.PictureBox pictureBoxTextAxisX;
        private System.Windows.Forms.PictureBox pictureBoxTextAxisY;
        private System.Windows.Forms.PictureBox pictureBoxTextGraphTitle;
        private System.Windows.Forms.PictureBox pictureBoxGraph;

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです。
        /// </summary>
        public GraphMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです。
        /// </summary>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        public GraphMonitor(int positionX, int positionY)
        {
            InitializeComponent();

            MoveWindow(positionX, positionY);
        }

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです。
        /// </summary>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        /// <param name="sizeX">変更後のウインドウの幅</param>
        /// <param name="sizeY">変更後のウインドウの高さ</param>
        public GraphMonitor(int positionX, int positionY, int sizeX, int sizeY)
        {
            InitializeComponent();

            MoveWindow(positionX, positionY);
            ChangeWindowSize(sizeX, sizeY);
        }

        /// <summary>
        /// デザイナーで自動作成されたコード
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
            this.pictureBoxTextAxisX = new System.Windows.Forms.PictureBox();
            this.pictureBoxTextAxisY = new System.Windows.Forms.PictureBox();
            this.pictureBoxTextGraphTitle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextAxisX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextAxisY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextGraphTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGraph
            // 
            this.pictureBoxGraph.Location = new System.Drawing.Point(61, 12);
            this.pictureBoxGraph.Name = "pictureBoxGraph";
            this.pictureBoxGraph.Size = new System.Drawing.Size(263, 202);
            this.pictureBoxGraph.TabIndex = 0;
            this.pictureBoxGraph.TabStop = false;
            // 
            // pictureBoxTextAxisX
            // 
            this.pictureBoxTextAxisX.Location = new System.Drawing.Point(118, 220);
            this.pictureBoxTextAxisX.Name = "pictureBoxTextAxisX";
            this.pictureBoxTextAxisX.Size = new System.Drawing.Size(149, 26);
            this.pictureBoxTextAxisX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxTextAxisX.TabIndex = 1;
            this.pictureBoxTextAxisX.TabStop = false;
            // 
            // pictureBoxTextAxisY
            // 
            this.pictureBoxTextAxisY.Location = new System.Drawing.Point(12, 52);
            this.pictureBoxTextAxisY.Name = "pictureBoxTextAxisY";
            this.pictureBoxTextAxisY.Size = new System.Drawing.Size(43, 134);
            this.pictureBoxTextAxisY.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxTextAxisY.TabIndex = 2;
            this.pictureBoxTextAxisY.TabStop = false;
            // 
            // pictureBoxTextGraphTitle
            // 
            this.pictureBoxTextGraphTitle.Location = new System.Drawing.Point(95, 252);
            this.pictureBoxTextGraphTitle.Name = "pictureBoxTextGraphTitle";
            this.pictureBoxTextGraphTitle.Size = new System.Drawing.Size(149, 26);
            this.pictureBoxTextGraphTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxTextGraphTitle.TabIndex = 3;
            this.pictureBoxTextGraphTitle.TabStop = false;
            // 
            // GraphMonitor
            // 
            this.ClientSize = new System.Drawing.Size(336, 290);
            this.Controls.Add(this.pictureBoxTextGraphTitle);
            this.Controls.Add(this.pictureBoxTextAxisY);
            this.Controls.Add(this.pictureBoxTextAxisX);
            this.Controls.Add(this.pictureBoxGraph);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GraphMonitor";
            this.Text = "グラフモニタ";
            this.Load += new System.EventHandler(this.GraphMonitor_Load);
            this.SizeChanged += new System.EventHandler(this.GraphMonitor_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextAxisX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextAxisY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTextGraphTitle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// フォーム読み込み時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphMonitor_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// フォームサイズ変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphMonitor_SizeChanged(object sender, EventArgs e)
        {
            this.pictureBoxGraph.Size = new System.Drawing.Size(this.ClientSize.Width - 24, this.ClientSize.Height - 24);
        }

        /// <summary>
        /// デバッグモニタのサイズを変更します。
        /// </summary>
        /// <param name="x">変更後のウインドウの幅</param>
        /// <param name="y">変更後のウインドウの高さ</param>
        public void ChangeWindowSize(int x, int y)
        {
            this.Size = new System.Drawing.Size(x, y);
        }

        /// <summary>
        /// デバッグモニタの表示位置を変更します。
        /// </summary>
        /// <param name="x">変更後のウインドウのx座標</param>
        /// <param name="y">変更後のウインドウのy座標</param>
        public void MoveWindow(int x, int y)
        {
            this.Location = new System.Drawing.Point(x, y);
        }

        public void ProtPoint(double x, double y)
        {

        }

        public void SetTextAxisX(string text)
        {

        }

        public void SetTextAxisY(string text)
        {

        }

        public void SetTexGraphTitle(string text)
        {

        }

        public void ChangeGraphSize(int xpx, int ypx)
        {

        }

        public void ChangeTextSizeTitle(double point)
        {

        }

        public void ChangeTextSizeAxisX(double point)
        {

        }

        public void ChangeTextSizeAxisY(double point)
        {

        }

        public void ShowTextTitle()
        {
            pictureBoxTextGraphTitle.Visible = true;
        }

        public void ShowTextAxisX()
        {
            pictureBoxTextAxisX.Visible = true;
        }

        public void ShowTextAxisY()
        {
            pictureBoxTextAxisY.Visible = true;
        }

        public void HideTextTitle()
        {
            pictureBoxTextGraphTitle.Visible = false;
        }

        public void HideTextAxisX()
        {
            pictureBoxTextAxisX.Visible = false;
        }

        public void HideTextAxisY()
        {
            pictureBoxTextAxisY.Visible = false;
        }
    }
}
