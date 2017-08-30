using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Tc.Crm.CustomWorkflowSteps.GetUsersStore;

namespace Tc.Crm.UnitTests.CustomWorkFlowSteps.GetUsersStore.Service
{
    [TestClass]
    public class GetUserStoreServiceTest
    {
        private XrmFakedContext _fakedContext;
        private IOrganizationService _service;
        private readonly Guid _storeId = Guid.NewGuid();
        private readonly Guid _externalLoginId = Guid.NewGuid();
        private readonly Guid _ownerId = Guid.NewGuid();

        [TestInitialize]
        public void Setup()
        {
            _fakedContext = new XrmFakedContext();
            _service = _fakedContext.GetFakedOrganizationService();
            _fakedContext.Data = new Dictionary<string, Dictionary<Guid, Entity>>();

            #region Stores
            var stores = new Dictionary<Guid, Entity>();
            var store = new Entity("tc_store");
            store["tc_storeid"] = _storeId;
            store.Id = _storeId;
            store["tc_budgetcentre"] = "20345";
            store["tc_name"] = "York (0345)";
            stores.Add(_storeId, store);
            #endregion

            #region Logins
            var externalLogins = new Dictionary<Guid, Entity>();
            var login = new Entity("tc_externallogin");
            login.Id = _externalLoginId;
            login["tc_externalloginid"] = _externalLoginId;
            login["tc_branchcode"] = "20345";
            login["ownerid"] = new EntityReference("systemuser", _ownerId);
            login["tc_budgetcentreid"] = new EntityReference("tc_store", _storeId);
            externalLogins.Add(_externalLoginId, login);
            #endregion

            _fakedContext.Data.Add("tc_store", stores);
            _fakedContext.Data.Add("tc_externallogin", externalLogins);
        }

        [TestMethod]
        public void GetStoreByUserTest()
        {
            _fakedContext.ProxyTypesAssembly = Assembly.GetAssembly(typeof(CrmEarlyBound.Contact));
            var user = new EntityReference("systemuser", _ownerId);
            var inputs = new Dictionary<string, object>()
            {
                {"User", user}
            };
            var store = _fakedContext.ExecuteCodeActivity<GetUsersStoreActivity>(inputs);
            Assert.AreEqual(((EntityReference)store["Store"]).Id, _storeId);
        }

        [TestMethod]
        public void GetStoreByUserEmptyTest()
        {
            _fakedContext.ProxyTypesAssembly = Assembly.GetAssembly(typeof(CrmEarlyBound.Opportunity));
            var user = new EntityReference("systemuser", Guid.NewGuid());
            var inputs = new Dictionary<string, object>
            {
                {"User", user}
            };
            var store = _fakedContext.ExecuteCodeActivity<GetUsersStoreActivity>(inputs);
            Assert.IsNull((EntityReference)store["Store"]);
        }
    }
}
