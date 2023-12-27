using BLL.ManagerServices.Abstracts;
using DAL.Repositories.Abstracts;
using ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ManagerServices.Concretes
{
    public class ContactManager : BaseManager<Contact>, IContactManager
    {
        IContactRepository _contactRepository;

        public ContactManager(IContactRepository contactRepository) : base(contactRepository)
        {
            _contactRepository = contactRepository;
        }
    }
}