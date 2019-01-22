using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using CIMS_CW001_Candidate.Business_Logic;
using CIMS_CW001_Candidate.Helper;
using CIMS_Models;

namespace CIMS_CW001_Candidate
{
    public class AddAnInterviewer : CodeActivity
    {
        // Variables
        Helper.Helper help = new Helper.Helper();
        BL_Interviewer interviewer = new BL_Interviewer();

        protected const string systemuserid = "systemuserid";
        protected const string firstname = "firstname";
        protected const string businessarea = "dxc_businessarea";
        protected const string interviewertype = "dxc_interviewertype";
        protected const string fullname = "fullname";

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                OptionSetValue businessAreaValue = (OptionSetValue)entity[businessarea];
                /// Note: By default we will going to use the Initial = 1 for the InterviwerType.
                /// dxc_interviewertype:
                ///      Initial = 1
                ///      Final = 2   
                CreateInterviewer(tracer, service, businessAreaValue.Value, 1, entity.Id);

            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        protected void CreateInterviewer(ITracingService tracer, IOrganizationService service, int dxc_businessarea, int dxc_interviewertype, Guid candidateID)
        {
            try
            {
                BL_Systemuser bL_Systemuser = new BL_Systemuser();
                EntityCollection ec_Systemuser = bL_Systemuser.GetSystemusersViaBusinessArea(tracer, service, dxc_businessarea, dxc_interviewertype);

                foreach (Entity e in ec_Systemuser.Entities)
                {
                    tracer.Trace("Entity e in ec_Systemuser.Entities started!!! ");
                    Guid systemUserId = e.GetAttributeValue<Guid>(systemuserid);
                    string firstName = e.GetAttributeValue<string>(firstname).ToString();
                    string interviewerFullname = e.GetAttributeValue<string>(fullname).ToString();

                    tracer.Trace("systemuser.SystemUserId = " + systemUserId.ToString());
                    /// Add the interviewer to the candidate
                    interviewer.CreateInterviewer(tracer, service, interviewerFullname, dxc_interviewertype, systemUserId, candidateID);
                    tracer.Trace(help.SuccessfulTraceMsg("added " + interviewerFullname + "as an interviewer. "));
                }
            }
            catch (Exception e)
            {
                tracer.Trace(e.Message);
                tracer.Trace(help.UnsuccessfulTraceMsg("CreateInterviewer"));
                throw new InvalidPluginExecutionException(e.Message);
            }
            finally
            {
                tracer.Trace(help.SuccessfulTraceMsg("CreateInterviewer"));
            }
        }



    }
}