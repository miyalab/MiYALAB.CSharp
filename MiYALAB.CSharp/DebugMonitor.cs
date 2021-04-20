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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiYALAB.CSharp.Monitor
{
    /// <summary>
    /// デバッグモニタを表示するフォームクラスです．
    /// </summary>
    public partial class DebugMonitor : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox textBoxDebug;

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです．
        /// </summary>
        public DebugMonitor()
        {
            InitializeComponent();

            this.Show();
        }

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="text">タイトルテキスト</param>
        public DebugMonitor(string text)
        {
            InitializeComponent();

            this.Text = text;
        }

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="text">タイトルテキスト</param>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        public DebugMonitor(string text, int positionX, int positionY)
        {
            InitializeComponent();

            this.Text = text;
            this.Show();
            ChangeLocationWindow(positionX, positionY);
        }

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        public DebugMonitor(int positionX, int positionY)
        {
            InitializeComponent();
            
            this.Show();
            ChangeLocationWindow(positionX, positionY);
        }

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="text">タイトルテキスト</param>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        /// <param name="sizeX">変更後のウインドウの幅</param>
        /// <param name="sizeY">変更後のウインドウの高さ</param>
        public DebugMonitor(string text, int positionX, int positionY, int sizeX, int sizeY)
        {
            InitializeComponent();

            this.Text = text;
            this.Show();
            ChangeLocationWindow(positionX, positionY);
            ChangeWindowSize(sizeX, sizeY);
        }

        /// <summary>
        /// デバッグモニタを表示するフォームクラスです．
        /// </summary>
        /// <param name="positionX">変更後のウインドウのx座標</param>
        /// <param name="positionY">変更後のウインドウのy座標</param>
        /// <param name="sizeX">変更後のウインドウの幅</param>
        /// <param name="sizeY">変更後のウインドウの高さ</param>
        public DebugMonitor(int positionX, int positionY, int sizeX, int sizeY)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugMonitor));
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.Location = new System.Drawing.Point(12, 12);
            this.textBoxDebug.Multiline = true;
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDebug.Size = new System.Drawing.Size(260, 237);
            this.textBoxDebug.TabIndex = 0;
            // 
            // DebugMonitor
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxDebug);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DebugMonitor";
            this.Text = "デバッグモニタ";
            this.SizeChanged += new System.EventHandler(this.DebugMonitor_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// フォームサイズ変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebugMonitor_SizeChanged(object sender, EventArgs e)
        {
            this.textBoxDebug.Size = new System.Drawing.Size(this.ClientSize.Width - 24, this.ClientSize.Height - 24);
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
        /// デバッグモニタにテキストを挿入します．
        /// </summary>
        /// <param name="text">挿入テキスト</param>
        public void Write(string text)
        {
            this.textBoxDebug.AppendText(text);
        }

        /// <summary>
        /// デバッグモニタに改行付きでテキストを挿入します．
        /// </summary>
        /// <param name="text">挿入テキスト</param>
        public void WriteLine(string text)
        {
            this.textBoxDebug.AppendText(text + Environment.NewLine);
        }
        
        /// <summary>
        /// デバッグモニタのテキストを消去します．
        /// </summary>
        public void Clear()
        {
            this.textBoxDebug.Text = "";
        }
    }
}
