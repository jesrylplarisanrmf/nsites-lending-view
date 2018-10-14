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
    public partial class BranchDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[6];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        Branch loBranch;
        Area loArea;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public BranchDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loBranch = new Branch();
            loArea = new Area();
        }
        public BranchDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loBranch = new Branch();
            loArea = new Area();;
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
            txtCode.Clear();
            txtDescription.Clear();
            cboArea.Text = "";
            txtBranchManager.Clear();
            txtRemarks.Clear();
            txtCode.Focus();
        }
        #endregion "END OF METHODS"

        private void BranchDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                try
                {
                    cboArea.DataSource = loArea.getAllData("ViewAll","","");
                    cboArea.DisplayMember = "Description";
                    cboArea.ValueMember = "Id";
                    cboArea.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    txtCode.ReadOnly = true;
                    txtCode.BackColor = SystemColors.Control;
                    txtCode.TabStop = false;
                    txtDescription.Text = lRecords[2];
                    cboArea.Text = lRecords[3];
                    txtBranchManager.Text = lRecords[4];
                    txtRemarks.Text = lRecords[5];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "BranchDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loBranch.Id = lId;
                loBranch.Code = txtCode.Text;
                loBranch.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                try
                {
                    loBranch.AreaId = cboArea.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select an Area!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboArea.Focus();
                    return;
                }
                loBranch.BranchManager = GlobalFunctions.replaceChar(txtBranchManager.Text);
                
                loBranch.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loBranch.UserId = GlobalVariables.UserId;

                string _Id = loBranch.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Branch has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtDescription.Text;
                    lRecords[3] = cboArea.Text;
                    lRecords[4] = txtBranchManager.Text;
                    lRecords[5] = txtRemarks.Text;
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
