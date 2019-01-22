using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using CIMS_CustomWorkflow.Model;
using CIMS_CustomWorkflow.Helper;
using Microsoft.Xrm.Sdk.Query;
using CIMS_CustomWorkflow.Business_Logic;

namespace CIMS_CustomWorkflow.Custom_Worflow_Plugins
{
    public class Interviewer : CodeActivity
    {
        Helper.Helper help = new Helper.Helper();
        SystemUser systemuser = new SystemUser();
        BL_Interviewer interviewer = new BL_Interviewer();
        // 
        protected const string systemuserid = "systemuserid";
        protected const string fullname = "";

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

       

    }
}