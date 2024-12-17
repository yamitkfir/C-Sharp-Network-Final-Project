using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Media.Imaging;

namespace ServerProject
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
        public int ImageControl(string imgLoc, Socket sender)
        {
            Uri uriSource = new Uri(imgLoc);
            BitmapImage image = new BitmapImage(uriSource);
            JpegBitmapEncoder JpegEncoder = new JpegBitmapEncoder();
            JpegEncoder.Frames.Add(BitmapFrame.Create(image));
            MemoryStream MS = new MemoryStream();
            JpegEncoder.Save(MS);
            byte[] Data = MS.ToArray();
            // read to end
            byte[] bmpBytes = MS.ToArray();
            MS.Close();
            int total = SendVarData(sender, bmpBytes);
            return total;
        }
        private static int SendVarData(Socket s, byte[] data)
        {
            /*
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            s.Send(Encoding.ASCII.GetBytes("OK" + " <EOF>"));

            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = s.Send(datasize);

            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            return total;
            */

            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            string data2;

            s.Send(Encoding.ASCII.GetBytes("OK" + " <EOF>"));
            Thread.Sleep(100);

            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = s.Send(datasize);

            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                Thread.Sleep(100);
                total += sent;
                dataleft -= sent;
            }
            return total;
        }

    }
}