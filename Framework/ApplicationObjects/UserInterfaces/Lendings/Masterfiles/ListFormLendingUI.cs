using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using NSites_V.Global;

using NSites_V.ApplicationObjects.Classes.Lendings;
using NSites_V.ApplicationObjects.Classes.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Masterfiles
{
    public partial class ListFormLendingUI : Form
    {
        #region "VARIABLES"
        public object lObject;
        Type lType;
        string[] lRecord;
        string[] lColumnName;
        int lCountCol;
        //SearchUI loSearch;
        SearchesUI loSearches;
        Common loCommon;
        System.Data.DataTable ldtShow;
        //System.Data.DataTable ldtReport;
        //System.Data.DataTable ldtReportSum;
        ReportViewerUI loReportViewer;
        bool lFromRefresh;
        #endregion "END OF VARIABLES"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "CONSTRUCTORS"
        public ListFormLendingUI(object pObject, Type pType)
        {
            InitializeComponent();
            lObject = pObject;
            lType = pType;
            this.Text = pObject.GetType().Name + " List";
            loCommon = new Common();
            ldtShow = new System.Data.DataTable();
            //ldtReport = new System.Data.DataTable();
            //ldtReportSum = new System.Data.DataTable();
            loReportViewer = new ReportViewerUI();
            lFromRefresh = false;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        public void refresh(string pDisplayType,string pPrimaryKey, string pSearchString, bool pShowRecord)
        {
            lFromRefresh = true;
            loSearches.lQuery = "";
            try
            {
                dgvLists.Rows.Clear();
                dgvLists.Columns.Clear();

            }
            catch 
            {
                dgvLists.DataSource = null;
            }
            tsmiViewAllRecords.Visible = false;
            object[] _params = { pDisplayType, pPrimaryKey, pSearchString };

            ldtShow = (System.Data.DataTable)lObject.GetType().GetMethod("getAllData").Invoke(lObject, _params);
            if(ldtShow == null)
            {
                return;
            }
            lCountCol = ldtShow.Columns.Count;
            lColumnName = new string[lCountCol];
            lRecord = new string[lCountCol];
            for (int i = 0; i < lCountCol; i++)
            {
                dgvLists.Columns.Add(ldtShow.Columns[i].ColumnName, ldtShow.Columns[i].ColumnName);
            }
            if (pShowRecord)
            {
                foreach (DataRow _dr in ldtShow.Rows)
                {
                    int n = dgvLists.Rows.Add();
                    if (n < GlobalVariables.DisplayRecordLimit)
                    {
                        for (int i = 0; i < lCountCol; i++)
                        {
                            dgvLists.Rows[n].Cells[i].Value = _dr[i].ToString();
                        }
                    }
                    else
                    {
                        dgvLists.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                        dgvLists.Rows[n].DefaultCellStyle.ForeColor = Color.White;
                        dgvLists.Rows[n].Height = 5;
                        dgvLists.Rows[n].ReadOnly = true;
                        tsmiViewAllRecords.Visible = true;
                        break;
                    }
                }
            }
            try
            {
                for (int i = 0; i < lCountCol; i++)
                {
                    lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
                }
            }
            catch { }
        }
        
        public void refreshAll()
        {
            tsmiViewAllRecords.Visible = false;
            dgvLists.Rows.Clear();
            foreach (DataRow _dr in ldtShow.Rows)
            {
                int n = dgvLists.Rows.Add();
                for (int i = 0; i < lCountCol; i++)
                {
                    dgvLists.Rows[n].Cells[i].Value = _dr[i].ToString();
                }
            }

            for (int i = 0; i < lCountCol; i++)
            {
                lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
            }
        }

        public void addData(string[] pRecordData)
        {
            try
            {
                int n = dgvLists.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvLists.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvLists.CurrentRow.Selected = false;
                dgvLists.FirstDisplayedScrollingRowIndex = dgvLists.Rows[n].Index;
                dgvLists.Rows[n].Selected = true;
            }
            catch
            {
                refresh("ViewAll","", "", true);
            }
        }

        public void updateData(string[] pRecordData)
        {
            for (int i = 0; i < pRecordData.Length; i++)
            {
                dgvLists.CurrentRow.Cells[i].Value = pRecordData[i];
            }
        }
        #endregion "END OF METHODS"

        private void ListFormLendingUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type;
                FieldInfo[] myFieldInfo;
                switch (lType.Name)
                {
                    case "Client":
                        _Type = typeof(Client);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Area":
                        _Type = typeof(Area);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Branch":
                        _Type = typeof(Branch);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Zone":
                        _Type = typeof(Zone);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Collector":
                        _Type = typeof(Collector);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Product":
                        _Type = typeof(Product);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ListFormLendingUI_Load");
                em.ShowDialog();
                Application.Exit();
            }
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Refresh"))
                {
                    return;
                }
                refresh("ViewAll", "", "", true);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Create"))
                {
                    return;
                }
                if (dgvLists.Rows.Count == 0)
                {
                    refresh("ViewAll", "", "", false);
                }
                switch (lType.Name)
                {
                    case "Client":
                        ClientDetailUI loClientDetail = new ClientDetailUI();
                        loClientDetail.ParentList = this;
                        loClientDetail.ShowDialog();
                        break;
                    case "Area":
                        AreaDetailUI loAreaDetail = new AreaDetailUI();
                        loAreaDetail.ParentList = this;
                        loAreaDetail.ShowDialog();
                        break;
                    case "Branch":
                        BranchDetailUI loBranchDetail = new BranchDetailUI();
                        loBranchDetail.ParentList = this;
                        loBranchDetail.ShowDialog();
                        break;
                    case "Zone":
                        ZoneDetailUI loZoneDetail = new ZoneDetailUI();
                        loZoneDetail.ParentList = this;
                        loZoneDetail.ShowDialog();
                        break;
                    case "Collector":
                        CollectorDetailUI loCollectorDetail = new CollectorDetailUI();
                        loCollectorDetail.ParentList = this;
                        loCollectorDetail.ShowDialog();
                        break;
                    case "Product":
                        ProductDetailUI loProductDetail = new ProductDetailUI();
                        loProductDetail.ParentList = this;
                        loProductDetail.ShowDialog();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCreate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Update"))
                {
                    return;
                }

                for (int i = 0; i < lCountCol; i++)
                {
                    lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
                }

                if (lRecord.Length > 0)
                {
                    if (lRecord[0].ToString() != "")
                    {
                        switch (lType.Name)
                        {
                            case "Client":
                                ClientDetailUI loClientDetail = new ClientDetailUI(lRecord);
                                loClientDetail.ParentList = this;
                                loClientDetail.ShowDialog();
                                break;
                            case "Area":
                                AreaDetailUI loAreaDetail = new AreaDetailUI(lRecord);
                                loAreaDetail.ParentList = this;
                                loAreaDetail.ShowDialog();
                                break;
                            case "Branch":
                                BranchDetailUI loBranchDetail = new BranchDetailUI(lRecord);
                                loBranchDetail.ParentList = this;
                                loBranchDetail.ShowDialog();
                                break;
                            case "Zone":
                                ZoneDetailUI loZoneDetail = new ZoneDetailUI(lRecord);
                                loZoneDetail.ParentList = this;
                                loZoneDetail.ShowDialog();
                                break;
                            case "Collector":
                                CollectorDetailUI loCollectorDetail = new CollectorDetailUI(lRecord);
                                loCollectorDetail.ParentList = this;
                                loCollectorDetail.ShowDialog();
                                break;
                            case "Product":
                                ProductDetailUI loProductDetail = new ProductDetailUI(lRecord);
                                loProductDetail.ParentList = this;
                                loProductDetail.ShowDialog();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnUpdate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Remove"))
                {
                    return;
                }
                if (lRecord.Length > 0)
                {
                    if (lRecord[0].ToString() != null)
                    {
                        DialogResult _dr = new DialogResult();
                        MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                        _mb.ShowDialog();
                        _dr = _mb.Operation;
                        if (_dr == DialogResult.Yes)
                        {
                            object[] param = { lRecord[0].ToString() };
                            if ((bool)lObject.GetType().GetMethod("remove").Invoke(lObject, param))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI(lType.Name + " has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh("ViewAll", "", "", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRemove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Search"))
                {
                    return;
                }

                string _DisplayFields = "";
                string _WhereFields = "";
                string _Alias = "";

                switch (lType.Name)
                {
                    case "Client":
                        _DisplayFields = "SELECT Id,Lastname,Firstname,Middlename,Nickname,CellphoneNo AS `Cellphone No.`, "+
		                    "DATE_FORMAT(Birthday,'%m-%d-%Y') AS `Date`, "+
		                    "SitioPurokStreet AS `Sitio/Purok/Street`,Barangay,TownCity AS `Town/City`, "+
                            "Province,YearsOfStay AS `Years of Stay`,HomeType AS `Home Type`,Remarks " +
		                    "FROM `client` ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Lastname,Firstname ASC;";
                        _Alias = "";
                        break;
                    case "Area":
                        _DisplayFields = "SELECT a.Id,a.Code,a.Description, "+
		                    "CONCAT(e.Firstname,' ',SUBSTRING(e.Middlename, 1, 1),'. ',e.Lastname) AS `Area Manager`,a.Remarks "+
		                    "FROM `area` a "+
                            "LEFT JOIN employee e " +
		                    "ON a.AreaManager = e.Id ";
                        _WhereFields = " AND a.Status = 'Active' ORDER BY a.Description ASC;";
                        _Alias = "";
                        break;
                    case "Branch":
                        _DisplayFields = "SELECT b.Id,b.Code,b.Description,a.Description AS `Area`, "+
		                    "CONCAT(e.Firstname,' ',SUBSTRING(e.Middlename, 1, 1),'. ',e.Lastname) AS `Branch Manager`,b.Remarks "+
		                    "FROM branch b "+
		                    "LEFT JOIN `area` a "+
		                    "ON b.AreaId = a.Id "+
                            "LEFT JOIN employee e " +
		                    "ON b.BranchManager = e.Id ";
                        _WhereFields = " AND b.Status = 'Active' ORDER BY b.Description ASC;";
                        _Alias = "";
                        break;
                    case "Zone":
                        _DisplayFields = "SELECT z.Id,z.Description,b.Description AS `Branch`,z.Remarks "+
		                    "FROM zone z "+
                            "LEFT JOIN branch b " +
		                    "ON z.BranchId = b.Id ";
                        _WhereFields = " AND z.Status = 'Active' ORDER BY z.Description ASC;";
                        _Alias = "";
                        break;
                    case "Collector":
                        _DisplayFields = "SELECT c.Id,CONCAT(e.Firstname,' ',SUBSTRING(e.Middlename, 1, 1),'. ',e.Lastname) AS `Employee Name`, "+
		                    "b.Description AS Branch, c.Remarks "+
		                    "FROM collector c "+
		                    "LEFT JOIN employee e "+
		                    "ON c.EmployeeId = e.Id "+
                            "LEFT JOIN branch b " +
		                    "ON c.BranchId = b.Id ";
                        _WhereFields = " AND c.Status = 'Active' ORDER BY e.Firstname ASC;";
                        _Alias = "";
                        break;
                    case "Product":
                        _DisplayFields = "SELECT Id,`Code`,Description,PaymentFrequency AS `Payment Frequency`, "+
                            "Terms,InterestRate AS `Interest Rate`,ServiceFeeRate AS `Service Fee Rate`,Remarks " +
		                    "FROM product ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                }
                loSearches.lAlias = _Alias;
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtShow = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvLists, ldtShow);
                    lFromRefresh = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSearch_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Preview"))
                {
                    return;
                }
                if (dgvLists.Rows.Count != 0)
                {
                    switch (lType.Name)
                    {
                        case "Client":
                            /*
                            ChartOfAccountRpt loChartOfAccountRpt = new ChartOfAccountRpt();
                            loChartOfAccountRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loChartOfAccountRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loChartOfAccountRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loChartOfAccountRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loChartOfAccountRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loChartOfAccountRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loChartOfAccountRpt.SetParameterValue("Title", "Chart of Account List");
                            loChartOfAccountRpt.SetParameterValue("SubTitle", "Chart of Account List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loChartOfAccountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loChartOfAccountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loChartOfAccountRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loChartOfAccountRpt;
                            loReportViewer.ShowDialog();
                            */
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvLists_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < lCountCol; i++)
                {
                    lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
                }
            }
            catch { }
        }

        private void dgvLists_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Drawing.Point pt = dgvLists.PointToScreen(e.Location);
                cmsFunction.Show(pt);
            }
        }

        private void tsmiViewAllRecords_Click(object sender, EventArgs e)
        {
            //GlobalFunctions.refreshAll(ref dgvLists, ldtShow);
            try
            {
                dgvLists.Rows.Clear();
                dgvLists.Columns.Clear();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
            try
            {
                dgvLists.DataSource = ldtShow;
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Search"))
            {
                return;
            }
            refresh("", "", txtSearch.Text, true);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnUpdate_Click(null, new EventArgs());
            }
        }

        private void dgvLists_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void dgvLists_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvLists.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvLists.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Code" || 
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Years of Stay" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Terms")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Interest Rate" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Service Fee Rate")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch { }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }

        private void tsmiCreate_Click(object sender, EventArgs e)
        {
            btnCreate_Click(null, new EventArgs());
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemove_Click(null, new EventArgs());
        }

        private void tmsiSearch_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void tmsiPreview_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
        }
    }
}
