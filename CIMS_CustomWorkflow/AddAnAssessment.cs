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
        protected const string systemuser = "systemuser";
        protected const string dxc_interviewername = "dxc_interviewername";
        protected const string dxc_businessareas = "dxc_businessareas";

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
                EntityReference systemusername = (EntityReference)entity.Attributes[dxc_interviewername];
                Guid assessmentID = entity.Id;

                CreateAssessmentViaAllAssmntMgr(tracer, service, assessmentID, systemusername.Id);
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
        protected void CreateAssessmentViaAllAssmntMgr(ITracingService tracer, IOrganizationService service, Guid assessmentID, Guid systemuserid)
        {
            BL_Systemuser bl_systemuser = new BL_Systemuser();
            BL_AssessmentManager assessment_Manager = new BL_AssessmentManager();
            // Get the user's Business Area of a user. 
            EntityCollection systemusers = bl_systemuser.GetAssesmentUser(tracer, service, systemuserid);
            EntityCollection assessmentManager = new EntityCollection();

            // Check if the user is existing. 
            if (systemusers.Entities.Count > 0)
            {
                // Get Business Area.
                EntityReference businessArea = new EntityReference();
                tracer.Trace("here 1");
                foreach (Entity en in systemusers.Entities)
                {
                    tracer.Trace("here 2");
                    // Check if Empty
                    if (en.GetAttributeValue<EntityReference>(dxc_businessareas) != null)
                    {
                        tracer.Trace("here 3");
                        businessArea = (EntityReference)en.Attributes[dxc_businessareas];
                        tracer.Trace("businessArea.Id = " + businessArea.Id);
                    }                
                    // If there's no Business Area for the user, then create an assessment without a business area criteria.
                    else
                    {
                        assessmentManager = assessment_Manager.GetAllAssessmentManagers(tracer, service);
                        CheckToCreateAssessment(tracer, service, assessmentManager, assessmentID);
                    }
                }
                if (businessArea != null)
                {
                    assessmentManager = assessment_Manager.GetAssessmentManagersViaBusArea(tracer, service, businessArea.Id);
                    CheckToCreateAssessment(tracer, service, assessmentManager, assessmentID);
                }
            } // If there's no user, then create an assessment without a business area criteria.
            else
            {
                assessmentManager = assessment_Manager.GetAllAssessmentManagers(tracer, service);
                CheckToCreateAssessment(tracer, service, assessmentManager, assessmentID);
            }

        }

        protected void CheckToCreateAssessment(ITracingService tracer, IOrganizationService service, EntityCollection assessmentManager, Guid assessmentID)
        {
            if (assessmentManager != null)
            {
                // Console
                tracer.Trace("Inside CreateAssessmentViaAllAssmntMgr 1 " + assessmentManager.Entities.Count);
                // Loop the rest of the records to add up to assessment.
                foreach (Entity e in assessmentManager.Entities)
                {
                    tracer.Trace("here! CreateAssessment");
                    string dxc_name_value = e.GetAttributeValue<String>(dxc_name);
                    tracer.Trace("here! CreateAssessment = " + dxc_name_value);
                    int dxc_criteriatype_Value = e.GetAttributeValue<OptionSetValue>(dxc_criteriatype).Value;
                    tracer.Trace("here! CreateAssessment = " + dxc_criteriatype_Value);
                    //EntityReference dxc_interviewerforassessment_value = e.GetAttributeValue<EntityReference>(dxc_interviewerforassessment);

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