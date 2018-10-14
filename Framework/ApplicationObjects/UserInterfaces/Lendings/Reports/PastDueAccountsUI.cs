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
    public partial class PastDueAccountsUI : Form
    {
        LoanApplication loLoanApplication;
        PastDueAccountsRpt loPastDueAccountsRpt;

        public PastDueAccountsUI()
        {
            InitializeComponent();
            loLoanApplication = new LoanApplication();
            loPastDueAccountsRpt = new PastDueAccountsRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void PastDueAccountsUI_Load(object sender, EventArgs e)
        {

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
                if (!GlobalFunctions.checkRights("tsmPastDueAccounts", "Refresh"))
                {
                    return;
                }

                try
                {
                    loPastDueAccountsRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPastDueAccountsRpt.Database.Tables[1].SetDataSource(loLoanApplication.getLoanApplicationPastDueAccounts());
                    loPastDueAccountsRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loPastDueAccountsRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loPastDueAccountsRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loPastDueAccountsRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loPastDueAccountsRpt.SetParameterValue("Title", "Past Due Accounts");
                    loPastDueAccountsRpt.SetParameterValue("SubTitle", "Past Due Accounts");
                    crvPastDueAccounts.ReportSource = loPastDueAccountsRpt;
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
