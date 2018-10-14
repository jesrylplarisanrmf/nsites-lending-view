using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Lendings;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Lendings.Reports.ReportsRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Reports
{
    public partial class MonthlyCollectionsUI : Form
    {
        Branch loBranch;
        LoanEndOfDay loLoanEndOfDay;
        MonthlyCollectionsRpt loMonthlyCollectionsRpt;

        public MonthlyCollectionsUI()
        {
            InitializeComponent();
            loBranch = new Branch();
            loLoanEndOfDay = new LoanEndOfDay();
            loMonthlyCollectionsRpt = new MonthlyCollectionsRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClose_Click");
                em.ShowDialog();
                return;
            }
        }

        private void CashFlowSummaryUI_Load(object sender, EventArgs e)
        {
            cboBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
            cboBranch.ValueMember = "Id";
            cboBranch.DisplayMember = "Description";
            cboBranch.SelectedIndex = -1;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmMonthlyCollections", "Refresh"))
                {
                    return;
                }

                try
                {
                    loMonthlyCollectionsRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loMonthlyCollectionsRpt.Database.Tables[1].SetDataSource(loLoanEndOfDay.getMonthlyCollections(cboYear.Text,cboBranch.SelectedValue.ToString()));
                    loMonthlyCollectionsRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loMonthlyCollectionsRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loMonthlyCollectionsRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loMonthlyCollectionsRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loMonthlyCollectionsRpt.SetParameterValue("Title", "Monthly Collections");
                    loMonthlyCollectionsRpt.SetParameterValue("SubTitle", "Monthly Collections");
                    loMonthlyCollectionsRpt.SetParameterValue("BranchName", cboBranch.Text);
                    loMonthlyCollectionsRpt.SetParameterValue("Year", cboYear.Text);
                    crvMonthlyCollections.ReportSource = loMonthlyCollectionsRpt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
