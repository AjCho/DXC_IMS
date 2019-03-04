using CIMS_CustomWorkflow.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMS_CustomWorkflow.Business_Logic
{
    class BL_Systemuser
    {
        // Variables
        protected const string systemuser = "systemuser";
        public const string systemuserid = "systemuserid";
        protected const string dxc_businessarea = "dxc_businessarea";
        protected const string dxc_interviewertype = "dxc_interviewertype";
        
        //protected const string
        public EntityCollection GetSystemusersViaBusinessArea(ITracingService tracer, IOrganizationService service, int businessArea, int interviewerType)
        {
            try
            {
                EntityCollection ec_SystemUser = null;
                SystemUser systemusers = new SystemUser();
                
                QueryExpression query = new QueryExpression() { };

                query.EntityName = systemusers.LogicalName;
                query.ColumnSet = new ColumnSet(dxc_businessarea, dxc_interviewertype);
                query.Criteria = new FilterExpression();
                query.Criteria.AddCondition(dxc_businessarea, ConditionOperator.Equal, businessArea);
                query.Criteria.AddCondition(dxc_interviewertype, ConditionOperator.Equal, interviewerType);

                ec_SystemUser = service.RetrieveMultiple(query);
                return ec_SystemUser;
            }
            catch (Exception e)
            {
                tracer.Trace("e.Message");
                throw new InvalidPluginExecutionException(e.Message);
            }

        }







    }
}
