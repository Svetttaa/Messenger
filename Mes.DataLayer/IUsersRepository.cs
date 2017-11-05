using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Mes.Model;

namespace Mes.DataLayer
{
    public interface IUsersRepository
    {
        User Create(User user);
        User Login(string name,string password);
        User Get(Guid id);
        void Delete(Guid id);
        void ChangeAvatar(Guid id,Image file);
        void ChangeName(Guid id,string name);
        void ChangePassword(Guid id,string pass);
    }
}
