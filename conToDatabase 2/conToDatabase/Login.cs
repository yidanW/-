using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace conToDatabase
{
    
    public partial class Login : Form
    {
        private Account account = new Account();
        public static String connetStr = "server=127.0.0.1;port=3306;" +
            "user=root;password=1101; database=pesys;";
        MySqlConnection conn = new MySqlConnection(connetStr);
        public Login()
        {
            InitializeComponent();
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句

                MySqlCommand command = conn.CreateCommand();
                //初始化tables
                command.CommandText = "create table if not exists Person (IdCard numeric(20)," +
                    "Name varchar(20),Type varchar(20),TeamAccount numeric(10),primary key(IdCard));" +
                    "create table if not exists Account(AccountNum numeric(10),pwd varchar(20),AccountType varchar(20),primary key(AccountNum));"
                    + "create table if not exists Athlete(IdCard numeric(20),AthNumber numeric(10)," +
                    "Age numeric(3),EduScore numeric(3),Gender varchar(10),AccountNum numeric(10),primary key(IdCard)," +
                    "foreign key(IdCard) references Person(IdCard) on delete cascade,foreign key(AccountNum) references Account(AccountNum) on delete cascade);" +
                    "create table if not exists Leader(IdCard numeric(20),Tel numeric(20),AccountNum numeric(10)," +
                    "primary key(IdCard),foreign key(IdCard) references Person(IdCard) on delete cascade," +
                    "foreign key(AccountNum) references Account(AccountNum) on delete cascade);" +
                    "create table if not exists Doctor(IdCard numeric(20),Tel numeric(20),primary key(IdCard)," +
                    "foreign key(IdCard) references Person(IdCard) on delete cascade); " +
                    "create table if not exists Coach(IdCard numeric(20),Tel numeric(20),Gender varchar(10)," +
                    "AccountNum numeric(10),primary key(IdCard),foreign key(IdCard) references Person(IdCard) on delete cascade," +
                    "foreign key(AccountNum) references Account(AccountNum) on delete cascade);"+
                    "create table if not exists Referee(IdCard numeric(20),Tel numeric(20)," +
                    "AccountNum numeric(10),primary key(IdCard),foreign key(IdCard) references Person(IdCard) on delete cascade," +
                    "foreign key(AccountNum) references Account(AccountNum) on delete cascade);"+
                    "create table if not exists Matches(MatchId numeric(4),MatchName varchar(20),Gender varchar(10)," +
                    "primary key(MatchId)); "+
                    "create table if not exists Ath_Mat(IdCard numeric(20),MatchId numeric(4),Score1 numeric(2)," +
                    "Score2 numeric(2),Score3 numeric(2),Score4 numeric(2),Score5 numeric(2),D numeric(2),P numeric(2)," +
                    "FinalScore numeric(4),primary key(IdCard, MatchId),foreign key(IdCard) references Athlete(IdCard) on delete cascade," +
                    "foreign key(MatchId) references Matches(MatchId) on delete cascade);"+
                    "create table if not exists Team(AccountNum numeric(10),TeamName varchar(20),Attachment varchar(20)," +
                    "primary key(AccountNum),foreign key(AccountNum) references Account(AccountNum) on delete cascade);";
                command.ExecuteNonQuery();
                //初始化代表队信息
                command.CommandText = "replace into account values('1001','123','team');"
                + "replace into team(accountNum,teamName) values ('1001','深圳中学');"
                +"replace into account values('1002','123','team');"
                + "replace into team(accountNum,teamName) values ('1002','深圳高级中学');"
                +"replace into account values('1003','123','team');"
                + "replace into team(accountNum,teamName) values ('1003','深圳实验中学');"
                +"replace into account values('1004','123','team');"
                + "replace into team(accountNum,teamName) values ('1004','深圳外国语中学');";
                command.ExecuteNonQuery();

                //初始化账号信息
                command.CommandText = "replace into account values('1','123','admin');" +
                    "replace into account values('201','123','controller');";
                command.ExecuteNonQuery();

                //初始化比赛信息
                command.CommandText = "replace into matches values('101','单杠','male');" +
                    "replace into matches values('102','双杠','male');" +
                    "replace into matches values('103','吊环','male');" +
                    "replace into matches values('104','跳马','male');" +
                    "replace into matches values('105','自由体操','male');" +
                    "replace into matches values('106','鞍马','male');" +
                    "replace into matches values('107','蹦床','male');" +
                    "replace into matches values('201','跳马','female');" +
                    "replace into matches values('202','高低杠','female');" +
                    "replace into matches values('203','平衡木','female');" +
                    "replace into matches values('204','自由体操','female');" +
                    "replace into matches values('205','蹦床','female');";
                command.ExecuteNonQuery();

                //初始化队员信息
                command.CommandText = "replace into Person values" +
                    "('1741920751','王江','athlete','1001');" +
                    "replace into athlete (Idcard,age,eduscore,gender) " +
                    "values('1741920751','12','88','female');"+
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('1741920751',201);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('1741920751',203);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('1741920751',202);" +
                    "replace into Person values" +
                    "('2340272151','张才出','athlete','1001');" +
                    "replace into athlete (Idcard,age,eduscore,gender) " +
                    "values('2340272151','10','77','female');" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('2340272151',202);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('2340272151',203);" +
                    "replace into Person values" +
                    "('81259026001','何熙','athlete','1001');" +
                    "replace into athlete (Idcard,age,eduscore,gender) " +
                    "values('81259026001','12','82','female');" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('81259026001',205);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('81259026001',201);" +
                    "replace into Person values" +
                    "('901257269581','江盐','athlete','1001');" +
                    "replace into athlete (Idcard,age,eduscore,gender) " +
                    "values('901257269581','8','82','male');" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('901257269581',101);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('901257269581',102);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('901257269581',107);" +
                    "replace into Person values" +
                    "('843562723','文非','athlete','1001');" +
                    "replace into athlete (Idcard,age,eduscore,gender) " +
                    "values('843562723','9','92','male');" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('843562723',103);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('843562723',102);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('843562723',104);" +
                    "replace into Person values" +
                    "('219145217','孙诺','athlete','1002');" +
                    "replace into athlete (Idcard,age,eduscore,gender) " +
                    "values('219145217','12','92','female');" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('219145217',203);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('219145217',202);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('219145217',205);" +
                    "replace into Person values" +
                    "('56284100028','王建中','athlete','1002');" +
                    "replace into athlete (Idcard,age,eduscore,gender) " +
                    "values('56284100028','12','92','male');" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('56284100028',103);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('56284100028',106);" +
                    "replace into ath_mat (Idcard,matchid) " +
                    "values ('56284100028',102);";
                command.ExecuteNonQuery();


                //初始化其他人员信息
                command.CommandText = "replace into Person values('891471992'" +
                    $",'张飞','referee','1001');" +
                    "replace into referee(idcard,tel) values('891471992'" +
                        $",'26821250');" +
                    "replace into Person values('91514981'" +
                    $",'洪江','leader','1001');" +
                    "replace into leader(idcard,tel) values('91514981'" +
                        $",'2682981250');" +
                    "replace into Person values('467687142'" +
                    $",'陈天','doctor','1001');" +
                    "replace into doctor(idcard,tel) values('467687142'" +
                        $",'2146518250');" +
                    "replace into Person values('12349274'" +
                    $",'郝勇','coach','1001');" +
                    "replace into coach(idcard,tel,gender) values('12349274'" +
                            $",'9186173542','female');" +
                    "replace into Person values('78861250'" +
                    $",'尹邦国','referee','1002');" +
                    "replace into referee(idcard,tel) values('78861250'" +
                        $",'096451250');" +
                    "replace into Person values('2614789157'" +
                    $",'徐千','leader','1002');" +
                    "replace into leader(idcard,tel) values ('2614789157'" +
                        $",'44216567980');" +
                    "replace into Person values('1515322535'" +
                    $",'文艺书','doctor','1002');" +
                    "replace into doctor(idcard,tel) values('467687142'" +
                        $",'2146518250');" +
                    "replace into Person values('91851398'" +
                    $",'张良','coach','1002');" +
                    "replace into coach(idcard,tel,gender) values('91851398'" +
                            $",'9186173542','male');";
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.Contact administrator");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
            }
            finally
            {
                conn.Close();
                //调试初始化
                textBox1.Text = "1001";
                textBox2.Text = "123";
                checkBox3.Checked = true;
            }
        }
        
        private void adminCheckBox(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox3.Checked = false;
                checkBox8.Checked = false;
                account.type = "admin";
            }
        }
        private void teamCheckBox(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox8.Checked = false;
                account.type = "team";
            }
        }
        private void loginClick(object sender, EventArgs e)
        {
            account.accountNum = Int32.Parse(textBox1.Text);
            account.pwd = textBox2.Text;
            if (CheckAccount(account))
            {
                if (account.type == "admin")
                {
                    AdminIni initial = new AdminIni(conn);
                    initial.ShowDialog();
                }else if (account.type == "team")
                {
                    TeamIni teamIni = new TeamIni(conn , account.accountNum.ToString());
                    teamIni.ShowDialog();
                }
                else if (account.type == "controller")
                {
                    ControllerInfo controllerInfo = new ControllerInfo(conn);
                    controllerInfo.ShowDialog();
                }
            }
            else 
            {
                MessageBox.Show("密码错误");
            }
        }
        private bool CheckAccount(Account account)
        {
            try
            {
                conn.Open();
                //向数据库查询是否账号密码是否正确
                string sqlStr = $"select pwd from account where" +
                    $" accountType= '{account.type}' and accountNum = {account.accountNum};";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlStr, conn);
                //返回一个dataSet
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet);
                string pwdInDBS = dataSet.Tables[0].Rows[0][0].ToString();
                conn.Close();
                if (pwdInDBS == account.pwd)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        private void controllerCheckChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                account.type = "controller";
            }
        }
    }
}
