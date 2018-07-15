using FileZillaServerBLL;
using FileZillaServerModel;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yiliangyijia.Comm;

namespace FileZillaServerWeb
{
    public partial class MaterialConfig : System.Web.UI.Page
    {
        CerficateBLL cBll = new CerficateBLL();
        protected List<Cerficate> lstCerficate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCerficate();
            }
        }

        protected void LoadCerficate()
        {
            List<Cerficate> lstCrt = cBll.GetModelList(" employeeID = '" + Request.QueryString["employeeID"] + "' ");
            lstCerficate = lstCrt;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fup.HasFile)
            {
                string employeeID = UserProfile.GetInstance().ID;
                string fileNamePrefix = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string fileName = string.Format("{0}_{1}", fileNamePrefix, fup.FileName);
                string cerficateName = txtCerficateName.Text.Trim();
                string description = txtDescription.Text.Trim();
                string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                string fileSavePath = ConfigurationManager.AppSettings["fileSavePath"].ToString();
                string thumbnailsPath = ConfigurationManager.AppSettings["thumbnailsPath"].ToString();
                string savePath = Path.Combine(rootPath, fileSavePath, fileName);
                try
                {
                    fup.SaveAs(savePath);
                    string fileThumbnails = Path.Combine(rootPath, thumbnailsPath, fileName);
                    System.Drawing.Image img = new Bitmap(savePath, true);
                    //aFile.Close();
                    System.Drawing.Image imgThumbnails = new ImageHelper().GetHvtThumbnail(img, 800, 0);

                    imgThumbnails.Save(fileThumbnails, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    return;
                }
                Cerficate crf = new Cerficate();
                crf.ID = Guid.NewGuid().ToString();
                crf.EMPLOYEEID = employeeID;
                crf.CERTIFICATENAME = cerficateName;
                crf.FILEPATH = fileName;
                crf.DESCRIPTION = description;
                crf.ISMAIN = cbxIsMain.Checked;
                cBll.Add(crf);
                if (cbxIsMain.Checked)
                {
                    cBll.SetIsMainToFalseExceptID(crf.ID);
                }
                LoadCerficate();
            }
        }
    }
}