using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class SignUp : Form
    {
        bool flag;//判断是否更改用户权限
        Login login;
        UserManagement mangement;
        public SignUp()
        {
            InitializeComponent();
        }
        public SignUp(UserManagement um, DataGridViewRow dr, bool f) //f to tell whether it's admin or user
        {
            this.mangement = um;
            InitializeComponent();
            this.txtID.ReadOnly = true;
            this.lblShade.Hide();
            this.txtID.Text = dr.Cells[0].Value.ToString();
            this.txtPwd.Text = dr.Cells[1].Value.ToString();

            flag = f;
            //MessageBox.Show(flag.ToString());
        }
        public SignUp(Login login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            radManager.Checked = false;
            radReader.Checked = true;
            txtID.Focus();
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Image\signUp.png");
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
            if (login != null)
            {
                login.Show();
            }
            if (mangement != null)
            {
                mangement.Show();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //if(radReader.Checked == true)// && flag == 1)//如果用户类型被改变，且最终是读者类型
           // {
            if(login != null)
            {
                int result = userInfo.addUser(txtID.Text, txtPwd.Text);
                if (result == -2)
                {
                    MessageBox.Show("该用户已存在");
                }
                else
                {
                    MessageBox.Show("注册成功！");
                }
                this.Close(); 
                login.Show();
            }
            else
            {
                if(!flag && radManager.Checked) // 把用户改为管理员
                {
                    int result = userInfo.addAdmin(txtID.Text, txtPwd.Text);
                    if(result == -2)
                    {
                        MessageBox.Show("该管理员已存在");
                    }
                    else
                    {
                        MessageBox.Show("更改限权成功");
                    }
                    this.Close();
                    mangement.Show();
                }
                else if(flag && radReader.Checked)
                {
                    int result = userInfo.addUser(txtID.Text, txtPwd.Text);
                    userInfo.deleteUser(txtID.Text, "admin");
                    if (result == -2)
                    {
                        MessageBox.Show("该管理员已存在");
                    }
                    else
                    {
                        MessageBox.Show("更改限权成功");
                    }
                    this.Close();
                    mangement.Show();
                }
                else
                {
                    string type;
                    if (radReader.Checked == true)
                    {
                        type = "reader";
                    }
                    else
                    {
                        type = "admin";
                    }
                    userInfo.updateUser(txtID.Text, txtPwd.Text, type);
                    this.Close();
                    mangement.Show();
                    MessageBox.Show("更改成功");
                }
            }
                
                //MessageBox.Show(result.ToString());
            //}
            //else if (radManager.Checked == true)//如果用户类型被改变，且最终是管理员类型
            //{
            //    MessageBox.Show("1234");

            //    int result = userInfo.addAdmin(txtID.Text, txtPwd.Text);
            //    if (result == -2)
            //    {
            //        MessageBox.Show("该管理员已存在");
            //    }
            //    //MessageBox.Show(result.ToString());


            //}
            //else
            //{//如果用户类型没有被改变，更新用户数据
            //    string type;
            //    if(radReader.Checked==true){
            //        type = "reader";
            //    }
            //    else
            //    {
            //        type = "admin";
            //    }
            //    userInfo.updateUser(txtID.Text, txtPwd.Text, type);
            //    //MessageBox.Show("12323");

            //}
            
        }
    }
}
