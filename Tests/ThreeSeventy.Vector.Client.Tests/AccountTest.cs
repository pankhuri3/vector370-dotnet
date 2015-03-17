using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using ThreeSeventy.Vector.Client.Models;

namespace ThreeSeventy.Vector.Client.Tests
{
    [TestFixture]
    public class AccountTest
    {
        private const int PARENT_ID = 119;
        private static int s_subSubAccount1;
        private static int s_subSubAccount2;

        [Test]//Create a new account under the parent account
        public void A_AccountAdd()
        {
            var context = new T70Context();
            var accountRepo = context.Repository<Account>(new { });
            
            var account = new Account
            {
                ParentId = PARENT_ID,
                Name = RanGen.Str
            };
            accountRepo.Add(account);
            LookupTable.AccountId = account.Id;

            //try
            //{
            //    accountRepo.Add(account);
            //    Assert.IsInstanceOf(typeof(Account), account);
            //}
            //catch
            //{
            //    Assert.Fail("Test failed. Can't create Account");
            //}

        }

        [Test]//Create multiple account under the parent account
        public void B_AccountAddRange()
        {
            var context = new T70Context();
            var accountRepo = context.Repository<Account>(new { });
            var accountList = new List<Account>
            {                                                   
                new Account { ParentId = PARENT_ID, Name = RanGen.Str },
                new Account { ParentId = PARENT_ID, Name = RanGen.Str }
            };

            accountRepo.AddRange(accountList);
            s_subSubAccount1 = accountList[0].Id;
            s_subSubAccount2 = accountList[1].Id;
        }

        [Test]//Get details of created account
        public void C_AccountGet()
        {
            var context = new T70Context();
            var accountRepo = context.Repository<Account>(new { ParentId = PARENT_ID });
            accountRepo.Get(LookupTable.AccountId);
        }

        [Test]//Get details of all created account
        public void D_AccountGetAll()
        {
            var context = new T70Context();
            var accountRepo = context.Repository<Account>(new { ParentId = PARENT_ID });
            List<Account> contactlist = (accountRepo.GetAll()).ToList();
        }

        [Test]//Update account
        public void E_AccountUpdate()
        {
            var context = new T70Context();
            var accountRepo = context.Repository<Account>(new { ParentId = PARENT_ID });

            var account = new Account
            {
                Id = LookupTable.AccountId,
                Name = RanGen.Str
            };
            accountRepo.Update(account);
        }

        [Test]//Update multiple account
        public void F_AccountUpdateRange()
        {
            var context = new T70Context();
            var accountRepo = context.Repository<Account>(new {});
            var accountList = new List<Account>
            {
                new Account() {Id = s_subSubAccount1, ParentId = PARENT_ID, Name = RanGen.Str},
                new Account() {Id = s_subSubAccount2, ParentId = PARENT_ID, Name = RanGen.Str}
            };
            accountRepo.UpdateRange(accountList);
        }

        [Test]//Delete account
        public void G_AccountDelete()
        {
            var context = new T70Context();
            var accountRepo = context.Repository<Account>(new { ParentId = PARENT_ID });
            var account = new Account
            {
                Id = LookupTable.AccountId
            };
            accountRepo.Delete(account);
        }
    }
}
