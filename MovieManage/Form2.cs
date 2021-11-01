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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            dataGridView1.DataSource = DataManager.Movies;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            button1.Click += (sender, e) => //등록 버튼
            {
                try
                {
                    if(DataManager.Movies.Exists((x)=>x.MovNum == textBox1.Text))
                    {
                        MessageBox.Show("이미 존재하는 영화 입니다.");
                    }
                    else
                    {
                        Movie movie = new Movie()
                        {
                            MovNum = textBox1.Text,
                            Name = textBox2.Text,
                            Publisher = textBox3.Text,
                            RunTime = textBox4.Text,
                        };
                        DataManager.Movies.Add(movie);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Movies;
                        DataManager.Save();
                    }
                }
                catch(Exception exception)
                {

                }
            };

            button2.Click += (sender, e) => // 변경 버튼
            {
                try
                {
                    Movie movie = DataManager.Movies.Single((x) => x.MovNum == textBox1.Text);
                    movie.Name = textBox2.Text;
                    movie.Publisher = textBox3.Text;
                    movie.RunTime = textBox4.Text;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Movies;
                    DataManager.Save();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 영화입니다.");
                }
            };

            button3.Click += (sender, e) => // 삭제 버튼
            {
                try
                {
                    Movie movie = DataManager.Movies.Single((x) => x.MovNum == textBox1.Text);
                    DataManager.Movies.Remove(movie);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Movies;
                    DataManager.Save();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 영화입니다.");
                }
            };
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                //선택시 자동으로 텍스트박스에 글자가 선택되어 있게 하는 거
                Movie movie = dataGridView1.CurrentRow.DataBoundItem as Movie;
                textBox1.Text = movie.MovNum;
                textBox2.Text = movie.Name;
                textBox3.Text = movie.Publisher;
                textBox4.Text = movie.RunTime;
            }
            catch(Exception exception)
            {

            }
        }
    }
}
