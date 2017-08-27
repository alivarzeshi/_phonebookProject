using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication2.Entity;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class PhoneService : IPhoneServices
    {
        private PhoneBookDB _context;

        public PhoneService(PhoneBookDB context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetContact()
        {
            return _context.Persons
               .OrderBy(a => a.Firstname)
               .ThenBy(a => a.Lastname)
               .ToList();
        }

        public Person GetContactById(string PhoneNumber)
        {
            Person contact = _context.Persons.Where(x => x.PhoneNumber == PhoneNumber).Single();
            return contact;
        }

        public void AddContact(Person person)
        {
            person.Id = Guid.NewGuid();
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void DeleteContact(string phoneNumber)
        {
            var contact = _context.Persons.Where(x => x.PhoneNumber == phoneNumber).Single();
            _context.Persons.Remove(contact);
            _context.SaveChanges();
        }

        public void UpdateContact(Person person)
        {
            var contact = _context.Persons.Where(x => x.Id == person.Id);
            _context.Entry(person).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}