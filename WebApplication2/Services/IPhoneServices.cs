using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IPhoneServices :IDisposable
    {
        IEnumerable<Person> GetContact();
        Person GetContactById(string personId);
        void AddContact(Person person);
        void UpdateContact(Person person);
        void DeleteContact(string phoneNumber);
    }
}
