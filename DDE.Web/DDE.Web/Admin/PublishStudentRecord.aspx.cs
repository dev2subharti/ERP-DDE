using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

namespace DDE.Web.Admin
{
    public partial class PublishStudentRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblHeading.Text = "Fee Report (" + Convert.ToDateTime(Session["From"]).ToString("dd-MM-yyyy").Substring(0, 10) + " to " + Convert.ToDateTime(Session["To"]).ToString("dd-MM-yyyy").Substring(0, 10) + ")";
                populateStudents();
            }
        }

        private void populateStudents()
        {
            string from = Session["from"].ToString();
            string to = Session["To"].ToString();

            //string srids = FindInfo.findSRIDSBySCCodeandBatch("029","A 2013-14");
           

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (Session["SCCode"].ToString() == "ALL")
            {

                if (Session["Exam"].ToString() == "NA")
                {
                    if (Session["EntryType"].ToString() == "10")
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                }
                else
                {
                    if (Session["EntryType"].ToString() == "10")
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                }
            }
            else
            {
                string srids = FindInfo.findAllSRIDSBySCCode(Session["SCCode"].ToString());
                if (Session["Exam"].ToString() == "NA")
                {
                    if (Session["EntryType"].ToString() == "10")
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                }
                else
                {
                    if (Session["EntryType"].ToString() == "10")
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["Year"]) == 0)
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                        else
                        {
                            cmd.CommandText = "Select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14]  where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "'  union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and EntryType='" + Session["EntryType"].ToString() + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and ForExam='" + Session["Exam"].ToString() + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "')a";
                        }
                    }
                }
            
                
            }
            cmd.Connection = con;

          
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
           
            DataColumn dtcol3 = new DataColumn("ANo");
            DataColumn dtcol4 = new DataColumn("Ad.Through");
            DataColumn dtcol5 = new DataColumn("EnrollmentNo");
            DataColumn dtcol6 = new DataColumn("ICardNo");
            DataColumn dtcol7 = new DataColumn("Ad.Type");
            DataColumn dtcol8 = new DataColumn("StudentName");
            DataColumn dtcol9 = new DataColumn("FatherName");
            DataColumn dtcol10 = new DataColumn("Batch");
            DataColumn dtcol11 = new DataColumn("SCCode");
            DataColumn dtcol12 = new DataColumn("Course");
            DataColumn dtcol13 = new DataColumn("CurrentYear");
            DataColumn dtcol14 = new DataColumn("DOB");
            DataColumn dtcol15 = new DataColumn("Gender");
            DataColumn dtcol16 = new DataColumn("Nationality");
            DataColumn dtcol17 = new DataColumn("Category");
            DataColumn dtcol18 = new DataColumn("Address");
            DataColumn dtcol19 = new DataColumn("Pincode");
            DataColumn dtcol20 = new DataColumn("PhoneNo");
            DataColumn dtcol21 = new DataColumn("MobileNo");
            DataColumn dtcol22 = new DataColumn("EMail");

            DataColumn dtcol23 = new DataColumn("FeeDetails");           
         
            DataColumn dtcol30 = new DataColumn("Status");
            DataColumn dtcol31 = new DataColumn("Remark");


            dt.Columns.Add(dtcol1);
           
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);
            dt.Columns.Add(dtcol12);
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);
            dt.Columns.Add(dtcol15);
            dt.Columns.Add(dtcol16);
            dt.Columns.Add(dtcol17);
            dt.Columns.Add(dtcol18);
            dt.Columns.Add(dtcol19);
            dt.Columns.Add(dtcol20);
            dt.Columns.Add(dtcol21);
            dt.Columns.Add(dtcol22);
            dt.Columns.Add(dtcol23);
         
            dt.Columns.Add(dtcol30);
            dt.Columns.Add(dtcol31);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (FindInfo.findRecordStatusBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"])))
                {
                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i+1;

                    fillStudentInfo(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), drow);

                    if (Session["ReportType"].ToString() == "1")
                    {
                        fillFeeDetails(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), drow);
                    }

                    string remark;
                    if (FindInfo.isDetained(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), "ALL", "ALL", out remark))
                    {
                        drow["Status"] = "Detained";
                        drow["Remark"] = remark;
                    }
                    else
                    {
                        drow["Status"] = "OK";
                        drow["Remark"] = "";
                    }

                    dt.Rows.Add(drow);
                  
                }

            }

            gvShowStudent.DataSource = dt;
            gvShowStudent.DataBind();
           
            con.Close();       

        }

        private void fillFeeDetails(int srid, DataRow drow)
        {
            string from = Session["from"].ToString();
            string to = Session["To"].ToString();
            StringBuilder mb = new StringBuilder();

            //mb.Append("<table border='1' cellspacing='0px' cellpading='5px' >");
            //mb.Append("<tr>");
            //mb.Append("<td align='Center'><b>S.No.</b></td>");
            //mb.Append("<td align='Center'><b>Fee Head</b></td>");
            //mb.Append("<td align='Center'><b>Payment Mode</b></td>");
            //mb.Append("<td align='Center'><b>Amount</b></td>");
            //mb.Append("<td align='Center'><b>D/C No.</b></td>");
            //mb.Append("<td align='Center'><b>D/C Date</b></td>");
            //mb.Append("<td align='Center'><b>Total D/C Amount</b></td>");
            //mb.Append("<td align='Center'><b>Bank Name</b></td>");
            //mb.Append("</tr>");

            //mb.Append("<b>S.No. -  Fee Head - Payment Mode - Amount - D/C No. - D/C Date - Total D/C Amount - Bank Name</b><br/>");

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (Session["EntryType"].ToString() == "10")
            {
                if (Convert.ToInt32(Session["Year"]) == 0)
                {
                    cmd.CommandText = "Select * from [DDEFeeRecord_2013-14]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2014-15]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2015-16]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2016-17]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "'";
                }
                else
                {
                    cmd.CommandText = "Select * from [DDEFeeRecord_2013-14]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2014-15]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2015-16]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2016-17]  where SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "'";
                }
            }
            else
            {
                if (Convert.ToInt32(Session["Year"]) == 0)
                {
                    cmd.CommandText = "Select * from [DDEFeeRecord_2013-14]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2014-15]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2015-16]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2016-17]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "'";
                }
                else
                {
                    cmd.CommandText = "Select * from [DDEFeeRecord_2013-14]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2014-15]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2015-16]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' union Select * from [DDEFeeRecord_2016-17]  where EntryType='" + Session["EntryType"].ToString() + "' and SRID='" + srid + "' and FeeHead in (" + Session["FHIDS"].ToString() + ") and ForYear='" + Convert.ToInt32(Session["Year"]) + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "'";
                }
            }

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
           
                //mb.Append("<tr>");
                //mb.Append("<td>" + i.ToString() + "</td>");
                //mb.Append("<td>" + FindInfo.findFeeHeadNameByID(Convert.ToInt32(dr["FeeHead"])) + "</td>");
                //mb.Append("<td>" + FindInfo.findPaymentModeByID(Convert.ToInt32(dr["PaymentMode"])) + "</td>");
                //mb.Append("<td>" + Convert.ToString(dr["Amount"]) + "</td>");
                //mb.Append("<td>" + Convert.ToString(dr["DCNumber"]) + "</td>");
                //mb.Append("<td>" + Convert.ToDateTime(dr["DCDate"]).ToString("dd-MM-yyyy") + "</td>");
                //mb.Append("<td>" + Convert.ToString(dr["TotalDCAmount"]) + "</td>");
                //mb.Append("<td>" + Convert.ToString(dr["IBN"]) + "</td>");
                //mb.Append("</tr>");

                mb.Append("<b>(</b>"+i.ToString() + " - ");
                mb.Append(FindInfo.findFeeHeadNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"])) + " - ");
                mb.Append(FindInfo.findPaymentModeByID(Convert.ToInt32(ds.Tables[0].Rows[i]["PaymentMode"])) + " - ");
                mb.Append(Convert.ToString(ds.Tables[0].Rows[i]["Amount"]) + " - ");
                mb.Append(Convert.ToString(ds.Tables[0].Rows[i]["DCNumber"]) + " - ");
                mb.Append(Convert.ToDateTime(ds.Tables[0].Rows[i]["DCDate"]).ToString("dd-MM-yyyy") + " - ");
                mb.Append(Convert.ToString(ds.Tables[0].Rows[i]["TotalDCAmount"]) + " - ");
                if (Convert.ToString(ds.Tables[0].Rows[i]["IBN"]) == "")
                {
                    mb.Append("NA <b>)</b>, ");
                }
                else
                {
                    mb.Append(Convert.ToString(ds.Tables[0].Rows[i]["IBN"]) + "<b>)</b>, ");
                }

             

            }
           


            //mb.Append("</table>").AppendLine();
            drow["FeeDetails"] = mb.ToString();

        }

       

        private void fillStudentInfo(int srid,DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
           


                drow["ANo"] = ds.Tables[0].Rows[i]["ApplicationNo"].ToString();
                if (ds.Tables[0].Rows[i]["AdmissionThrough"].ToString() != "")
                {
                    drow["Ad.Through"] = FindInfo.findAdmissionThrough(Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionThrough"]));
                }
                else
                {
                    drow["Ad.Through"] = "NF";
                }
                drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                drow["ICardNo"] = ds.Tables[0].Rows[i]["ICardNo"].ToString();

                if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() != "")
                {
                    drow["Ad.Type"] = FindInfo.findAdmissionType(Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionType"]));
                }
                else
                {
                    drow["Ad.Type"] = "NF";
                }
                drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                drow["FatherName"] = ds.Tables[0].Rows[i]["FatherName"].ToString();
                drow["Batch"] = ds.Tables[0].Rows[i]["Session"].ToString();
                drow["SCCode"] = FindInfo.findBothTCSCCodeBySRID(srid);
                drow["Course"] = FindInfo.findCourseShortNameBySRID(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                drow["CurrentYear"] = Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]);
                drow["DOB"] = ds.Tables[0].Rows[i]["DOBDay"].ToString() + "-" + ds.Tables[0].Rows[i]["DOBMonth"].ToString() + "-" + ds.Tables[0].Rows[i]["DOBYear"].ToString();
                drow["Gender"] = ds.Tables[0].Rows[i]["Gender"].ToString();
                drow["Nationality"] = ds.Tables[0].Rows[i]["Nationality"].ToString();
                drow["Category"] = ds.Tables[0].Rows[i]["Category"].ToString();
                drow["Address"] = ds.Tables[0].Rows[i]["CAddress"].ToString();
                drow["Pincode"] = ds.Tables[0].Rows[i]["PinCode"].ToString();
                drow["PhoneNo"] = ds.Tables[0].Rows[i]["PhoneNo"].ToString();
                drow["MobileNo"] = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                drow["Email"] = ds.Tables[0].Rows[i]["Email"].ToString();
            }


            con.Close();

        }

        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=FeeReport_"+Session["Year"]+"_(" +Convert.ToDateTime(Session["From"]).ToString("dd-MM-yyyy").Substring(0, 10) + "to" + Convert.ToDateTime(Session["To"]).ToString("dd-MM-yyyy").Substring(0, 10)+").xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvShowStudent.RenderBeginTag(hw);
            gvShowStudent.HeaderRow.RenderControl(hw);
            foreach (GridViewRow row in gvShowStudent.Rows)
            {
                row.RenderControl(hw);
            }
            gvShowStudent.FooterRow.RenderControl(hw);
            gvShowStudent.RenderEndTag(hw);

            //gvShowStudent.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

    }
}
