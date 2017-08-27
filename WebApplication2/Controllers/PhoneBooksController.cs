using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Services;
using WebApplication2.Entity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/phonebook")]
    public class PhoneBooksController : ApiController
    {

        private readonly IPhoneServices _phoneservices;
        private bool disposed = false;

        public PhoneBooksController()
        {
            _phoneservices = new PhoneService(new PhoneBookDB());
        }


        /// <summary>
        /// Get all Contact
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllContact()
        {
            var ContactList = _phoneservices.GetContact();

            if (ContactList == null)
                return null;

            return Ok(ContactList);
        }


        /// <summary>
        /// Get list of Contact
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{phoneNumber}")]
        public IHttpActionResult GetContactByPhoneNo([FromUri] string phoneNumber)
        {
            Person findContact = _phoneservices.GetContactById(phoneNumber);

            if (findContact != null)
                return Ok(findContact);

            return NotFound();
        }


        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateContact([FromBody] Person contact)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _phoneservices.AddContact(contact);
            return Ok($"contact {contact.PhoneNumber} successful Added");
        }


        /// <summary>
        /// Update Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateContact([FromBody] Person contact)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (contact == null)
                return NotFound();

            _phoneservices.UpdateContact(contact);
            return Ok($"Contact with ID {contact.Id} successful updated.");
        }


        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{phoneNumber}")]
        public IHttpActionResult DeleteContact([FromUri]string phoneNumber)
        {
            _phoneservices.DeleteContact(phoneNumber.Trim());
            return Ok($"Contact {phoneNumber} Successfull Deleted");
        }


        /// <summary>
        /// Dispose _context 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _phoneservices.Dispose();
            }
            disposed = true;
        }
    }
}
