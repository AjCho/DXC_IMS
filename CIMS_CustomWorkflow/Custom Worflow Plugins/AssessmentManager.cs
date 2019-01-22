using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using CIMS_CustomWorkflow.Model;
using Microsoft.Xrm.Sdk.Query;

namespace CIMS_CustomWorkflow.Custom_Worflow_Plugins
{
    public class Assessment_Manager : CodeActivity
    {
        // Variables CRM Logical Names
        protected const string dxc_assesmentmanager = "dxc_assesmentmanager";
        protected const string dxc_name = "dxc_name";
        protected const string dxc_criteriatype = "dxc_criteriatype";

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);



            try
            {
                Entity entity = (Entity)context.InputParameters["Target"];


                //TODO: Do stuff
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public EntityCollection GetAllAssessmentManagers(ITracingService tracer, IOrganizationService service)
        {
            try
            {
                EntityCollection assessmentManager = null;
                dxc_assesmentmanager dxc_assesmentmanagers = new dxc_assesmentmanager();

                QueryExpression query = new QueryExpression() { };
                query.EntityName = dxc_assesmentmanagers.LogicalName;
                query.ColumnSet = new ColumnSet(dxc_name, dxc_criteriatype);

                assessmentManager = service.RetrieveMultiple(query);
                return assessmentManager;
            }
            catch (Exception e)
            {
                tracer.Trace("e.Message");
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}