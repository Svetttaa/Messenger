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
        User Get(Guid id);
        void Delete(Guid id);
        bool ChangeAvatar(Guid id,Image file);
        bool ChangeName(Guid id,string name);
        bool ChangePassword(Guid id,string pass);
    }
}
