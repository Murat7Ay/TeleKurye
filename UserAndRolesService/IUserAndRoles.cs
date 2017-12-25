using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UserAndRolesService.Models;

namespace UserAndRolesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUser
    {

        [OperationContract]
        List<UserModel> GetAllList();

        [OperationContract]
        List<UserDetailModel> GetById(int usrId);

        [OperationContract]
        UserModel Create(UserModel usr);

        [OperationContract]
        string Update(UserModel usr);

        [OperationContract]
        string Delete(int usrId);

        [OperationContract]
        string AddUserRole(RoleOperations ar);

        [OperationContract]
        string RemoveUserRole(RoleOperations ar);

        [OperationContract]
        string UpdateUserRole(RoleOperations ar);
    }

    [ServiceContract]
    public interface IRoles
    {
        [OperationContract]
        List<UserAndRolesModel> UserAndRolesList();

        [OperationContract]
        List<RolesModel> GetAllRoleList();
        [OperationContract]
        List<RoleWithUsers> GetRoleById(int rId);
        [OperationContract]
        RolesModel CreateRole(RolesModel rl);
        [OperationContract]
        string UpdateRole(RolesModel rl);
        [OperationContract]
        string DeleteRole(int rId);
    }


}
