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
    public partial class MonthlyProjectionsUI : Form
    {
        Branch loBranch;
        LoanApplication loLoanApplication;
        MonthlyProjectionsRpt loMonthlyProjectionsRpt;
        public MonthlyProjectionsUI()
        {
            InitializeComponent();
            loBranch = new Branch();
            loLoanApplication = new LoanApplication();
            loMonthlyProjectionsRpt = new MonthlyProjectionsRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void MonthlyProjectionsUI_Load(object sender, EventArgs e)
        {
            cboBranch.DataSource = loBranch.getAllData("ViewAll", "", "");
            cboBranch.ValueMember = "Id";
            cboBranch.DisplayMember = "Description";
            cboBranch.SelectedIndex = -1;
        }

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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmMonthlyProjections", "Refresh"))
                {
                    return;
                }

                try
                {
                    loMonthlyProjectionsRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loMonthlyProjectionsRpt.Database.Tables[1].SetDataSource(loLoanApplication.getMonthlyProjectionByBranch(cboBranch.SelectedValue.ToString()));
                    loMonthlyProjectionsRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loMonthlyProjectionsRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loMonthlyProjectionsRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loMonthlyProjectionsRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loMonthlyProjectionsRpt.SetParameterValue("Title", "Monthly Projections");
                    loMonthlyProjectionsRpt.SetParameterValue("SubTitle", "Monthly Projections");
                    loMonthlyProjectionsRpt.SetParameterValue("BranchName", cboBranch.Text);
                    crvMonthlyProjections.ReportSource = loMonthlyProjectionsRpt;
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
