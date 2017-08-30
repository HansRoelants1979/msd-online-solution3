using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Tc.Crm.CustomWorkflowSteps.Attributes;

namespace Tc.Crm.CustomWorkflowSteps.GetUsersStore.Service
{
    public class GetUserStoreService
    {
        public EntityReference GetStoreByUser(EntityReference user, IOrganizationService service, ITracingService trace)
        {
            var query = new QueryExpression
            {
                EntityName = EntityName.Store,
                ColumnSet = new ColumnSet(Stores.Name, Stores.BudgetCentre)
            };

            var linkLogin = new LinkEntity
            {
                LinkFromAttributeName = Stores.StoreId,
                LinkFromEntityName = EntityName.Store,
                LinkToEntityName = EntityName.ExternalLogin,
                LinkToAttributeName =  ExternalLogins.StoreId
            };
            var ownerCondition = new ConditionExpression
            {
                AttributeName = ExternalLogins.OwnerId,
                Operator = ConditionOperator.Equal
            };
            ownerCondition.Values.Add(user.Id);
            linkLogin.LinkCriteria.Conditions.Add(ownerCondition);
            query.LinkEntities.Add(linkLogin);
            trace.Trace("call retrievemultiple on stores");
            var entityCollection = service.RetrieveMultiple(query);
            return entityCollection.Entities.FirstOrDefault()?.ToEntityReference();
        }
    }
}
