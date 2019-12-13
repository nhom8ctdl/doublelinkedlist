using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DoAnCTDL
{
    public partial class UngDung : Form
    {
        private LinkedList<Bitmap> picList = new LinkedList<Bitmap>();
        private LinkedListNode<Bitmap> current;
        public UngDung() 
        {
            InitializeComponent();
            this.KeyPreview = true;
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(NextButton, "Next");
            toolTip1.SetToolTip(PrevButton, "Previous");
            toolTip1.SetToolTip(Insert, "Insert");
            toolTip1.SetToolTip(Delete, "Delete");
            Picture.Controls.Add(NextButton);
            Picture.Controls.Add(PrevButton);
            picList.AddLast(Properties.Resources.pic2);
            picList.AddLast(Properties.Resources.pic1);
            picList.AddLast(Properties.Resources.pic3);
            picList.AddLast(Properties.Resources.pic4);
            picList.AddLast(Properties.Resources.pic5);
            current = picList.First;
            Picture.Image = current.Value;
            Loading();
        }
        private int CountIndex(LinkedList<Bitmap> a, LinkedListNode<Bitmap> b)
        {
            int count=1;
            LinkedListNode <Bitmap> temp = a.First;
            while(true)
            {
                if (temp == b)
                    break;
                count++;
                temp = temp.Next;
            }
            return count;
        }
        private void Loading()
        {
            if (picList.Count == 0)
            {
                Picture.SizeMode = PictureBoxSizeMode.CenterImage;
                Picture.Image = Properties.Resources.no_image;
                NextButton.Visible = false;
                PrevButton.Visible = false;
                number.Text = "0/0";
                return;
            }
            NextButton.Visible = true;
            PrevButton.Visible = true;
            number.Text = $"{CountIndex(picList,current)}/{picList.Count}";
            Picture.Image = current.Value;
            if ((current.Value.Width == Picture.Width && current.Value.Height == Picture.Height)
                || current.Value.Width > Picture.Width || current.Value.Height > Picture.Height)
                Picture.SizeMode = PictureBoxSizeMode.Zoom;
            else
                Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            if (current.Next == null)
                NextButton.Visible=false;
            if (current.Previous == null)
                PrevButton.Visible = false;
            Refresh();
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            current = current.Next;
            Loading();
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            current = current.Previous;
            Loading();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (picList.Count == 0)
                return;
            if (picList.Count == 1)
            {
                current.Value.Dispose();
                picList.Clear();
                Loading();
                return;
            }
            if (current.Next != null)
            {
                current = current.Next;
                current.Previous.Value.Dispose();
                picList.Remove(current.Previous);
            }
            else
            {
                current = current.Previous;
                current.Next.Value.Dispose();
                picList.Remove(current.Next);
            }
            Loading();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.jpg; *.jpeg; *.gif; *.bmp;*.jfif";
                dlg.Title = "Open an Image file";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                foreach (string file in dlg.FileNames)
                {
                    try
                    {
                        picList.AddLast(new Bitmap(file));
                        current = picList.Last;
                    }
                    catch (Exception)
                    {
                        string path = file.Split('\\').Last();
                        MessageBox.Show($"Failed loading image: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        path = null;
                        Loading();
                    }
                }
                Loading();
            }
        }
        private void UngDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && PrevButton.Visible == true)
                PrevButton.PerformClick();
            if (e.KeyCode == Keys.Right && NextButton.Visible == true)
                NextButton.PerformClick();
        }
    }
}
