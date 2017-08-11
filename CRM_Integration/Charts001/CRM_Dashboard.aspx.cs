using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FusionCharts.Charts;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;

namespace Charts001
{
    public partial class JSON1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1-Harini
            GetColorH();
            Chart sales9 = new Chart("column2d", "myChartH", "320", "250", "jsonurl", "../Data/Employee.json", "000000", "100"); // ../Data/Data.json
                                                                                                               // Render the chart
            Literal1.Text = sales9.Render();
            //GetDataSourceH();


            // 2 -Ganesh

            GetColor_revenue();
            Chart sales = new Chart("column2d", "myChartG", "320", "250", "jsonurl", "../Data/json_revenue.json","000000", "100"); // ../Data/Data.json
            Literal2.Text = sales.Render();

            //Dariia
            //Build & Render the chart
            GetColor2();
            Chart barchart = new Chart("bar2d", "myChart1", "320", "250", "jsonurl", "../Data/barchart_top_10.json", "000000", "100");            
            Literal4.Text = barchart.Render();

            GetDataSource();
            Chart gaugeangular = new Chart("angulargauge", "myChart2", "320", "250", "jsonurl", "../Data/angulargauge_1ind.json", "000000", "100"); 
            Literal6.Text = gaugeangular.Render();
            /*
                        Chart sales3 = new Chart("pyramid", "myChart3", "600", "350", "jsonurl", "../Data/pyramid.json"); // ../Data/Data.json
                        // Render the chart
                        Literal3.Text = sales3.Render();
                         */
            /*
Chart doughnutInd = new Chart("bar2d", "myChart4", "600", "350", "jsonurl", "../Data/new.json"); // ../Data/Data.json
// Render the chart doughnut2d
Literal4.Text = doughnutInd.Render();*/
            /*
            GetDataSource2();
            GetDataSource3();
            
            Chart cri_vs_industry = new Chart("mscolumn2d", "myChart3", "300", "250", "jsonurl", "../Data/barreslevel.json", "000000", "100"); //
            Literal2.Text = cri_vs_industry.Render();

            */

            /***************************************************/
            //Chenyang Feng
            /*
            GetColor();
            Chart correlationGraph = new Chart("bar3d", "myChart", "300", "250", "jsonurl", "../Data/CRBarForCompany.json", "000000", "100");
            // Render the chart
            Literal4.Text = correlationGraph.Render();
            GetData4();
            Chart companySample = new Chart("angulargauge", "myChart5", "300", "250", "jsonurl", "../Data/piChart.json", "000000", "100");
            Literal5.Text = companySample.Render();

            Chart databaseSeach = new Chart("column3d", "myChart4", "300", "250", "jsonurl", "../Data/CRbar.json", "000000", "100");
            // Render the chart
            Literal6.Text = databaseSeach.Render();
            */
            GetColor();

            Chart correlationGraph = new Chart("bar3d", "myChart", "320", "250", "jsonurl", "../Data/CRBarForCompany.json", "000000", "100");
            Literal3.Text = correlationGraph.Render();

            //Vishakh


            GetDataSourceVS();
            nameChange();
            Chart sales2 = new Chart("bar2d", "myChartV", "320", "250", "jsonurl", "../Data/dtstojson.json", "000000", "100"); // ../Data/Data.json
            Literal5.Text = sales2.Render();
        
    }


        protected void SearchData(object sender, EventArgs e)
        {
            Page_Load(null, null);
        }

        private string GetDataSource()
        {
            var responseFromDb = "0";
            if (Text1.Text != "")
            {
                responseFromDb = GetdataFromDb(Text1.Text);
            }

            var json2Path = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/angulargauge_1ind.json";
            //ozapros w bazu, poluczili otwet
            //otkryli fail
            JObject o1 = JObject.Parse(File.ReadAllText(json2Path));
            //mieniajem znaczenie to czto tebe nożno 
            o1["dials"]["dial"][0]["value"] = string.Format("{0}", responseFromDb);
            //o1["categories"]["category"][0]["value"] = string.Format("{0}", responseFromDb);

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(json2Path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                o1.WriteTo(writer);
            }
            
            return json2Path;
        }

        private string GetdataFromDb(string text)
        {
            SqlConnection conn = new SqlConnection(@"Server= tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password = sa#1234;");

            string strQuery = @"SELECT sec.SecurLevel FROM Fortune500 As fort inner join
                                SecurityLevel As sec ON fort.IndustryID = sec.ID WHERE fort.Title like '%'+ @industry + '%'";

            SqlCommand cmd = new SqlCommand(strQuery, conn);
            cmd.Parameters.AddWithValue("@industry", text.Trim());
            DataTable dt = GetData(cmd);
            return dt?.Rows[0]?.ItemArray[0]?.ToString();
        }

        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }







        private string GetDataSource2()//
        {
            var responseFromDb = "0";

           // var responseFromDb2 = "0";
            if (Text1.Text != "")
            {
                responseFromDb = GetdataFromDb2(Text1.Text);
               // responseFromDb2 = GetdataFromDb22(Text1.Text);
            }
            var json2Path = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/barreslevel.json";
            //ozapros w bazu, poluczili otwet


            //otkryli fail
            JObject o1 = JObject.Parse(File.ReadAllText(json2Path));
            //mieniajem znaczenie to czto tebe nożno 
            o1["dataset"][0]["data"][0]["value"] = string.Format("{0}", responseFromDb);
           // o1["dataset"][1]["data"][0]["value"] = string.Format("{0}", responseFromDb2);
            // o1["categories"][0]["category"][0]["label"] = string.Format("{0}", responseFromDb);

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(json2Path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                o1.WriteTo(writer);
            }

            return json2Path;
        }
        private string GetdataFromDb2(string text)
        {
            SqlConnection conn = new SqlConnection(@"Server= tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password = sa#1234;");

            string strQuery = @"SELECT sec.SecurLevel FROM Fortune500 As fort inner join
                                SecurityLevel As sec ON fort.IndustryID = sec.ID WHERE fort.Title like '%'+ @industry + '%'";

            //, sec.SecurIndustry

            SqlCommand cmd = new SqlCommand(strQuery, conn);
            cmd.Parameters.AddWithValue("@industry", text.Trim());
            DataTable dt = GetData2(cmd);
            return dt?.Rows[0]?.ItemArray[0]?.ToString();
        }
        private string GetdataFromDb22(string text)
        {
            SqlConnection conn = new SqlConnection(@"Server= tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password = sa#1234;");

            string strQuery = @"SELECT sec.BreachLevel FROM Fortune500 As fort inner join
                                SecurityLevel As sec ON fort.IndustryID = sec.ID WHERE fort.Title like '%'+ @industry + '%'";

            //, sec.SecurIndustry

            SqlCommand cmd = new SqlCommand(strQuery, conn);
            cmd.Parameters.AddWithValue("@industry", text.Trim());
            DataTable dt = GetData22(cmd);
            return dt?.Rows[0]?.ItemArray[0]?.ToString();
        }

        private DataTable GetData2(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        private DataTable GetData22(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
        private string GetColor2()
        {
            var dataDB = GetdataFromDb3(Text1.Text);
            var jsonPath = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/barchart_top_10.json";
            if (dataDB != "Click 'GO' ")
            {
                //var dataDB = GetDataSource();
                //var x = decimal.Parse(dataDB, CultureInfo.InvariantCulture.NumberFormat);
                // var jsonPath = @"C:\Users\fcyfcy\Desktop\CRM_Subsidiaries\Charts001\Charts001\Data\json.json";

                JObject o1 = JObject.Parse(File.ReadAllText(jsonPath));
                for (int i = 0; i <9; i++)
                {
                    var x =o1["data"][i]["label"]+"";
                    if(dataDB.Contains(x))
                    {
                        o1["data"][i]["color"]= string.Format("{0}", "#cb1126");
                    }
                    else
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#0000ff");
                    }
                    //int y = Convert.ToInt2(o1["dataset"][0]["data"][i]["label"]);
                    //int y2 = Convert.ToInt32(o1["dataset"][0]["data"][i - 1]["label"]);
                   /* if (y2 >= x && y < x)
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#cb1126");

                    }
                    else
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#0000ff");
                        // o1["data"][i - 1]["color"] = string.Format("{0}", "#0000ff");
                    }*/
                }
                using (StreamWriter file = File.CreateText(jsonPath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    o1.WriteTo(writer);
                }
            }

            return jsonPath;

            //o1["dataset"][0]["data"][0]["value"] = string.Format("{0}", responseFromDb);
        }

















        private string GetDataSource3()//
        {
            var responseFromDb = "0";
            if (Text1.Text != "")
            {
                responseFromDb = GetdataFromDb3(Text1.Text);
            }
            var json2Path = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/barreslevel.json";
            //ozapros w bazu, poluczili otwet


            //otkryli fail
            JObject o1 = JObject.Parse(File.ReadAllText(json2Path));
            //mieniajem znaczenie to czto tebe nożno 
           // o1["dataset"][0]["data"][0]["value"] = string.Format("{0}", responseFromDb);
            o1["categories"][0]["category"][0]["label"] = string.Format("{0}", responseFromDb);

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(json2Path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                o1.WriteTo(writer);
            }

            return json2Path;
        }
        private string GetdataFromDb3(string text)
        {
            SqlConnection conn = new SqlConnection(@"Server= tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password = sa#1234;");

            string strQuery = @"SELECT sec.SecurIndustry FROM Fortune500 As fort inner join
                                SecurityLevel As sec ON fort.IndustryID = sec.ID WHERE fort.Title like '%'+ @industry + '%'";

            //, sec.SecurIndustry

            SqlCommand cmd = new SqlCommand(strQuery, conn);
            cmd.Parameters.AddWithValue("@industry", text.Trim());
            DataTable dt = GetData3(cmd);
            return dt?.Rows[0]?.ItemArray[0]?.ToString();
        }

        private DataTable GetData3(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        protected void SearchData4(object sender, EventArgs e)
        {
            Page_Load(null, null);
        }
        private string GetDataSource4()
        {
            var responseFromDb = "0";
            if (Text1.Text != "")
            {
                responseFromDb = GetdataFromDb4(Text1.Text);
            }
            else
            {
                responseFromDb = "Click 'GO' ";
            }
            return responseFromDb;

        }

        private string GetdataFromDb4(string text)
        {
            // string strQuery = @"SELECT SubsidiaryCyber.cyberResilience FROM SubsidiaryCyber WHERE SubsidiaryCyber.CompanyName like '%'+@text+'%'";
            string access_link = (@"Server=tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password = sa#1234;");
            SqlConnection conn = new SqlConnection(access_link);
            var con = ConfigurationManager.ConnectionStrings["conString"].ToString();

            string matchingPerson = "";
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "SELECT SubsidiaryCyber.cyberResilience FROM SubsidiaryCyber WHERE SubsidiaryCyber.CompanyName like '%'+@text+'%'";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                oCmd.Parameters.AddWithValue("@text", text);
                conn.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingPerson = oReader["cyberResilience"].ToString();
                    }

                    myConnection.Close();
                }
            }
            return matchingPerson;
        }



        protected void Text2_TextChanged(object sender, EventArgs e)
        {

        }
        private string GetData4()
        {
            var responseFromDb = GetDataSource4();
            var json2Path = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/piChart.json";
            JObject o1 = JObject.Parse(File.ReadAllText(json2Path));
            o1["dials"]["dial"][0]["value"] = string.Format("{0}", responseFromDb);

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(json2Path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                o1.WriteTo(writer);
            }

            return json2Path;
        }
        private string GetColor()
        {
            var dataDB = GetDataSource4();
            var jsonPath = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/CRBarForCompany.json";
            if (dataDB != "Click 'GO' ")
            {
                //var dataDB = GetDataSource();
                var x = float.Parse(dataDB, CultureInfo.InvariantCulture.NumberFormat);
                // var jsonPath = @"C:\Users\fcyfcy\Desktop\CRM_Subsidiaries\Charts001\Charts001\Data\json.json";

                JObject o1 = JObject.Parse(File.ReadAllText(jsonPath));
                for (int i = 8; i > 1; i--)
                {
                    int y = Convert.ToInt32(o1["data"][i]["label"]);
                    int y2 = Convert.ToInt32(o1["data"][i - 1]["label"]);
                    if (y2 >= x && y < x)
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#cb1126");

                    }
                    else
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#0000ff");
                        // o1["data"][i - 1]["color"] = string.Format("{0}", "#0000ff");
                    }
                }
                using (StreamWriter file = File.CreateText(jsonPath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    o1.WriteTo(writer);
                }
            }

            return jsonPath;

            //o1["dataset"][0]["data"][0]["value"] = string.Format("{0}", responseFromDb);
        }


        protected void SearchData_revenue(object sender, EventArgs e)
        {
            Page_Load(null, null);
        }


        private string[] GetdataFromDb_revenue(string text)
        {
            // string strQuery = @"SELECT SubsidiaryCyber.cyberResilience FROM SubsidiaryCyber WHERE SubsidiaryCyber.CompanyName like '%'+@text+'%'";
            string access_link = (@"Server=tcp:79.30.2.148,1433; Database=CRM; User Id=sa; Password =sa#1234;"); //(@"Server=tcp:192.168.1.146,1433; Database=CRM; User Id=sa; Password =sa#1234;"); ---- sterling office
            SqlConnection conn = new SqlConnection(access_link);



            var con = ConfigurationManager.ConnectionStrings["conString"].ToString();

            string[] matchingPerson = new string[4];
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "SELECT CRI, Revenues_in_M, Profit_in_M, Avg_Budget_on_Security_in_M FROM CRM_Revenue As fort  WHERE fort.Company_Name = '' + @text + '' ";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                oCmd.Parameters.AddWithValue("@text", text);
                conn.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingPerson[0] = oReader["CRI"].ToString();
                        matchingPerson[1] = oReader["Revenues_in_M"].ToString();
                        matchingPerson[2] = oReader["Avg_Budget_on_Security_in_M"].ToString();
                    }

                    myConnection.Close();
                }
            }
            return matchingPerson;
            /* DataTable dt = GetData(cmd);
            return dt?.Rows[0]?.ItemArray[0]?.ToString();*/
        }
        //Server=GANESH-VAIO; Database=List_of_Public_Companies; User Id=sa; Password =1234
        private string GetColor_revenue()
        {
            var dataDB = GetdataFromDb_revenue(Text1.Text)[0];
            var dataDB1 = "Revenue Spent on Cybe Security " + GetdataFromDb_revenue(Text1.Text)[2] + " M";
            var jsonPath = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/json_revenue.json";

            if (dataDB != "Click 'GO' ")
            {
                //var dataDB = GetDataSource();
                var x = Convert.ToInt32(float.Parse(dataDB, CultureInfo.InvariantCulture.NumberFormat)) + "";
                // var jsonPath = @"C:\Users\fcyfcy\Desktop\CRM_Subsidiaries\Charts001\Charts001\Data\json.json";

                JObject o1 = JObject.Parse(File.ReadAllText(jsonPath));
                o1["annotations"]["groups"][0]["items"][1]["label"] = string.Format("{0}", dataDB1);
                for (int i = 0; i < 9; i++)
                {
                    var y = o1["data"][i]["label"] + "";
                    if (y == x)
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#cb1126");

                    }
                    else
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#0000ff");
                        // o1["data"][i - 1]["color"] = string.Format("{0}", "#0000ff");
                    }
                }
                using (StreamWriter file = File.CreateText(jsonPath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    o1.WriteTo(writer);
                }
            }

            return jsonPath;

            //o1["dataset"][0]["data"][0]["value"] = string.Format("{0}", responseFromDb);
        }

        protected void Text1_TextChanged(object sender, EventArgs e)
        {

        }


        protected void SearchDataVS(object sender, EventArgs e)
        {
            Page_Load(null, null);
        }
        private string GetDataSourceVS()
        {
            string plottingData = "";
            string responseFromDb = "0";
            string[] data1 = new string[5];
            data1[0] = GetdataFromDbVS(Text1.Text);

            if (Text1.Text != "")
            {
                plottingData = GetPlottingValues(Text1.Text);
                responseFromDb = Convert.ToString(GetdataFromDbVS(Text1.Text));


                var json2Path = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/json2.json";



                JObject o1 = JObject.Parse(File.ReadAllText(json2Path));

                o1["dials"]["dial"][0]["value"] = string.Format("{0}", responseFromDb);


                using (StreamWriter file = File.CreateText(json2Path))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    o1.WriteTo(writer);
                }


                var jsonBarGraphPath = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/dtstojson.json";

                using (StreamWriter tw = new StreamWriter(jsonBarGraphPath))
                {
                    tw.WriteLine(plottingData);
                }



                return json2Path;
            }
            return null;
        }

        private string GetdataFromDbVS2(string text)
        {
            // string strQuery = @"SELECT SubsidiaryCyber.cyberResilience FROM SubsidiaryCyber WHERE SubsidiaryCyber.CompanyName like '%'+@text+'%'";
            string access_link = (@"Server=tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password =sa#1234;");
            SqlConnection conn = new SqlConnection(access_link);



            var con = ConfigurationManager.ConnectionStrings["conString"].ToString();

            string matchingPerson1 = "";
            string matchingPerson2 = "";
            string matchingPerson3 = "";
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "SELECT RecordStolen,LeakedMethod,DataSensitivity FROM DataStolen As fort  WHERE fort.CompanyName like '%'+ @text + '%'";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                oCmd.Parameters.AddWithValue("@text", text);
                conn.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingPerson1 = oReader["RecordStolen"].ToString();
                        matchingPerson2 = oReader["LeakedMethod"].ToString();
                        matchingPerson3 = oReader["DataSensitivity"].ToString();
                    }

                    myConnection.Close();
                }
            }
            return matchingPerson1 + "\n" + matchingPerson2 + "\n" + matchingPerson3;

        }


        //-----------------------------------------------Main Code----------------------------------------
        private string GetPlottingValues(string text)
        {
            string access_link = (@"Server=tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password =sa#1234;");
            SqlConnection conn = new SqlConnection(access_link);
            long max = 0;


            var con = ConfigurationManager.ConnectionStrings["conString"].ToString();


            List<Data> dataArr = new List<Data>();
            for (int year = 2012; year < 2018; year++)
            {
                dataArr.Add(new Data(0.ToString(), year.ToString()));
            }

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "select * from DataStolen where Industry = (select Industry from DataStolen where CompanyName = ''+ @text + '')";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                oCmd.Parameters.AddWithValue("@text", text);
                conn.Open();
                using (SqlDataReader dataReader = oCmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Data newData = new Data(dataReader["RecordStolen"].ToString(), dataReader["Year"].ToString());
                        foreach (var item in dataArr)
                        {
                            if (item.label == newData.label)
                            {
                                item.value += newData.value;
                                if (item.value > max)
                                {
                                    max = item.value;
                                }
                            }
                        }
                    }
                }
                myConnection.Close();
                //max /= 10;  Decimal bound
            }

            StringBuilder strBuilt = new StringBuilder();
            strBuilt.Append("{'chart':{'caption':'Targeted Industries','subcaption':'Based on number of Data Breach/ Data Stolen / Data Lost','yaxisname':'Number of Records Stolen','xaxisname':'Year','plotgradientcolor':'','rotatevalues':'0','canvasBgColor':'#666666','baseFontColor': '#FFFFFF','bgColor':'#000000','divlinecolor':'','showvalues':'1','valuefontbold':'1','yaxisnamefontsize':'12','labelsepchar':': ','labeldisplay':'AUTO','numberscalevalue':'1,5,15,20','numberscaleunit':' ','animation':'0','theme':'zune'},'data':[");
            foreach (var item in dataArr)
            {
                //item.value /= max; scaling to the decimal values
                strBuilt.Append("{'label':'" + item.label + "','value':" + item.value + "},");
            }
            strBuilt = strBuilt.Remove(strBuilt.Length - 1, 1);
            strBuilt.Append("]}");
            return strBuilt.ToString().Replace("'", "\"");
        }

        private string GetdataFromDbVS(string text)
        {
            // string strQuery = @"SELECT SubsidiaryCyber.cyberResilience FROM SubsidiaryCyber WHERE SubsidiaryCyber.CompanyName like '%'+@text+'%'";
            string access_link = (@"Server=tcp: 79.30.2.148,1433; Database=CRM; User Id=sa; Password =sa#1234;");
            SqlConnection conn = new SqlConnection(access_link);

            var con = ConfigurationManager.ConnectionStrings["conString"].ToString();

            string matchingPerson = "";
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "SELECT Industry FROM DataStolen As fort  WHERE fort.CompanyName like '%'+ @text + '%'";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                oCmd.Parameters.AddWithValue("@text", text);
                conn.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingPerson = oReader["Industry"].ToString();
                        //matchingPerson[1] = oReader["RecordStolen"].ToString();
                        //matchingPerson[2] = oReader["LeakMethod"].ToString();
                        //matchingPerson[3] = oReader["DataSensitivity"].ToString();
                        //matchingPerson[4] = oReader["Action"].ToString();


                    }

                }
                myConnection.Close();
                // myConnection.Dispose();
            }
            return matchingPerson;
            /* DataTable dt = GetData(cmd);
            return dt?.Rows[0]?.ItemArray[0]?.ToString();*/
        }

        private string nameChange()
        {
            var dataDB = GetDataSourceVS();
            var jsonPath = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/dtstojson.json";
            JObject o3 = JObject.Parse(File.ReadAllText(jsonPath));
            //"caption":"Targeted Industries",
            o3["chart"]["caption"] = string.Format("{0}", "Targeted Industries " + GetdataFromDbVS(Text1.Text));
            using (StreamWriter file = File.CreateText(jsonPath))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                o3.WriteTo(writer);
            }
            return jsonPath;
        }

        public class Data
        {
            public long value;
            public string label;

            public Data(string recStole, string yr)
            {
                long.TryParse(recStole, out value);
                label = yr;
            }
        }

        protected void SearchDataH(object sender, EventArgs e)
        {
            Page_Load(null, null);
        }

        private string GetDataSourceH()
        {
            var responseFromDb = "0";
            if (Text1.Text != "")
            {
                responseFromDb = GetdataFromDbH(Text1.Text)[0];
            }
            else
            {
                responseFromDb = "Click 'GO' ";
            }
            return responseFromDb;

        }


        private string[] GetdataFromDbH(string text)
        {
            // string strQuery = @"SELECT SubsidiaryCyber.cyberResilience FROM SubsidiaryCyber WHERE SubsidiaryCyber.CompanyName like '%'+@text+'%'";
            string access_link = (@"Server=TCP:79.30.2.148,1433; Database=CRM; User Id=sa; Password =sa#1234;");
            SqlConnection conn = new SqlConnection(access_link);



            var con = ConfigurationManager.ConnectionStrings["conString"].ToString();

            string[] matchingPerson = new string[2];
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                // @"SELECT fortine1000list.employees FROM fortune1000list WHERE fortune1000list.Title like '%'+ @Title + '%'";
                string oString = "SELECT calc3, Employees FROM [CRM].[dbo].[CRM_EmployeeCount] WHERE [CRM].[dbo].[CRM_EmployeeCount].Title like '%'+ @Title + '%'";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                oCmd.Parameters.AddWithValue("@title", text);
                conn.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        matchingPerson[0] = oReader["calc3"].ToString();
                        matchingPerson[1] = oReader["Employees"].ToString();
                        break;
                    }

                    myConnection.Close();
                }
            }
            return matchingPerson;
            /* DataTable dt = GetData(cmd);
            return dt?.Rows[0]?.ItemArray[0]?.ToString();*/
        }
        private string GetColorH()
        {
            var dataDB = GetdataFromDbH(Text1.Text)[0];
            var dataDB1 = "No. of Employees: " + GetdataFromDbH(Text1.Text)[1];
            var jsonPath = @"C:\Users\Dariia\Documents\Visual Studio 2015\Projects\Charts001\Charts001\Data/Employee.json";
            if (dataDB != "Click 'GO' ")
            {
                //var dataDB = GetDataSourceH();
                var x = (int)Math.Floor(float.Parse(dataDB, CultureInfo.InvariantCulture.NumberFormat));

                JObject o1 = JObject.Parse(File.ReadAllText(jsonPath));
                o1["annotations"]["groups"][0]["items"][1]["label"] = string.Format("{0}", dataDB1);
                for (int i = 10; i > -1; i--)
                {
                    int y = Convert.ToInt32(o1["data"][i]["label"]);
                    if (y == x)
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#ff0000");

                    }
                    else
                    {
                        o1["data"][i]["color"] = string.Format("{0}", "#ffff00");
                        // o1["data"][i - 1]["color"] = string.Format("{0}", "#0000ff");
                    }
                }
                using (StreamWriter file = File.CreateText(jsonPath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    o1.WriteTo(writer);
                }
            }

            return jsonPath;

            //o1["dataset"][0]["data"][0]["value"] = string.Format("{0}", responseFromDb);
        }
    }
}                
        

     