using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using ThreeSeventy.Vector.Client.Models;

namespace ThreeSeventy.Vector.Client.Tests
{
    [TestFixture]
    public class ContactTest
    {
        private readonly int m_accountId;

        private readonly int m_contactId;

        private T70Context m_context;
        private IRepository<Contact> m_contactRepo;
        
        public ContactTest()
        {
            m_accountId = ConfigHelper.GetValue<int>("mainAccountId");
            m_contactId = ConfigHelper.GetValue<int>("constantContactId");
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            m_context = new T70Context(new MockConfig());
            m_contactRepo = m_context.Repository<Contact>(new { AccountId = m_accountId });
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            // Remove all contacts except the constant.
            var contacts = m_contactRepo.GetAll().ToList();

            contacts.RemoveAll(c => c.Id == m_contactId);

            if (contacts.Any())
                m_contactRepo.DeleteAll(contacts);
        }
        
        [Test] // Create single new contact
        public void ContactAdd()
        {
            var contact = new Contact
            {
                Email = RanGen.Email
            };

            Assert.DoesNotThrow(() => m_contactRepo.Add(contact));
        }
        
        [Test] // Create multiple contacts
        public void ContactAddRange()
        {
            var contactList = new List<Contact>
            {                                                   
                new Contact { PhoneNumber = RanGen.UsaNumber },
                new Contact { PhoneNumber = RanGen.UsaNumber }
            };

            Assert.DoesNotThrow(() => m_contactRepo.AddRange(contactList));

            foreach (var contact in contactList)
                Assert.AreNotEqual(0, contact.Id, "Contact ID was not filled in!");
        }

        [Test] // Get details of a contact
        public void ContactGet()
        {
            var contact = m_contactRepo.Get(m_contactId);

            Assert.NotNull(contact, "Constant contact not found!");
        }

        [Test] // Get details of all created contacts
        public void ContactGetAll()
        {
            List<Contact> contactList = (m_contactRepo.GetAll()).ToList();

            Assert.IsNotEmpty(contactList, "No contacts found!  We should have gotten the constant contact at least");
        }

        [Test] // Update contact
        public void ContactUpdate()
        {
            var contact = new Contact
            {
                PhoneNumber = RanGen.UsaNumber,
                Email = RanGen.Email
            };

            Assert.DoesNotThrow(() => m_contactRepo.Add(contact));

            var c2 = m_contactRepo.Get(contact.Id);

            Assert.IsNotNull(c2, "Contact for update test was not properly created");

            while (c2.PhoneNumber == contact.PhoneNumber)
                c2.PhoneNumber = RanGen.UsaNumber;

            m_contactRepo.Update(c2);

            // Repull the contact to verify was actually changed.
            c2 = m_contactRepo.Get(contact.Id);

            Assert.AreNotEqual(contact.PhoneNumber, c2.PhoneNumber, "Contact was not properly updated");
        }

        [Test] // Update multiple contacts
        public void ContactUpdateRange()
        {
            var contacts = m_contactRepo.GetAll().ToList();

            contacts.RemoveAll(c => c.Id == m_contactId);

            if (!contacts.Any())
            {
                for (int i = 0; i < 2; ++i)
                {
                    var c = new Contact
                    {
                        PhoneNumber = RanGen.UsaNumber,
                        Email = RanGen.Email
                    };

                    contacts.Add(c);
                }

                m_contactRepo.AddRange(contacts);

                foreach (var c in contacts)
                    Assert.AreNotEqual(0, c.Id, "Unable to properly add new contacts for ContactUpdateRange");
            }

            var updateList = new List<Contact>();

            foreach (var c in contacts)
            {
                var updateContact = new Contact
                {
                    Id = c.Id,
                    PhoneNumber = RanGen.UsaNumber,
                    Email = c.Email
                };

                while (c.PhoneNumber == updateContact.PhoneNumber)
                    updateContact.PhoneNumber = RanGen.UsaNumber;

                updateList.Add(updateContact);
            }

            m_contactRepo.UpdateRange(updateList);

            var items = m_contactRepo.GetAll().ToDictionary(c => c.Id, c => c);

            foreach (var c in contacts)
            {
                Contact updated;

                if (items.TryGetValue(c.Id, out updated))
                {
                    Assert.AreNotEqual(
                        c.PhoneNumber,
                        updated.PhoneNumber,
                        String.Format("Contact {0} was not properly updated!", c.Id));
                }
                else
                    Assert.Fail("Unable to fetch contact {0}", c.Id);
            }
        }

        [Test] // Delete contact
        public void ContactDelete()
        {
            var contact = new Contact
            {
                PhoneNumber = RanGen.UsaNumber
            };

            Assert.DoesNotThrow(() => m_contactRepo.Add(contact));

            // Assert was created property
            var c2 = m_contactRepo.Get(contact.Id);

            Assert.IsNotNull(c2, "New contact for delete test was not created properly!");

            m_contactRepo.Delete(contact);

            c2 = m_contactRepo.Get(contact.Id);

            Assert.AreNotEqual(
                ResourceStatus.Active,
                c2.Status,
                String.Format("Contact {0} was not properly deleted", contact.Id));
        }

        [Test] // Delete all contact
        public void ContactDeleteAll()
        {
            var contacts = m_contactRepo.GetAll().ToList();

            // Don't delete our constant contact
            contacts.RemoveAll(c => c.Id == m_contactId);

            if (!contacts.Any())
            {
                // Create at least one new contact
                var newContact = new Contact
                {
                    PhoneNumber = RanGen.UsaNumber,
                    Email = RanGen.Email
                };

                Assert.DoesNotThrow(
                    () => m_contactRepo.Add(newContact),
                    "Could not add a new contact for the test");

                contacts.Add(newContact);
            }

            Assert.DoesNotThrow(
                () => m_contactRepo.DeleteAll(contacts),
                "Unable to delete all contacts in list");

            var repull = m_contactRepo.GetAll().ToList();

            Assert.AreEqual(1, repull.Count());
        }

        [Test] // Search contact
        public void ContactSearch()
        {
            var contact = m_contactRepo.Get(m_contactId);

            Assert.IsNotNull(contact, "Could not locate constant contact for search test");

            // Build a search string we know will pull in our constant contact.
            string searchPattern = contact.PhoneNumber.Substring(0, 5) + "*";

            List<Contact> contacts = null;

            Assert.DoesNotThrow(
                () =>
                {
                    contacts = m_context.ContactSearch(m_accountId, searchPattern).ToList();
                }, "Searching threw an exception");

            Assert.IsNotNull(contacts, "Search returned a NULL value");

            Assert.IsNotEmpty(
                contacts,
                "No contacts found on search.  Our constant contact should have been found.");
        }
    }
}
