using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicada.Account
{
    public partial class UserRole : System.Web.UI.Page
    {
        private void BindUsersToUserList()
        {
            // Get all of the user accounts 
            MembershipUserCollection users = Membership.GetAllUsers();
            UserList.DataSource = users;
            UserList.DataBind();
        }

        private void BindRolesToList()
        {
            // Get all of the roles 
            string[] roles = Roles.GetAllRoles();
            UsersRoleList.DataSource = roles;
            UsersRoleList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Bind the users and roles 
                BindUsersToUserList();
                BindRolesToList();
            } 
        }
    }
}