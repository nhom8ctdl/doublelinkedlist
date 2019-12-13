using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;

namespace DoAnCTDL
{
    public partial class LyThuyet : Form
    {
        private int Spd = 10;
        private PictureBox insert;
        private LinkedList<PictureBox> nodes = new LinkedList<PictureBox>();
        private int X = 0;
        private Point temp;
        private LinkedListNode<PictureBox> currentnode;
        private List<PictureBox> aaa = new List<PictureBox>();
        public LyThuyet()
        {
            InitializeComponent();
            Label a = new Label();
            comboBox1.SelectedIndex = 0;
        }
        private void Insert()
        {
            Label a = new Label();
            a.Size = new Size(100, 48);
            a.Text = textBox1.Text;
            a.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            a.TextAlign = ContentAlignment.MiddleCenter;
            a.BackColor = Color.Transparent;
            a.Name = "Value";
            PictureBox node = new PictureBox();
            node.Size = new Size(100, 48);
            node.SizeMode = PictureBoxSizeMode.StretchImage;
            node.Image = Properties.Resources.Untitled;
            node.Controls.Add(a);
            node.Location = new Point(10, 140);
            insert = node;
            this.Controls.Add(node);
        }
        private void AddTail()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Chưa nhập!", "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Insert();
                if (nodes.Count > 0)
                {
                    timer2.Start();
                }
                else
                {
                    timer1.Start();
                    nodes.AddLast(insert);
                }
            }
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = false;
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            insert.Top += Spd;
            if (insert.Top > 250 - Spd && insert.Top < 250 + Spd)
            {
                timer1.Stop();
                insert.Top = 250;
                foreach (var button in panel1.Controls.OfType<Button>())
                    button.Enabled = true;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (insert.Left > nodes.Last().Right + 50 - Spd && insert.Left < nodes.Last().Right + 50 + Spd &&
                insert.Top > 250 - Spd && insert.Top < 250 + Spd)
            {
                timer2.Stop();
                insert.Left = nodes.Last().Right + 50;
                insert.Top = 250;
                PictureBox arrow = new PictureBox();
                arrow.Location = new Point(nodes.Last().Right, nodes.Last().Top);
                arrow.Size = new Size(50, 48);
                arrow.BackColor = Color.Transparent;
                arrow.SizeMode = PictureBoxSizeMode.StretchImage;
                arrow.Image = Properties.Resources.two_way_arrows;
                Controls.Add(arrow);
                nodes.AddLast(arrow);
                nodes.AddLast(insert);
                foreach (var button in panel1.Controls.OfType<Button>())
                    button.Enabled = true;
                indexNum.Maximum = nodes.Count / 2;
            }
            else if (insert.Left > nodes.Last().Right + 50 - Spd && insert.Left < nodes.Last().Right + 50 + Spd)
            {
                insert.Top += Spd;
            }
            else
            {
                insert.Left += Spd;
            }
        }
        private void AddHead()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Chưa nhập!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Insert();
            if (nodes.Count > 0)
            {
                X = 0;
                timer3.Start();
            }
            else
            {
                timer1.Start();
                nodes.AddLast(insert);
            }
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            foreach (var temp in nodes)
                temp.Left += Spd;
            X += Spd;
            if (X == 150)
            {
                timer3.Stop();
                PictureBox arrow = new PictureBox();
                arrow.Location = new Point(110, 250);
                arrow.Size = new Size(50, 48);
                arrow.BackColor = Color.Transparent;
                arrow.SizeMode = PictureBoxSizeMode.StretchImage;
                arrow.Image = Properties.Resources.two_way_arrows;
                Controls.Add(arrow);
                nodes.AddFirst(arrow);
                nodes.AddFirst(insert);
                indexNum.Maximum = nodes.Count / 2;
                timer1.Start();
            }
        }

        private void DeleteHead()
        {
            if (nodes.Count > 1)
            {
                nodes.First().Dispose();
                nodes.First.Next.Value.Dispose();
                nodes.RemoveFirst();
                nodes.RemoveFirst();
                timer4.Start();
                foreach (var button in panel1.Controls.OfType<Button>())
                    button.Enabled = false;
            }
            else if (nodes.Count == 1)
            {
                nodes.First().Dispose();
                nodes.RemoveFirst();
            }
            else
                MessageBox.Show("Danh sách rỗng!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            foreach (var temp in nodes)
                temp.Left -= Spd;
            if (nodes.First().Left == 10)
            {
                foreach (var button in panel1.Controls.OfType<Button>())
                    button.Enabled = true;
                timer4.Stop();
            }
        }
        private void DeleteTail()
        {
            if (nodes.Count > 1)
            {
                nodes.Last().Dispose();
                nodes.Last.Previous.Value.Dispose();
                nodes.RemoveLast();
                nodes.RemoveLast();
            }
            else if (nodes.Count == 1)
            {
                nodes.Last().Dispose();
                nodes.RemoveLast();
            }
            else
                MessageBox.Show("Danh sách rỗng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void DeleteAtIndex()
        {
            if(nodes.Count==0)
            {
                MessageBox.Show("Danh sách rỗng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = false;
            int index = Convert.ToInt32(indexNum.Value);
            var current = nodes.First;
            for(int i = 0; i <= index; i++)
            {
                Thread.Sleep(200);
                Label lbl = current.Value.Controls.Find("Value", true).FirstOrDefault() as Label;
                lbl.BackColor = Color.FromArgb(235, 59, 90);
                this.Refresh();
                if (i == index)
                    break;
                Thread.Sleep(200);
                lbl.BackColor = Color.Transparent;
                current = current.Next.Next;
            }
            Thread.Sleep(200);
            if(index==0)
            {
                DeleteHead();
                return;
            }
            if(index== nodes.Count/2)
            {
                DeleteTail();
                return;
            }
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = false;
            current.Next.Value.Dispose();
            current.Value.Dispose();
            nodes.Remove(current.Next);
            nodes.Remove(current);
            temp = nodes.Last().Location;
            timer5.Start();
        }

        private void Search()
        {
            if (nodes.Count == 0)
            {
                MessageBox.Show("Danh sách rỗng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Chưa nhập!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = false;
            foreach (var temp in nodes)
            {
                Thread.Sleep(200);
                if (temp.Controls.Count == 0)
                    continue;
                Label lbl = temp.Controls.Find("Value", true).FirstOrDefault() as Label;
                lbl.BackColor = Color.FromArgb(235, 59, 90);
                if (lbl.Text == textBox1.Text)
                {
                    MessageBox.Show("Tìm thấy!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    lbl.BackColor = Color.Transparent;
                    foreach (var button in panel1.Controls.OfType<Button>())
                        button.Enabled = true;
                    return;
                }
                this.Refresh();
                Thread.Sleep(200);
                lbl.BackColor = Color.Transparent;
            }
            MessageBox.Show("Không tìm thấy!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = true;
        }

        private void Clear(object sender, EventArgs e)
        {
            foreach(var temp in nodes)
                temp.Dispose();
            nodes.Clear();
        }
        private void AddIndex()
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Chưa nhập!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (indexNum.Value == 0)
            {
                AddHead();
                return;
            }
            if(indexNum.Value== nodes.Count / 2 + 1)
            {
                AddTail();
                return;
            }
            Insert();
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = false;
            int index = Convert.ToInt32(indexNum.Value);
            currentnode = nodes.First;
            aaa.Clear();
            for(int i = 0; i < index; i++)
            {
                currentnode = currentnode.Next.Next;
            }
            LinkedListNode<PictureBox> tempnode = currentnode;
            while(true) 
            {
                aaa.Add(tempnode.Value);
                if (tempnode == nodes.Last)
                    break;
                tempnode = tempnode.Next;
            }
            temp = nodes.Last().Location;
            timer6.Start();
        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            var node = nodes.First;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (node.Next!=null)
                    if(node.Value.Right!=node.Next.Value.Left)
                    node.Next.Value.Left -= Spd;
                if (nodes.Last().Left == temp.X - 150)
                {
                    foreach (var button in panel1.Controls.OfType<Button>())
                        button.Enabled = true;
                    timer5.Stop();
                    return;
                }
                node = node.Next;
            }
        }

        private void DeleteByData()
        {
            if (nodes.Count == 0)
            {
                MessageBox.Show("Danh sách rỗng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Chưa nhập!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = false;
            var current = nodes.First;
            for (int i = 0; i <= nodes.Count/2; i++)
            {
                Thread.Sleep(200);
                Label lbl = current.Value.Controls.Find("Value", true).FirstOrDefault() as Label;
                lbl.BackColor = Color.FromArgb(235, 59, 90);
                this.Refresh();
                Thread.Sleep(200);
                lbl.BackColor = Color.Transparent;
                if (i == nodes.Count/2)
                {
                    if (lbl.Text == textBox1.Text)
                        break;
                    foreach (var button in panel1.Controls.OfType<Button>())
                        button.Enabled = true;
                    MessageBox.Show("Không tìm thấy!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lbl.Text == textBox1.Text)
                    break;
                current = current.Next.Next;
            }
            Thread.Sleep(200);
            if (current == nodes.First)
            {
                DeleteHead();
                foreach (var button in panel1.Controls.OfType<Button>())
                    button.Enabled = true;
                return;
            }
            if (current == nodes.Last)
            {
                DeleteTail();
                foreach (var button in panel1.Controls.OfType<Button>())
                    button.Enabled = true;
                return;
            }
            current.Next.Value.Dispose();
            current.Value.Dispose();
            nodes.Remove(current.Next);
            nodes.Remove(current);
            temp = nodes.Last().Location;
            timer5.Start();
            foreach (var button in panel1.Controls.OfType<Button>())
                button.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Enabled = true;
            indexNum.Maximum = nodes.Count/2;
            //indexNum.Minimum = 0;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    textBox1.Visible = true;
                    indexNum.Visible = false;
                    break;
                case 1:
                    textBox1.Visible = true;
                    indexNum.Visible = false;
                    break;
                case 2:
                    textBox1.Visible = true;
                    indexNum.Visible = true;
                    break;
                case 3:
                    textBox1.Visible = false;
                    indexNum.Visible = false;
                    break;
                case 4:
                    textBox1.Visible = false;
                    indexNum.Visible = false;
                    break;
                case 5:
                    textBox1.Visible = true;
                    textBox1.Enabled = false;
                    indexNum.Visible = true;
                    break;
                case 6:
                    textBox1.Visible = true;
                    indexNum.Visible = false;
                    break;
                case 7:
                    textBox1.Visible = true;
                    indexNum.Visible = false;
                    break;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    AddHead();
                    break;
                case 1:
                    AddTail();
                    break;
                case 2:
                    AddIndex();
                    break;
                case 3:
                    DeleteHead();
                    break;
                case 4:
                    DeleteTail();
                    break;
                case 5:
                    DeleteAtIndex();
                    textBox1.Enabled = true;
                    break;
                case 6:
                    Search();
                    break;
                case 7:
                    DeleteByData();
                    break;
            }
            indexNum.Maximum = nodes.Count / 2;
            textBox1.Text = ""; 
        }

        private void indexNum_ValueChanged(object sender, EventArgs e)
        {
            indexNum.Maximum = nodes.Count/2;
            if(comboBox1.SelectedIndex==2)
                indexNum.Maximum = nodes.Count / 2+1;
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            insert.Left += Spd;
            if (insert.Left == aaa.First().Left) 
            {
                timer6.Stop();
                Thread.Sleep(200);
                timer7.Start();
            }
        }
        private void timer7_Tick(object sender, EventArgs e)
        {
            foreach (var temp in aaa)
                temp.Left += Spd;
            if (nodes.Last().Left == temp.X + 150)
            {
                timer7.Stop();
                timer8.Start();
            }
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            insert.Top += Spd;
            if(insert.Top>250-Spd&&insert.Top<250+Spd)
            {
                insert.Top = 250;
                PictureBox arrow = new PictureBox();
                arrow.Location = new Point(insert.Right, 250);
                arrow.Size = new Size(50, 48);
                arrow.BackColor = Color.Transparent;
                arrow.SizeMode = PictureBoxSizeMode.StretchImage;
                arrow.Image = Properties.Resources.two_way_arrows;
                Controls.Add(arrow);
                nodes.AddBefore(currentnode, insert);
                nodes.AddBefore(currentnode, arrow);
                timer8.Stop();
                foreach (var button in panel1.Controls.OfType<Button>())
                    button.Enabled = true;
                indexNum.Maximum = nodes.Count / 2;
            }
        }
    }
}
