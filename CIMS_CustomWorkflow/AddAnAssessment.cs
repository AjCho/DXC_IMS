using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using CIMS_CustomWorkflow.Model;
using CIMS_CustomWorkflow.Business_Logic;
using CIMS_CustomWorkflow.Helper;
using Microsoft.Xrm.Sdk.Query;

namespace CIMS_CustomWorkflow
{
    public class AddAnAssessment : CodeActivity
    {
        // Variables
        protected const string dxc_interviewer = "dxc_interviewer";
        protected const string dxc_assessment = "dxc_assessment";
        protected const string dxc_name = "dxc_name";
        protected const string dxc_criteriatype = "dxc_criteriatype"; // OptionSet
        protected const string dxc_interviewerforassessment = "dxc_interviewerforassessment"; // Lookup

        Helper.Helper help = new Helper.Helper();
        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                tracer.Trace("entity = " + entity.LogicalName + " id = " + entity.Id);
                Guid assessmentID = entity.Id;
                CreateAssessmentViaAllAssmntMgr(tracer, service, assessmentID);

            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
        protected void CreateAssessmentViaAllAssmntMgr(ITracingService tracer, IOrganizationService service, Guid assessmentID)
        {
            BL_AssessmentManager assessment_Manager = new BL_AssessmentManager();
            EntityCollection assessmentManager = assessment_Manager.GetAllAssessmentManagers(tracer, service);
            if (assessmentManager != null)
            {
                // Console
                tracer.Trace("Inside CreateAssessmentViaAllAssmntMgr 1");
                // Loop the rest of the records to add up to assessment.
                foreach (Entity e in assessmentManager.Entities)
                {
                    string dxc_name_value = e.GetAttributeValue<String>(dxc_name);
                    int dxc_criteriatype_Value = e.GetAttributeValue<OptionSetValue>(dxc_criteriatype).Value;
                    //EntityReference dxc_interviewerforassessment_value = e.GetAttributeValue<EntityReference>(dxc_interviewerforassessment);
                    // console
                    tracer.Trace("Inside CreateAssessmentViaAllAssmntMgr 1.A.");
                    tracer.Trace("Inside dxc_name_value =" + dxc_name_value);
                    tracer.Trace("Inside dxc_criteriatype_Value = " + dxc_criteriatype_Value);
                    tracer.Trace("Inside curID = " + assessmentID);
                    CreateAssessment(tracer, service, dxc_name_value, dxc_criteriatype_Value, assessmentID);
                }
            }
        }

        protected void CreateAssessment(ITracingService tracer, IOrganizationService service, string name, int criteriaType, Guid assessmentID)
        {
            try
            {
                // console
                tracer.Trace("Inside CreateAssessment > name = " + name);
                Entity newAssessment = new Entity(dxc_assessment);
                newAssessment[dxc_name] = name;
                newAssessment[dxc_criteriatype] = new OptionSetValue(criteriaType);
                newAssessment[dxc_interviewerforassessment] = new EntityReference(dxc_interviewer, assessmentID);

                service.Create(newAssessment);

                tracer.Trace(help.SuccessfulTraceMsg(name));
            }
            catch (Exception e)
            {
                tracer.Trace(help.UnsuccessfulTraceMsg(name));
                throw new InvalidPluginExecutionException(e.Message);
            }
        }



    }
}