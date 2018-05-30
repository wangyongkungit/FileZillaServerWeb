using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileZillaServerProfile
{
    [Serializable]
    public class UserProfile
    {
        /// <summary>
        /// 用户ID（唯一标识）
        /// </summary>
        public string ID { get; set; }

        public string EmployeeNO { get; set; }

        public string Name { get; set; }

        public string MenuName { get; set; }

        public List<Menu> Menu { get; set; }

        public List<Role> Role { get; set; }

        public static UserProfile instance
        {
            get
            {
                if (HttpContext.Current.Session["Profile"] == null)
                {
                    try
                    {
                        HttpCookie UserProfile_ck = HttpContext.Current.Request.Cookies["Profile"];
                        //if (UserProfile_ck != null)
                        //{
                        //    string v = PASCEncryptUtil.DesEncrypt(UserProfile_ck.Value);
                        //    HttpContext.Current.Session["PASCProfileSP"] = GetUserProfileByUserId(v);
                        //}
                        //else
                        //    return null;
                        if (UserProfile_ck == null || string.IsNullOrEmpty(UserProfile_ck.Value))
                        {
                            return null;
                        }
                        else
                        {
                            //string v = PASCEncryptUtil.DesEncrypt(UserProfile_ck.Value);
                            HttpContext.Current.Session["Profile"] = GetUserProfileByUserID(UserProfile_ck.Value);
                        }
                    }
                    catch { return null; }

                }
                return (UserProfile)HttpContext.Current.Session["Profile"];
            }
            set
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();


                HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

                HttpContext.Current.Session["Profile"] = value;
                UserProfile user = (UserProfile)value;
                //string v = PASCEncryptUtil.EncryptString(user.UserId);
                HttpCookie UserCookie = new HttpCookie("Profile", user.EmployeeNO);
                UserCookie.HttpOnly = true;
                UserCookie.Expires = DateTime.Now.AddHours(3);
                UserCookie.Path = "/";
                HttpContext.Current.Response.Cookies.Add(UserCookie);
            }
        }

        public static UserProfile GetUserProfileByUserID(string employeeNO)
        {
            DataTable dtUser = GetDataSet.GetUesrProfileByUserID(employeeNO);
            UserProfile user=new UserProfile();
            user.ID = dtUser.Rows[0]["ID"].ToString();
            user.Name = dtUser.Rows[0]["name"].ToString();
            user.EmployeeNO = dtUser.Rows[0]["employeeno"].ToString();
            List<Menu> lstMenu = new List<Menu>();
            Menu menu = new Menu();
            int dtUserRowsCount = dtUser.Rows.Count;
            for (int i = 0; i < dtUserRowsCount; i++)
            {
                menu = new Menu();
                menu.Name = dtUser.Rows[i]["MENUNAME"].ToString();
                menu.Path = dtUser.Rows[i]["MENUPATH"].ToString();
                menu.ParentID = dtUser.Rows[i]["PARENTID"].ToString();
                menu.Remarks = dtUser.Rows[i]["REMARKS"].ToString();
                lstMenu.Add(menu);
            }
            user.Menu = lstMenu;
            List<Role> lstRole = new List<Role>();
            Role role = new Role();
            for (int i = 0; i < dtUserRowsCount; i++)
            {
                role = new Role();
                role.RoleID = dtUser.Rows[i]["roleID"].ToString();
                role.RoleName = dtUser.Rows[i]["roleName"].ToString();
                lstRole.Add(role);
            }
            user.Role = lstRole;
            return user;
        }

        /// <summary>
        /// 根据员工编号和密码获取用户
        /// </summary>
        /// <param name="employeeNO">员工编号</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static UserProfile GetUserProfileByUserIDNew(string employeeNO, string passWord)
        {
            DataTable dtUser = GetDataSet.GetUesrProfileByUserIDandPwdNew(employeeNO, passWord);
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                UserProfile user = new UserProfile();
                user.ID = dtUser.Rows[0]["ID"].ToString();
                user.Name = dtUser.Rows[0]["name"].ToString();
                user.EmployeeNO = dtUser.Rows[0]["employeeno"].ToString();
                List<Menu> lstMenu = new List<Menu>();
                Menu menu = new Menu();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    menu = new Menu();
                    menu.Name = dtUser.Rows[i]["MENUNAME"].ToString();
                    menu.Path = dtUser.Rows[i]["MENUPATH"].ToString();
                    menu.ParentID = dtUser.Rows[i]["PARENTID"].ToString();
                    menu.Remarks = dtUser.Rows[i]["REMARKS"].ToString();
                    lstMenu.Add(menu);
                }
                user.Menu = lstMenu;
                List<Role> lstRole = new List<Role>();
                Role role = new Role();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    role = new Role();
                    role.RoleID = dtUser.Rows[i]["roleID"].ToString();
                    role.RoleName = dtUser.Rows[i]["roleName"].ToString();
                    lstRole.Add(role);
                }
                user.Role = lstRole;
                return user;
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetUserProfileByUserIDandPwd(string employeeNO, string password)
        {
            return GetDataSet.GetUesrProfileByUserIDandPwd(employeeNO, password);
        }

        /// <summary>
        /// 根据员工编号和密码获取用户（新）
        /// </summary>
        /// <param name="employeeNO"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static DataTable GetUesrProfileByUserIDandPwdNew(string employeeNO, string password)
        {
            return GetDataSet.GetUesrProfileByUserIDandPwdNew(employeeNO, password);
        }

        public static UserProfile GetInstance()
        {
            if (instance == null)
            {
                HttpContext.Current.Response.Redirect("Tip.html", true);
                return null;
            }
            else
            {
                return instance;
            }
        }
    }
}
