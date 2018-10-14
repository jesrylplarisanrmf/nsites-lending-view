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

namespace NSites_V.ApplicationObjects.UserInterfaces.Lendings.Masterfiles
{
    public partial class CollectorDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[4];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        Collector loCollector;
        Branch loBranch;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public CollectorDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loCollector = new Collector();
            loBranch = new Branch();
        }
        public CollectorDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loCollector = new Collector();
            loBranch = new Branch();
            lRecords = pRecords;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        private void clear()
        {
            lId = "";
            txtEmployeeName.Text = "";
            cboBranch.Text = "";
            txtRemarks.Clear();
            txtEmployeeName.Focus();
        }
        #endregion "END OF METHODS"

        private void CollectorDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                try
                {
                    cboBranch.DataSource = loBranch.getAllData("ViewAll","","");
                    cboBranch.DisplayMember = "Description";
                    cboBranch.ValueMember = "Id";
                    cboBranch.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtEmployeeName.Text = lRecords[1];
                    cboBranch.Text = lRecords[2];
                    txtRemarks.Text = lRecords[3];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CollectorDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loCollector.Id = lId;
                loCollector.EmployeeName = GlobalFunctions.replaceChar(txtEmployeeName.Text);
                
                try
                {
                    loCollector.BranchId = cboBranch.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Branch!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboBranch.Focus();
                    return;
                }
                
                loCollector.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loCollector.UserId = GlobalVariables.UserId;

                string _Id = loCollector.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Collector has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtEmployeeName.Text;
                    lRecords[2] = cboBranch.Text;
                    lRecords[3] = txtRemarks.Text;
                    object[] _params = { lRecords };
                    if (lOperation == GlobalVariables.Operation.Edit)
                    {
                        ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                        this.Close();
                    }
                    else
                    {
                        ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                        clear();
                    }
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Failure to save the record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
