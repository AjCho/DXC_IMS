using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using CIMS_CustomWorkflow.Business_Logic;
using CIMS_CustomWorkflow.Model;
using CIMS_CustomWorkflow.Helper;
using Microsoft.Xrm.Sdk.Query;

namespace CIMS_CustomWorkflow.Custom_Worflow_Plugins
{
    public class Candidate_AddAnInterviewer : CodeActivity
    {
        // Variables
        Helper.Helper help = new Helper.Helper();
        BL_Interviewer interviewer = new BL_Interviewer();

        protected const string systemuserid = "systemuserid";
        protected const string firstname = "firstname";
        protected const string middlename = "";
        protected const string lastname = "";
        protected const string businessarea = "dxc_businessarea";
        protected const string interviewertype = "dxc_interviewertype";
        protected string fullname = "";

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                tracer.Trace("entity[businessarea] = " + entity[businessarea] + " | entity[interviewertype]" + entity[interviewertype]);
                AddAnInterviewer(tracer, service, (int)entity[businessarea], (int)entity[interviewertype], entity.Id);

            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        protected void AddAnInterviewer(ITracingService tracer, IOrganizationService service, int dxc_businessarea, int dxc_interviewertype, Guid candidateID)
        {
            try
            {
                BL_Systemuser bL_Systemuser = new BL_Systemuser();
                /// dxc_interviewertype:
                ///      Initial = 1
                ///      Final = 2   
                EntityCollection ec_Systemuser = bL_Systemuser.GetSystemusersViaBusinessArea(tracer, service, dxc_businessarea, dxc_interviewertype);
                foreach (Entity e in ec_Systemuser.Entities)
                {
                    //EntityReference systemuserID = e.GetAttributeValue<EntityReference>(su.SystemUserId.);
                    Guid systemUserId = e.GetAttributeValue<Guid>(systemuserid);
                    string firstName = e.GetAttributeValue<string>(firstname).ToString();
                    string middleName = e.GetAttributeValue<string>(middlename).ToString();
                    string lastName = e.GetAttributeValue<string>(lastname).ToString();
                    fullname = string.Concat(firstName, " ", middleName, " ", lastName);

                    tracer.Trace("systemuser.SystemUserId = " + systemUserId
                        + " | systemuser.FirstName = " + firstName
                        + " | systemuser.MiddleName = " + middleName
                        + " | systemuser.LastName = " + lastName);
                    /// Add the interviewer to the candidate
                    interviewer.CreateInterviewer(tracer, service, fullname, dxc_interviewertype, (Guid)systemUserId, candidateID);

                }
                tracer.Trace(help.SuccessfulTraceMsg("AddAnInterviewer"));
            }
            catch (Exception e)
            {
                tracer.Trace(help.UnsuccessfulTraceMsg("AddAnInterviewer"));
                throw new InvalidPluginExecutionException(e.Message);
            }
        }





    }
}