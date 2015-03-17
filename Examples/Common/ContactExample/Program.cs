using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;

namespace ContactExample
{
    class Program
    {
        /*
         * NOTE:
         * Please review the various constants before you run this code and 
         * fill in the needed items with appropriate values.
         */

        // Account info
        private const int ACCOUNT_ID = 0;           // TODO: Fill in with your account ID
        private const int CONTACT_ID = 0;           // TODO: Fill in with an existing contact ID
        
        private const string PHONE_NUMBER = "+1";   // TODO: Fill in with a valid phone number
        private const string EMAIL_ADDRESS = "";

        private const string SEARCH_PATTERN = "+1512*";
        
        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on during execution.
            //InitLogging();

            // User name and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();

            //ContactCreateExample(context);
            //ContactAddRangeExample(context);
            //GetAllContactDetailsExample(context);
            //ContactDetailsExample(context);
            ContactSearchExample(context);
            //ContactUpdateExample(context);
            //ContactUpdateRangeExample(context);
            //ContactDeleteExample(context);
            //ContactDeleteAllExample(context);
            
            Console.WriteLine();
            Console.WriteLine("Done, press a key to quit.");
            Console.ReadKey();
        }
        
        private static void InitLogging()
        {
            var properties = new NameValueCollection();
            properties["showDateTime"] = "true";

            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
        }

        private static void DisplayContact(Contact contact)
        {
            Console.WriteLine("Id: {0}", contact.Id);
            Console.WriteLine("AccountId: {0}", contact.AccountId);
            Console.WriteLine("PhoneNumber: {0}", contact.PhoneNumber);
            Console.WriteLine("Email: {0}", contact.Email);
            Console.WriteLine("Attributes:");

            foreach (var attr in contact.Attributes)
                Console.WriteLine("  {0}: {1}", attr.Name, attr.Value);

            Console.WriteLine("Subscriptions:");

            foreach (var sub in contact.Subscriptions)
                Console.WriteLine("  {0}: SMS: {1}  EMAIL: {2}", sub.SubscriptionId, sub.SmsEnabled, sub.EmailEnabled);

            Console.WriteLine("Created: {0}", contact.Created);
            Console.WriteLine("Modified: {0}", contact.Modified);
            Console.WriteLine();
        }

        static void ContactCreateExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            var contact = new Contact
            {
                PhoneNumber = PHONE_NUMBER,
                Email = EMAIL_ADDRESS
            };

            contactRepo.Add(contact);

            Console.WriteLine("Added contact:");
            DisplayContact(contact);
        }

        static void ContactDetailsExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            var contact = contactRepo.Get(CONTACT_ID);

            Console.WriteLine("Got contact:");
            DisplayContact(contact);
        }

        static void ContactSearchExample(T70Context context)
        {
            Console.WriteLine("Searching for contacts matching: {0}", SEARCH_PATTERN);
            var contacts = context.ContactSearch(ACCOUNT_ID, SEARCH_PATTERN);

            Console.WriteLine("Found:");

            foreach (var c in contacts)
                Console.WriteLine("  {0}: {1}", c.Id, c.PhoneNumber);
        }

        static void GetAllContactDetailsExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            List<Contact> contactlist = (contactRepo.GetAll()).ToList();

            foreach (Contact contact in contactlist)
                DisplayContact(contact);
        }

        static void ContactUpdateExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            var contact = new Contact
            {
                Id = CONTACT_ID,
                PhoneNumber = PHONE_NUMBER,
                Email = EMAIL_ADDRESS
            };

            contactRepo.Update(contact);
            var updateContact = contactRepo.Get(CONTACT_ID);

            Console.WriteLine("Updated contact:");
            DisplayContact(updateContact);
        }
        
        static void ContactDeleteExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            var contact = new Contact
            {
                Id = CONTACT_ID
            };

            contactRepo.Delete(contact);

            var deleteContact = contactRepo.Get(CONTACT_ID);

            Console.WriteLine("Deleted Contact:");
            DisplayContact(contact);
        }

        static void ContactDeleteAllExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            contactRepo.DeleteAll(contactRepo.GetAll().ToList());

            Console.WriteLine("Delete all contact successfully");
        }

        static void ContactAddRangeExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            // TODO: Remove hard coded phone numbers!!!!!
            var contactList = new List<Contact>
            {
                new Contact() {PhoneNumber = "+1"},
                new Contact() {PhoneNumber = "+1"}
            };

            contactRepo.AddRange(contactList);
            foreach (Contact aContact in contactList)
            {
                Console.WriteLine("Added contact {0}: {1}", aContact.Id, aContact.PhoneNumber);
                Console.WriteLine();
            }
        }

        static void ContactUpdateRangeExample(T70Context context)
        {
            var contactRepo = context.Repository<Contact>(new { AccountId = ACCOUNT_ID });

            // TODO: Remove hard coded phone numbers and contact IDs
            var contactList = new List<Contact>
            {
                new Contact() { Id = 0, PhoneNumber = "+1", Email = ""},
                new Contact() { Id = 0, PhoneNumber = "+1", Email = ""}
            };

            contactRepo.UpdateRange(contactList);

            foreach (Contact aContact in contactList)
            {
                Console.WriteLine("Update contact {0}: {1}", aContact.Id, aContact.PhoneNumber);
                Console.WriteLine();
            }
        }
    }
}
