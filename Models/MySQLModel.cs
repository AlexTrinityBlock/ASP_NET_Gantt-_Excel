using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ASP_NET_Gantt__Excel.Models
{
    public class MySQLModel
    {
        public string connString;
        public MySqlConnection conn;

        public MySQLModel()
        {
            connString = "server=127.0.0.1;port=3306;user id=root;password=root;database=mvcdb;charset=utf8;";
            conn = new MySqlConnection();
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
            {
                testDatabase();
                conn.Open();
            }

        }

        //測試資料庫
        public void testDatabase()
        {
            //如果找不到目標資料表，則重建資料表
            try
            {
                getTaskData();
            }
            catch (Exception ex)
            {
                conn.Close();
                buildDatabase();
            }
        }

        //重建資料庫
        public void buildDatabase()
        {
            connString = "server=127.0.0.1;port=3306;user id=root;password=root;charset=utf8;";
            conn = new MySqlConnection();
            conn.ConnectionString = connString;
            conn.Open();
            string sql = @"CREATE DATABASE IF NOT EXISTS `mvcdb`;USE `mvcdb`;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            sql = @"
                CREATE TABLE IF NOT EXISTS `tasklist` (
                  `id` int(11) NOT NULL AUTO_INCREMENT,
                  `taskName` varchar(50) NOT NULL DEFAULT '',
                  `startTime` varchar(50) NOT NULL DEFAULT '',
                  `endTime` varchar(50) NOT NULL DEFAULT '',
                  `needNumber` varchar(50) NOT NULL DEFAULT '',
                  `finishedNumber` varchar(50) NOT NULL DEFAULT '',
                  PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
            ";
            //
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //取得工作任務資料
        public void getTaskData()
        {
            string sql = @"SELECT * FROM `tasklist` ";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            DataTable dataTable = new DataTable();
            MySqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                }
            }

            conn.Close();

            //return null;
        }

        //設置工作任務資料
        public void setTaskData(TaskData taskDataObj)
        {
            string sql = @"INSERT INTO `tasklist` (`taskName`,`startTime`,`endTime`,`needNumber`,`finishedNumber`) VALUES (@taskName,@startTime,@endTime,@needNumber,@finishedNumber)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@taskName", MySqlDbType.VarChar);
            cmd.Parameters["@taskName"].Value = taskDataObj.taskName;

            cmd.Parameters.Add("@startTime", MySqlDbType.VarChar);
            cmd.Parameters["@startTime"].Value = taskDataObj.startTime;

            cmd.Parameters.Add("@endTime", MySqlDbType.VarChar);
            cmd.Parameters["@endTime"].Value = taskDataObj.endTime;

            cmd.Parameters.Add("@needNumber", MySqlDbType.VarChar);
            cmd.Parameters["@needNumber"].Value = taskDataObj.needNumber;

            cmd.Parameters.Add("@finishedNumber", MySqlDbType.VarChar);
            cmd.Parameters["@finishedNumber"].Value = taskDataObj.finishedNumber;

            cmd.ExecuteNonQuery();
            conn.Close();

        }
    }
}