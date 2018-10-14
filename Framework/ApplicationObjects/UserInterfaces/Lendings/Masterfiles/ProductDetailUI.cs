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
    public partial class ProductDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[8];
        GlobalVariables.Operation lOperation;
        Product loProduct;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ProductDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loProduct = new Product();
        }
        public ProductDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loProduct = new Product();
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
            cboPaymentFrequency.Text = "";
            txtTerms.Text = "0";
            txtInterestRate.Text = "0.00";
            txtServiceFeeRate.Text = "0.00";
            txtRemarks.Clear();
            txtCode.Focus();
        }
        #endregion "END OF METHODS"

        private void ProductDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    txtCode.ReadOnly = true;
                    txtCode.BackColor = SystemColors.Control;
                    txtCode.TabStop = false;
                    txtDescription.Text = lRecords[2];
                    cboPaymentFrequency.Text = lRecords[3];
                    txtTerms.Text = lRecords[4];
                    txtInterestRate.Text = string.Format("{0:n}", decimal.Parse(lRecords[5]));
                    txtServiceFeeRate.Text = string.Format("{0:n}", decimal.Parse(lRecords[6]));
                    txtRemarks.Text = lRecords[7];
                }
                else
                {
                    cboPaymentFrequency.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ProductDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loProduct.Id = lId;
                loProduct.Code = txtCode.Text;
                loProduct.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                loProduct.PaymentFrequency = cboPaymentFrequency.Text;
                loProduct.Terms = int.Parse(txtTerms.Text);
                loProduct.InterestRate = decimal.Parse(txtInterestRate.Text);
                loProduct.ServiceFeeRate = decimal.Parse(txtServiceFeeRate.Text);
                loProduct.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loProduct.UserId = GlobalVariables.UserId;

                string _Id = loProduct.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Product has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtDescription.Text;
                    lRecords[3] = cboPaymentFrequency.Text;
                    lRecords[4] = txtTerms.Text;
                    lRecords[5] = decimal.Parse(txtInterestRate.Text).ToString();
                    lRecords[6] = decimal.Parse(txtServiceFeeRate.Text).ToString();
                    lRecords[7] = txtRemarks.Text;
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

        private void txtTerms_Leave(object sender, EventArgs e)
        {
            try
            {
                txtTerms.Text = string.Format("{0:0}", int.Parse(txtTerms.Text));
            }
            catch
            {
                txtTerms.Text = "0";
            }
        }

        private void txtInterestRate_Leave(object sender, EventArgs e)
        {
            try
            {
                txtInterestRate.Text = string.Format("{0:n}", decimal.Parse(txtInterestRate.Text));
            }
            catch
            {
                txtInterestRate.Text = "0";
            }
        }

        private void txtServiceFeeRate_Leave(object sender, EventArgs e)
        {
            try
            {
                txtServiceFeeRate.Text = string.Format("{0:n}", decimal.Parse(txtServiceFeeRate.Text));
            }
            catch
            {
                txtServiceFeeRate.Text = "0";
            }
        }
    }
}
