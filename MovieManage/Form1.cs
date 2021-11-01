using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieManage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label4.Text = DataManager.Movies.Count.ToString();
            label5.Text = DataManager.Users.Count.ToString();
            label6.Text = DataManager.Movies.Where((x) => x.IsBooked).Count().ToString();

            //데이타 그리드 1이랑 2 연결하기
            dataGridView1.DataSource = DataManager.Movies;
            dataGridView2.DataSource = DataManager.Users;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;
            dataGridView2.CurrentCellChanged += dataGridView2_CurrentCellChanged;

            
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Movie movie = dataGridView1.CurrentRow.DataBoundItem as Movie;
                textBox1.Text = movie.MovNum;
                textBox2.Text = movie.Name;
            }
            catch(Exception exception)
            {

            }
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User book = dataGridView2.CurrentRow.DataBoundItem as User;
                textBox3.Text = book.Id.ToString();
            }
            catch(Exception exception)
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("영화 식별번호를 입력해주세요!");
            }else if (textBox3.Text.Trim()=="")
            {
                MessageBox.Show("사용자 ID를 입력하세요!");
            }
            else
            {
                try
                {
                    Movie movie = DataManager.Movies.Single((x) => x.MovNum == textBox1.Text);
                    if (movie.IsBooked)
                    {
                        MessageBox.Show(" 이미 예약된 좌석 입니다.");
                    }
                    else
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox3.Text);
                        movie.UserId = user.Id;
                        movie.UserName = user.Name;
                        movie.IsBooked = true;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Movies;
                        DataManager.Save();

                        MessageBox.Show("\"" + movie.Name + "\"이/가\"" + user.Name + "\"님에게 예매 되었습니다.");
                    }
                }
                catch(Exception exception)
                {
                    MessageBox.Show(" 존재하지 않는 영화 또는 사용자 입니다!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("좌석번호를 입력해주세요");
            }
            else
            {
                try
                {
                    Movie movie = DataManager.Movies.Single((x) => x.MovNum == textBox1.Text);
                    if (movie.IsBooked)
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == movie.UserId.ToString());
                        movie.UserId = 0;
                        movie.UserName = "";
                        movie.IsBooked = false;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Movies;
                        DataManager.Save();

                        MessageBox.Show("\""+movie.Name+"\"의 예매가 취소되었습니다!");
                    }
                    else
                    {
                        MessageBox.Show("예매중이 아닙니다.");
                    }
                }
                catch(Exception exception)
                {
                    MessageBox.Show("존재하지 않는 영화 또는 사용자 입니다.");
                }
            }
        }

        private void 영ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog(); // 메뉴 눌러서 폼2 띄우기
        }

        private void 관객관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog(); // 메뉴 눌러서 폼3 띄우기
        }
    }
}
