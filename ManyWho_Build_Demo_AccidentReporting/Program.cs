using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyWho.Flow.SDK;
using ManyWho.Flow.SDK.Utils;
using ManyWho.Flow.SDK.Security;
using ManyWho.Flow.SDK.Describe;
using ManyWho.Flow.SDK.Run;
using ManyWho.Flow.SDK.Run.Elements.Type;
using ManyWho.Flow.SDK.Draw.Flow;
using ManyWho.Flow.SDK.Draw.Elements.UI;
using ManyWho.Flow.SDK.Draw.Elements.Map;
using ManyWho.Flow.SDK.Draw.Elements.Type;
using ManyWho.Flow.SDK.Draw.Elements.Group;
using ManyWho.Flow.SDK.Draw.Elements.Value;
using ManyWho.Flow.SDK.Draw.Elements.Config;

namespace ManyWho_Build_Demo_AccidentReporting
{
    public class Program
    {
        public const String MANYWHO_BASE_URL = "https://flowtest.manywho.com";

        public const String SERVICE_URL_SALESFORCE = "https://salesforce.manywho.com/plugins/api/salesforce/1";
        public const String SERVICE_VALUE_AUTHENTICATION_URL_VALUE = "https://login.salesforce.com";
        public const String SERVICE_VALUE_USERNAME_VALUE = "username";
        public const String SERVICE_VALUE_PASSWORD_VALUE = "password";
        public const String SERVICE_VALUE_SECURITY_TOKEN_VALUE = "securitytoken";
        public const String SERVICE_VALUE_CHATTER_BASE_URL_VALUE = "https://na15.salesforce.com";
        public const String SERVICE_VALUE_ADMIN_EMAIL_VALUE = "admin@manywho.com";

        // Constants from the salesforce service
        public const String SERVICE_VALUE_SECURITY_TOKEN = "SecurityToken";
        public const String SERVICE_VALUE_USERNAME = "Username";
        public const String SERVICE_VALUE_PASSWORD = "Password";
        public const String SERVICE_VALUE_AUTHENTICATION_URL = "AuthenticationUrl";
        public const String SERVICE_VALUE_CHATTER_BASE_URL = "ChatterBaseUrl";
        public const String SERVICE_VALUE_ADMIN_EMAIL = "AdminEmail";

        public static void Main(string[] args)
        {
            IAuthenticatedWho authenticatedWho = null;
            FlowRequestAPI flowRequest = null;
            FlowResponseAPI flowResponse = null;
            ServiceElementResponseAPI serviceElementResponse = null;
            MessageActionAPI messageAction = null;
            TypeElementRequestAPI typeElementRequest = null;
            PageElementRequestAPI pageElementRequest = null;
            PageElementResponseAPI contactLegalPageElementResponse = null;
            PageElementResponseAPI legalSubmitIncidentPageElementResponse = null;
            PageElementResponseAPI requestReportPageElementResponse = null;
            PageElementResponseAPI legalReportApprovalPageElementResponse = null;
            PageElementResponseAPI reportReviewCheck2WeekPageElementResponse = null;
            PageElementResponseAPI reportReviewCheck1YearPageElementResponse = null;
            PageElementResponseAPI reportReview2WeekPageElementResponse = null;
            PageElementResponseAPI reportReview1YearPageElementResponse = null;
            PageElementResponseAPI reportReviewDonePageElementResponse = null;
            MapElementRequestAPI mapElementRequest = null;
            MapElementResponseAPI contactLegalMapElementResponse = null;
            MapElementResponseAPI legalSubmitIncidentMapElementResponse = null;
            MapElementResponseAPI requestReportMapElementResponse = null;
            MapElementResponseAPI legalReportApprovalMapElementResponse = null;
            MapElementResponseAPI reportReviewCheck2WeekMapElementResponse = null;
            MapElementResponseAPI reportReviewCheck1YearMapElementResponse = null;
            MapElementResponseAPI reportReview2WeekMapElementResponse = null;
            MapElementResponseAPI reportReview1YearMapElementResponse = null;
            MapElementResponseAPI startMapElementResponse = null;
            MapElementResponseAPI reportReviewDoneMapElementResponse = null;
            MapElementResponseAPI createCalendarItemsMapElementResponse = null;
            GroupElementRequestAPI groupElementRequest = null;
            GroupElementResponseAPI storeManagerSwimlaneGroupElementResponse = null;
            GroupElementResponseAPI legalDepartmentSwimlaneGroupElementResponse = null;
            ValueElementRequestAPI valueElementRequest = null;
            ValueElementResponseAPI legalSubmitIncidentValueElementResponse = null;
            ValueElementResponseAPI requestReportValueElementResponse = null;
            ValueElementResponseAPI legalReportApprovalValueElementResponse = null;
            ValueElementResponseAPI taskSubjectValueElementResponse = null;
            ValueElementResponseAPI taskDescriptionValueElementResponse = null;
            ValueElementResponseAPI taskWhenValueElementResponse = null;
            ValueElementResponseAPI taskPriorityValueElementResponse = null;
            ValueElementResponseAPI taskStatusValueElementResponse = null;
            ValueElementResponseAPI eventSubjectValueElementResponse = null;
            ValueElementResponseAPI eventDescriptionValueElementResponse = null;
            ValueElementResponseAPI eventWhenValueElementResponse = null;
            ValueElementResponseAPI eventDurationValueElementResponse = null;
            ValueElementResponseAPI subjectValueElementResponse = null;
            ValueElementResponseAPI descriptionValueElementResponse = null;
            ValueElementResponseAPI optionsTriageValueElementResponse = null;
            ValueElementResponseAPI triageValueElementResponse = null;
            ValueElementResponseAPI incidentDescriptionValueElementResponse = null;
            ValueElementResponseAPI storeManagerSignatureValueElementResponse = null;
            ValueElementResponseAPI customerSignatureValueElementResponse = null;
            ValueElementResponseAPI confirmDocumentsSignedAttachedValueElementResponse = null;
            ValueElementResponseAPI weekReviewNotesValueElementResponse = null;
            ValueElementResponseAPI annualReviewNotesValueElementResponse = null;
            TypeElementResponseAPI dropDownTypeElementResponse = null;
            ObjectAPI objectAPI = null;

            // User needs to be prompted to login so we can get the authenticated who
            authenticatedWho = Login();

            if (authenticatedWho == null)
            {
                // We can't do anything if the user can't login
                Console.WriteLine("Unable to login to the system");
            }
            else
            {
                // We've authenticated so we can now do the correct thing for the command
                if (args != null &&
                    args.Length > 0 &&
                    args[0] != null &&
                    args[0].Equals("create", StringComparison.InvariantCultureIgnoreCase) == true)
                {
                    // Create the salesforce service and dependent value elements
                    serviceElementResponse = CreateSalesforceService(authenticatedWho);

                    // Create the new flow
                    flowRequest = new FlowRequestAPI();
                    flowRequest.developerName = "Accident Reporting";
                    flowRequest.developerSummary = "This is a demo Flow showing accident reporting";
                    flowRequest.authorization = new GroupAuthorizationAPI();
                    flowRequest.authorization.globalAuthenticationType = ManyWhoConstants.GROUP_AUTHORIZATION_GLOBAL_AUTHENTICATION_TYPE_ALL_USERS;
                    flowRequest.authorization.serviceElementId = serviceElementResponse.id;
                    flowRequest.authorization.streamBehaviourType = ManyWhoConstants.GROUP_AUTHORIZATION_STREAM_BEHAVIOUR_CREATE_NEW;

                    // Save the flow and start the editing session
                    flowResponse = DrawSingleton.GetInstance().SaveFlow(authenticatedWho, MANYWHO_BASE_URL, flowRequest, "Program.Main", "admin@manywho.com");

                    // We also add the service to the flow as this is not done automatically
                    DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowResponse.id.id, "service", serviceElementResponse.id, "Program.Main", "admin@manywho.com");

                    // Write the values back out to the console for the caller
                    Console.WriteLine("Outputs");
                    Console.WriteLine("Flow Id: " + flowResponse.id.id);
                    Console.WriteLine("Editing Token: " + flowResponse.editingToken);
                    Console.WriteLine("Start Map Element Id: " + flowResponse.startMapElementId);
                    Console.WriteLine("Service Element Id: " + serviceElementResponse.id);
                }
                if (args != null &&
                    args.Length > 0 &&
                    args[0] != null &&
                    args[0].Equals("build", StringComparison.InvariantCultureIgnoreCase) == true &&
                    args.Length == 5)
                {
                    Boolean performBuild = true;
                    String flowId = null;
                    String editingToken = null;
                    String startMapElementId = null;
                    String serviceElementId = null;

                    flowId = args[1];
                    editingToken = args[2];
                    startMapElementId = args[3];
                    serviceElementId = args[4];

                    if (flowId == null ||
                        flowId.Trim().Length == 0)
                    {
                        Console.WriteLine("Missing parameter: Flow Id");
                        performBuild = false;
                    }

                    if (editingToken == null ||
                        editingToken.Trim().Length == 0)
                    {
                        Console.WriteLine("Missing parameter: Editing Token");
                        performBuild = false;
                    }

                    if (startMapElementId == null ||
                        startMapElementId.Trim().Length == 0)
                    {
                        Console.WriteLine("Missing parameter: Start Map Element Id");
                        performBuild = false;
                    }

                    if (serviceElementId == null ||
                        serviceElementId.Trim().Length == 0)
                    {
                        Console.WriteLine("Missing parameter: Service Element Id");
                        performBuild = false;
                    }

                    // Check to make sure the input parameters passed validation
                    if (performBuild == true)
                    {
                        Console.WriteLine("Creating Value Elements And Type");

                        typeElementRequest = Utils.CreateDropDownType("Drop Down");
                        dropDownTypeElementResponse = DrawSingleton.GetInstance().SaveTypeElement(authenticatedWho, MANYWHO_BASE_URL, typeElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "type", dropDownTypeElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Subject", ManyWhoConstants.CONTENT_TYPE_STRING);
                        subjectValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", subjectValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Description", ManyWhoConstants.CONTENT_TYPE_STRING);
                        descriptionValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", descriptionValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Triage", ManyWhoConstants.CONTENT_TYPE_STRING);
                        triageValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", triageValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Triage Options", ManyWhoConstants.CONTENT_TYPE_LIST);

                        objectAPI = new ObjectAPI();
                        objectAPI.developerName = dropDownTypeElementResponse.developerName;
                        objectAPI.properties = new List<PropertyAPI>();
                        objectAPI.properties.Add(new PropertyAPI() { contentValue = "Incident", developerName = "Label", typeElementPropertyId = Utils.GetTypeElementEntryIdForDeveloperName(dropDownTypeElementResponse, "Label") });
                        objectAPI.properties.Add(new PropertyAPI() { contentValue = "Incident", developerName = "Value", typeElementPropertyId = Utils.GetTypeElementEntryIdForDeveloperName(dropDownTypeElementResponse, "Value") });

                        // Now serialize the object data as a string
                        valueElementRequest.defaultContentValue = ValueUtils.SerializeListData(new List<ObjectAPI>() { objectAPI }, dropDownTypeElementResponse);
                        valueElementRequest.typeElementId = dropDownTypeElementResponse.id;
                        optionsTriageValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", optionsTriageValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Incident Description", ManyWhoConstants.CONTENT_TYPE_STRING);
                        incidentDescriptionValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", incidentDescriptionValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Store Manager Signature", ManyWhoConstants.CONTENT_TYPE_STRING);
                        storeManagerSignatureValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", storeManagerSignatureValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Customer Signature", ManyWhoConstants.CONTENT_TYPE_STRING);
                        customerSignatureValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", customerSignatureValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Confirm Documents Signed", ManyWhoConstants.CONTENT_TYPE_BOOLEAN);
                        confirmDocumentsSignedAttachedValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", confirmDocumentsSignedAttachedValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Weekly Review Notes", ManyWhoConstants.CONTENT_TYPE_STRING);
                        weekReviewNotesValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", weekReviewNotesValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Annual Review Notes", ManyWhoConstants.CONTENT_TYPE_STRING);
                        annualReviewNotesValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", annualReviewNotesValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Creating Value Elements And Type Done");
                        Console.WriteLine("Creating Swimlane: Store Manager");

                        // Create the swimlane for store managers
                        groupElementRequest = Utils.CreateSwimlaneElement("Store Manager", 100, 150, 200, 1600, serviceElementId, "0F9i00000004P2E");
                        storeManagerSwimlaneGroupElementResponse = DrawSingleton.GetInstance().SaveGroupElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, groupElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Swimlane Completed");
                        Console.WriteLine("Creating Swimlane: Legal Department");

                        // Create the swimlane for legal department
                        groupElementRequest = Utils.CreateSwimlaneElement("Legal Department", 100, 350, 200, 1600, serviceElementId, "0F9i00000004P2J");
                        legalDepartmentSwimlaneGroupElementResponse = DrawSingleton.GetInstance().SaveGroupElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, groupElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Swimlane Completed");
                        Console.WriteLine("Creating Page Layout: Legal Request");

                        // Create the page layout for the initial legal contact
                        pageElementRequest = CreateLegalRequestPage("Legal Request", subjectValueElementResponse, descriptionValueElementResponse);
                        contactLegalPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", contactLegalPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Input: Legal Request");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("Legal Request", contactLegalPageElementResponse.id, storeManagerSwimlaneGroupElementResponse.id, null, null, 75, 50);
                        contactLegalMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Page Layout: Create Incident");

                        // Create the page layout for legal to create an incident
                        pageElementRequest = CreateLegalTriagePage("Triage Request", subjectValueElementResponse, descriptionValueElementResponse, dropDownTypeElementResponse, optionsTriageValueElementResponse, triageValueElementResponse);
                        legalSubmitIncidentPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", legalSubmitIncidentPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Notification Message: Notify Create Incident");

                        // Create the notification message
                        valueElementRequest = Utils.CreateValueElement("Notify Create Incident", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "You have a request for legal support waiting.";
                        legalSubmitIncidentValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", legalSubmitIncidentValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Notification Message Completed");
                        Console.WriteLine("Creating Input: Create Incident");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("Create Incident", legalSubmitIncidentPageElementResponse.id, legalDepartmentSwimlaneGroupElementResponse.id, serviceElementId, legalSubmitIncidentValueElementResponse, 75, 50);
                        legalSubmitIncidentMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Page Layout: Incident Report");

                        // Create the page layout for legal requesting the incident report
                        pageElementRequest = CreateIncidentAndApprovalDetailsPage("Incident Report", subjectValueElementResponse, descriptionValueElementResponse, incidentDescriptionValueElementResponse, storeManagerSignatureValueElementResponse, customerSignatureValueElementResponse, confirmDocumentsSignedAttachedValueElementResponse, true);
                        requestReportPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", requestReportPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Notification Message: Notify Incident Report");

                        // Create the notification message
                        valueElementRequest = Utils.CreateValueElement("Notify Incident Report", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "A report needs to be completed based on your request to the legal department.";
                        requestReportValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", requestReportValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Notification Message Completed");
                        Console.WriteLine("Creating Input: Incident Report");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("Incident Report", requestReportPageElementResponse.id, storeManagerSwimlaneGroupElementResponse.id, serviceElementId, requestReportValueElementResponse, 275, 50);
                        requestReportMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Page Layout: Approve Incident Report");

                        // Create the page layout for legal to approve the incident report
                        pageElementRequest = CreateIncidentAndApprovalDetailsPage("Approve Incident Report", subjectValueElementResponse, descriptionValueElementResponse, incidentDescriptionValueElementResponse, storeManagerSignatureValueElementResponse, customerSignatureValueElementResponse, confirmDocumentsSignedAttachedValueElementResponse, false);
                        legalReportApprovalPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", legalReportApprovalPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Notification Message: Notify Approve Incident Report");

                        // Create the notification message
                        valueElementRequest = Utils.CreateValueElement("Notify Approve Incident Report", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "A report needs your approval.";
                        legalReportApprovalValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", legalReportApprovalValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Notification Message Completed");
                        Console.WriteLine("Creating Input: Approve Incident Report");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("Approve Incident Report", legalReportApprovalPageElementResponse.id, legalDepartmentSwimlaneGroupElementResponse.id, serviceElementId, legalReportApprovalValueElementResponse, 275, 50);
                        legalReportApprovalMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Value Elements: Create Calendar Items");

                        // Create the values for the events
                        valueElementRequest = Utils.CreateValueElement("Review Task Subject", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "Week 2 Incident Review";
                        taskSubjectValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", taskSubjectValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Task Description", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "The incident needs to be reviewed. Please follow the link to the flow.";
                        taskDescriptionValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", taskDescriptionValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Task When", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "14 days";
                        taskWhenValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", taskWhenValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Task Priority", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "Normal";
                        taskPriorityValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", taskPriorityValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Task Status", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "Not Started";
                        taskStatusValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", taskStatusValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Event Subject", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "1 Year Incident Review";
                        eventSubjectValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", eventSubjectValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Event Description", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "The incident needs to be reviewed. Please follow the link to the flow.";
                        eventDescriptionValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", eventDescriptionValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Event When", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "365 days";
                        eventWhenValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", eventWhenValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        valueElementRequest = Utils.CreateValueElement("Review Event Duration", ManyWhoConstants.CONTENT_TYPE_STRING);
                        valueElementRequest.defaultContentValue = "60";
                        valueElementRequest.contentType = ManyWhoConstants.CONTENT_TYPE_NUMBER;
                        eventDurationValueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "value", eventDurationValueElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Creating Value Elements Done");
                        Console.WriteLine("Creating Message: Create Calendar Items");

                        // Create the message
                        mapElementRequest = Utils.CreateMessageElement("Create Calendar Items", storeManagerSwimlaneGroupElementResponse.id, 475, 50);
                        mapElementRequest.messageActions = new List<MessageActionAPI>();

                        // Create the message action for creating the task
                        messageAction = new MessageActionAPI();
                        messageAction.serviceElementId = serviceElementId;
                        messageAction.uriPart = "createtask";
                        messageAction.inputs = new List<MessageInputAPI>();
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "Subject", valueElementToReferenceId = new ValueElementIdAPI() { id = taskSubjectValueElementResponse.id } });
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "Description", valueElementToReferenceId = new ValueElementIdAPI() { id = taskDescriptionValueElementResponse.id } });
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "When", valueElementToReferenceId = new ValueElementIdAPI() { id = taskWhenValueElementResponse.id } });
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "Status", valueElementToReferenceId = new ValueElementIdAPI() { id = taskStatusValueElementResponse.id } });
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "Priority", valueElementToReferenceId = new ValueElementIdAPI() { id = taskPriorityValueElementResponse.id } });

                        mapElementRequest.messageActions.Add(messageAction);

                        // Create the message action for creating the event
                        messageAction = new MessageActionAPI();
                        messageAction.serviceElementId = serviceElementId;
                        messageAction.uriPart = "createevent";
                        messageAction.inputs = new List<MessageInputAPI>();
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "Subject", valueElementToReferenceId = new ValueElementIdAPI() { id = eventSubjectValueElementResponse.id } });
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "Description", valueElementToReferenceId = new ValueElementIdAPI() { id = eventDescriptionValueElementResponse.id } });
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "When", valueElementToReferenceId = new ValueElementIdAPI() { id = eventWhenValueElementResponse.id } });
                        messageAction.inputs.Add(new MessageInputAPI() { developerName = "Duration", valueElementToReferenceId = new ValueElementIdAPI() { id = eventDurationValueElementResponse.id } });

                        mapElementRequest.messageActions.Add(messageAction);

                        // Save the message to the service
                        createCalendarItemsMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Message Completed");
                        Console.WriteLine("Creating Page Layout: Confirm Report 2 Week");

                        // Create the page layout for starting the 2 week review
                        pageElementRequest = Utils.CreateGenericPageElement("Confirm Report 2 Week", "You are required to complete a review 2 weeks after and then 1 year after the incident. You have been sent calendar information for both of these and may exit this flow now.");
                        reportReviewCheck2WeekPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", reportReviewCheck2WeekPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Input: Confirm Report 2 Week");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("Confirm Report 2 Week", reportReviewCheck2WeekPageElementResponse.id, storeManagerSwimlaneGroupElementResponse.id, null, null, 675, 50);
                        reportReviewCheck2WeekMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Page Layout: 2 Week Report Review");

                        // Create the page layout for the 2 week review
                        pageElementRequest = CreateIncident2WeekReviewDetailsPage("2 Week Report Review", subjectValueElementResponse, descriptionValueElementResponse, incidentDescriptionValueElementResponse, storeManagerSignatureValueElementResponse, customerSignatureValueElementResponse, confirmDocumentsSignedAttachedValueElementResponse, weekReviewNotesValueElementResponse, true);
                        reportReview2WeekPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", reportReview2WeekPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Input: 2 Week Report Review");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("2 Week Report Review", reportReview2WeekPageElementResponse.id, storeManagerSwimlaneGroupElementResponse.id, null, null, 875, 50);
                        reportReview2WeekMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Page Layout: Confirm Report 1 Year");

                        // Create the page layout for starting the 1 year review
                        pageElementRequest = Utils.CreateGenericPageElement("Confirm Report 1 Year", "You are required to complete a review 1 year after the incident. You have been sent calendar information for this and may exit this flow now.");
                        reportReviewCheck1YearPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", reportReviewCheck1YearPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Input: Confirm Report 1 Year");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("Confirm Report 1 Year", reportReviewCheck1YearPageElementResponse.id, storeManagerSwimlaneGroupElementResponse.id, null, null, 1075, 50);
                        reportReviewCheck1YearMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Page Layout: 1 Year Report Review");

                        // Create the page layout for the 1 year review
                        pageElementRequest = CreateIncidentAnnualReviewDetailsPage("1 Year Report Review", subjectValueElementResponse, descriptionValueElementResponse, incidentDescriptionValueElementResponse, storeManagerSignatureValueElementResponse, customerSignatureValueElementResponse, confirmDocumentsSignedAttachedValueElementResponse, weekReviewNotesValueElementResponse, annualReviewNotesValueElementResponse);
                        reportReview1YearPageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", reportReview1YearPageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Input: 1 Year Report Review");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("1 Year Report Review", reportReview1YearPageElementResponse.id, storeManagerSwimlaneGroupElementResponse.id, null, null, 1275, 50);
                        reportReview1YearMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Page Layout: Done");

                        // Create the page layout for the finished process
                        pageElementRequest = Utils.CreateGenericPageElement("Done", "Done");
                        reportReviewDonePageElementResponse = DrawSingleton.GetInstance().SavePageElement(authenticatedWho, MANYWHO_BASE_URL, pageElementRequest, "Program.Main", "admin@manywho.com");
                        DrawSingleton.GetInstance().AddElementToFlow(authenticatedWho, MANYWHO_BASE_URL, flowId, "pagelayout", reportReviewDonePageElementResponse.id, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Page Layout Completed");
                        Console.WriteLine("Creating Input: 1 Year Report Review");

                        // Create the input
                        mapElementRequest = Utils.CreateInputElement("Done", reportReviewDonePageElementResponse.id, storeManagerSwimlaneGroupElementResponse.id, null, null, 1475, 50);
                        reportReviewDoneMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, mapElementRequest, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Input Completed");
                        Console.WriteLine("Creating Outcomes");

                        // We need to load the start map element to create the entry point
                        startMapElementResponse = DrawSingleton.GetInstance().LoadMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, startMapElementId, "Program.Main", "admin@manywho.com");
                        Utils.CreateOutcome("Go", "Go", startMapElementResponse, contactLegalMapElementResponse);
                        startMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, startMapElementResponse, "Program.Main", "admin@manywho.com");

                        // Now we wire together the outcomes and save the changes to the map elements
                        Utils.CreateOutcome("Submit", "Submit", contactLegalMapElementResponse, legalSubmitIncidentMapElementResponse);
                        contactLegalMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, contactLegalMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Request", "Request", legalSubmitIncidentMapElementResponse, requestReportMapElementResponse);
                        legalSubmitIncidentMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, legalSubmitIncidentMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Submit", "Submit", requestReportMapElementResponse, legalReportApprovalMapElementResponse);
                        requestReportMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, requestReportMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Approve", "Approve", legalReportApprovalMapElementResponse, createCalendarItemsMapElementResponse);
                        Utils.CreateOutcome("Reject", "Reject", legalReportApprovalMapElementResponse, requestReportMapElementResponse);
                        legalReportApprovalMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, legalReportApprovalMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Created", "Created", createCalendarItemsMapElementResponse, reportReviewCheck2WeekMapElementResponse);
                        createCalendarItemsMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, createCalendarItemsMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Do Now", "Do Review Now", reportReviewCheck2WeekMapElementResponse, reportReview2WeekMapElementResponse);
                        reportReviewCheck2WeekMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, reportReviewCheck2WeekMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Submit", "Submit", reportReview2WeekMapElementResponse, reportReviewCheck1YearMapElementResponse);
                        reportReview2WeekMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, reportReview2WeekMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Do Now", "Do Review Now", reportReviewCheck1YearMapElementResponse, reportReview1YearMapElementResponse);
                        reportReviewCheck1YearMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, reportReviewCheck1YearMapElementResponse, "Program.Main", "admin@manywho.com");

                        Utils.CreateOutcome("Submit", "Submit", reportReview1YearMapElementResponse, reportReviewDoneMapElementResponse);
                        reportReview1YearMapElementResponse = DrawSingleton.GetInstance().SaveMapElement(authenticatedWho, MANYWHO_BASE_URL, editingToken, flowId, reportReview1YearMapElementResponse, "Program.Main", "admin@manywho.com");

                        Console.WriteLine("Flow Done.");
                    }
                }
                else
                {
                    Console.WriteLine("ManyWho Flow Builder");
                    Console.WriteLine("To create the flow, use the following command: create");
                    Console.WriteLine("To create the objects in the flow, use the following command: build <Flow Id> <Editing Token> <Service Element Id>");
                }
            }
        }

        private static IAuthenticatedWho Login()
        {
            AuthenticationCredentialsAPI authenticationCredentials = null;
            String username = null;
            String password = null;
            String tenantId = null;

            Console.Write("Username:");
            username = Console.ReadLine();

            Console.Write("Password:");
            password = Console.ReadLine();

            Console.Write("Tenant Id:");
            tenantId = Console.ReadLine();

            // Create a new authentication credentials object based on the provided information
            authenticationCredentials = new AuthenticationCredentialsAPI();
            authenticationCredentials.loginUrl = MANYWHO_BASE_URL + DrawSingleton.MANYWHO_DRAW_URI_PART_ADMIN_PLUGIN_AUTHENTICATION;
            authenticationCredentials.username = username;
            authenticationCredentials.password = password;

            // Execute the login and return the result
            return DrawSingleton.GetInstance().Login(tenantId, MANYWHO_BASE_URL, authenticationCredentials, "Program.Login", "admin@manywho.com");
        }

        /// <summary>
        /// Method to create the salesforce service plugin so we can work with the salesforce.com.
        /// </summary>
        private static ServiceElementResponseAPI CreateSalesforceService(IAuthenticatedWho authenticatedWho)
        {
            ServiceElementRequestAPI serviceElementRequest = null;
            ServiceElementResponseAPI serviceElementResponse = null;
            ValueElementResponseAPI valueElementResponse = null;
            ValueElementRequestAPI valueElementRequest = null;
            DescribeServiceResponseAPI describeServiceResponse = null;
            DescribeServiceRequestAPI describeServiceRequest = null;
            ServiceValueRequestAPI serviceValueRequest = null;

            describeServiceRequest = new DescribeServiceRequestAPI();
            describeServiceRequest.culture = null;
            describeServiceRequest.uri = SERVICE_URL_SALESFORCE;

            // Add the configuration values for the salesforce service
            describeServiceRequest.configurationValues = new List<EngineValueAPI>();
            describeServiceRequest.configurationValues.Add(new EngineValueAPI() { developerName = SERVICE_VALUE_AUTHENTICATION_URL, contentValue = SERVICE_VALUE_AUTHENTICATION_URL_VALUE, contentType = ManyWhoConstants.CONTENT_TYPE_STRING });
            describeServiceRequest.configurationValues.Add(new EngineValueAPI() { developerName = SERVICE_VALUE_CHATTER_BASE_URL, contentValue = SERVICE_VALUE_CHATTER_BASE_URL_VALUE, contentType = ManyWhoConstants.CONTENT_TYPE_STRING });
            describeServiceRequest.configurationValues.Add(new EngineValueAPI() { developerName = SERVICE_VALUE_USERNAME, contentValue = SERVICE_VALUE_USERNAME_VALUE, contentType = ManyWhoConstants.CONTENT_TYPE_STRING });
            describeServiceRequest.configurationValues.Add(new EngineValueAPI() { developerName = SERVICE_VALUE_PASSWORD, contentValue = SERVICE_VALUE_PASSWORD_VALUE, contentType = ManyWhoConstants.CONTENT_TYPE_PASSWORD });
            describeServiceRequest.configurationValues.Add(new EngineValueAPI() { developerName = SERVICE_VALUE_SECURITY_TOKEN, contentValue = SERVICE_VALUE_SECURITY_TOKEN_VALUE, contentType = ManyWhoConstants.CONTENT_TYPE_STRING });
            describeServiceRequest.configurationValues.Add(new EngineValueAPI() { developerName = SERVICE_VALUE_ADMIN_EMAIL, contentValue = SERVICE_VALUE_ADMIN_EMAIL_VALUE, contentType = ManyWhoConstants.CONTENT_TYPE_STRING });

            // Call the describe call against the salesforce service
            describeServiceResponse = DrawSingleton.GetInstance().Describe(authenticatedWho, describeServiceRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            serviceElementRequest = new ServiceElementRequestAPI();
            serviceElementRequest.developerName = "Salesforce Service";
            serviceElementRequest.developerSummary = "Service to connect to Salesforce.com";
            serviceElementRequest.elementType = ManyWhoConstants.SERVICE_ELEMENT_TYPE_IMPLEMENTATION_SERVICE;
            serviceElementRequest.format = "json";
            serviceElementRequest.id = null;
            serviceElementRequest.providesDatabase = describeServiceResponse.providesDatabase;
            serviceElementRequest.providesLogic = describeServiceResponse.providesLogic;
            serviceElementRequest.providesViews = describeServiceResponse.providesViews;
            serviceElementRequest.providesIdentity = describeServiceResponse.providesIdentity;
            serviceElementRequest.providesSocial = describeServiceResponse.providesSocial;
            serviceElementRequest.uri = describeServiceRequest.uri;

            serviceElementRequest.configurationValues = new List<ServiceValueRequestAPI>();

            // Create the value element for the authentication url
            valueElementRequest = Utils.CreateValueElement(SERVICE_VALUE_AUTHENTICATION_URL, ManyWhoConstants.CONTENT_TYPE_STRING);
            valueElementRequest.elementType = ManyWhoConstants.SHARED_ELEMENT_TYPE_IMPLEMENTATION_VARIABLE;
            valueElementRequest.developerSummary = "The Url to use when logging into salesforce.com";
            valueElementRequest.access = ManyWhoConstants.ACCESS_PRIVATE;
            valueElementRequest.isFixed = true;
            valueElementRequest.defaultContentValue = SERVICE_VALUE_AUTHENTICATION_URL_VALUE;

            // Save the value element back to the service
            valueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            // Map the value element to the service configuration
            serviceValueRequest = new ServiceValueRequestAPI();
            serviceValueRequest.developerName = SERVICE_VALUE_AUTHENTICATION_URL;
            serviceValueRequest.valueElementToReferenceId = new ValueElementIdAPI(valueElementResponse.id, null, null);
            serviceValueRequest.contentType = ManyWhoConstants.CONTENT_TYPE_STRING;

            // Add the service value request to the service configuration
            serviceElementRequest.configurationValues.Add(serviceValueRequest);

            // Create the value element for the chatter base url
            valueElementRequest = Utils.CreateValueElement(SERVICE_VALUE_CHATTER_BASE_URL, ManyWhoConstants.CONTENT_TYPE_STRING);
            valueElementRequest.elementType = ManyWhoConstants.SHARED_ELEMENT_TYPE_IMPLEMENTATION_VARIABLE;
            valueElementRequest.developerSummary = "The Url to use as the base location for chatter.";
            valueElementRequest.access = ManyWhoConstants.ACCESS_PRIVATE;
            valueElementRequest.isFixed = true;
            valueElementRequest.defaultContentValue = SERVICE_VALUE_CHATTER_BASE_URL_VALUE;

            // Save the value element back to the service
            valueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            // Map the value element to the service configuration
            serviceValueRequest = new ServiceValueRequestAPI();
            serviceValueRequest.developerName = SERVICE_VALUE_CHATTER_BASE_URL;
            serviceValueRequest.valueElementToReferenceId = new ValueElementIdAPI(valueElementResponse.id, null, null);
            serviceValueRequest.contentType = ManyWhoConstants.CONTENT_TYPE_STRING;

            // Add the service value request to the service configuration
            serviceElementRequest.configurationValues.Add(serviceValueRequest);

            // Create the value element for the salesforce username
            valueElementRequest = Utils.CreateValueElement(SERVICE_VALUE_USERNAME, ManyWhoConstants.CONTENT_TYPE_STRING);
            valueElementRequest.elementType = ManyWhoConstants.SHARED_ELEMENT_TYPE_IMPLEMENTATION_VARIABLE;
            valueElementRequest.developerSummary = "The username to login to salesforce.com.";
            valueElementRequest.access = ManyWhoConstants.ACCESS_PRIVATE;
            valueElementRequest.isFixed = true;
            valueElementRequest.defaultContentValue = SERVICE_VALUE_USERNAME_VALUE;

            // Save the value element back to the service
            valueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            // Map the value element to the service configuration
            serviceValueRequest = new ServiceValueRequestAPI();
            serviceValueRequest.developerName = SERVICE_VALUE_USERNAME;
            serviceValueRequest.valueElementToReferenceId = new ValueElementIdAPI(valueElementResponse.id, null, null);
            serviceValueRequest.contentType = ManyWhoConstants.CONTENT_TYPE_STRING;

            // Add the service value request to the service configuration
            serviceElementRequest.configurationValues.Add(serviceValueRequest);

            // Create the value element for the salesforce password
            valueElementRequest = Utils.CreateValueElement(SERVICE_VALUE_PASSWORD, ManyWhoConstants.CONTENT_TYPE_STRING);
            valueElementRequest.elementType = ManyWhoConstants.SHARED_ELEMENT_TYPE_IMPLEMENTATION_VARIABLE;
            valueElementRequest.developerSummary = "The password to login to salesforce.com.";
            valueElementRequest.access = ManyWhoConstants.ACCESS_PRIVATE;
            valueElementRequest.isFixed = true;
            valueElementRequest.defaultContentValue = SERVICE_VALUE_PASSWORD_VALUE;

            // Save the value element back to the service
            valueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            // Map the value element to the service configuration
            serviceValueRequest = new ServiceValueRequestAPI();
            serviceValueRequest.developerName = SERVICE_VALUE_PASSWORD;
            serviceValueRequest.valueElementToReferenceId = new ValueElementIdAPI(valueElementResponse.id, null, null);
            serviceValueRequest.contentType = ManyWhoConstants.CONTENT_TYPE_STRING;

            // Add the service value request to the service configuration
            serviceElementRequest.configurationValues.Add(serviceValueRequest);

            // Create the value element for the salesforce security token
            valueElementRequest = Utils.CreateValueElement(SERVICE_VALUE_SECURITY_TOKEN, ManyWhoConstants.CONTENT_TYPE_STRING);
            valueElementRequest.elementType = ManyWhoConstants.SHARED_ELEMENT_TYPE_IMPLEMENTATION_VARIABLE;
            valueElementRequest.developerSummary = "The security token to login to salesforce.com.";
            valueElementRequest.access = ManyWhoConstants.ACCESS_PRIVATE;
            valueElementRequest.isFixed = true;
            valueElementRequest.defaultContentValue = SERVICE_VALUE_SECURITY_TOKEN_VALUE;

            // Save the value element back to the service
            valueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            // Map the value element to the service configuration
            serviceValueRequest = new ServiceValueRequestAPI();
            serviceValueRequest.developerName = SERVICE_VALUE_SECURITY_TOKEN;
            serviceValueRequest.valueElementToReferenceId = new ValueElementIdAPI(valueElementResponse.id, null, null);
            serviceValueRequest.contentType = ManyWhoConstants.CONTENT_TYPE_STRING;

            // Add the service value request to the service configuration
            serviceElementRequest.configurationValues.Add(serviceValueRequest);

            // Create the value element for the admin email
            valueElementRequest = Utils.CreateValueElement(SERVICE_VALUE_ADMIN_EMAIL, ManyWhoConstants.CONTENT_TYPE_STRING);
            valueElementRequest.elementType = ManyWhoConstants.SHARED_ELEMENT_TYPE_IMPLEMENTATION_VARIABLE;
            valueElementRequest.developerSummary = "The admin email for errors in salesforce.com.";
            valueElementRequest.access = ManyWhoConstants.ACCESS_PRIVATE;
            valueElementRequest.isFixed = true;
            valueElementRequest.defaultContentValue = SERVICE_VALUE_ADMIN_EMAIL_VALUE;

            // Save the value element back to the service
            valueElementResponse = DrawSingleton.GetInstance().SaveValueElement(authenticatedWho, MANYWHO_BASE_URL, valueElementRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            // Map the value element to the service configuration
            serviceValueRequest = new ServiceValueRequestAPI();
            serviceValueRequest.developerName = SERVICE_VALUE_ADMIN_EMAIL;
            serviceValueRequest.valueElementToReferenceId = new ValueElementIdAPI(valueElementResponse.id, null, null);
            serviceValueRequest.contentType = ManyWhoConstants.CONTENT_TYPE_STRING;

            // Add the service value request to the service configuration
            serviceElementRequest.configurationValues.Add(serviceValueRequest);

            // Now we can go through any actions and add that to the request also
            if (describeServiceResponse.actions != null &&
                describeServiceResponse.actions.Count > 0)
            {
                serviceElementRequest.actions = new List<ServiceActionRequestAPI>();

                foreach (DescribeServiceActionResponseAPI describeServiceActionResponse in describeServiceResponse.actions)
                {
                    ServiceActionRequestAPI serviceActionRequest = new ServiceActionRequestAPI();

                    serviceActionRequest.uriPart = describeServiceActionResponse.uriPart;
                    serviceActionRequest.developerName = describeServiceActionResponse.developerName;
                    serviceActionRequest.developerSummary = describeServiceActionResponse.developerSummary;
                    serviceActionRequest.isViewMessageAction = describeServiceActionResponse.isViewMessageAction;

                    if (describeServiceActionResponse.serviceActionOutcomes != null &&
                        describeServiceActionResponse.serviceActionOutcomes.Count > 0)
                    {
                        serviceActionRequest.serviceActionOutcomes = new List<ServiceActionOutcomeAPI>();

                        foreach (DescribeUIServiceActionOutcomeAPI describeUIServiceActionOutcome in describeServiceActionResponse.serviceActionOutcomes)
                        {
                            ServiceActionOutcomeAPI serviceActionOutcome = new ServiceActionOutcomeAPI();

                            serviceActionOutcome.id = describeUIServiceActionOutcome.id;
                            serviceActionOutcome.developerName = describeUIServiceActionOutcome.developerName;
                            serviceActionOutcome.developerSummary = describeUIServiceActionOutcome.developerSummary;

                            serviceActionRequest.serviceActionOutcomes.Add(serviceActionOutcome);
                        }
                    }

                    if (describeServiceActionResponse.serviceInputs != null &&
                        describeServiceActionResponse.serviceInputs.Count > 0)
                    {
                        serviceActionRequest.serviceInputs = new List<ServiceValueRequestAPI>();

                        foreach (DescribeValueAPI serviceInput in describeServiceActionResponse.serviceInputs)
                        {
                            serviceValueRequest = new ServiceValueRequestAPI();
                            serviceValueRequest.contentValue = serviceInput.contentValue;
                            serviceValueRequest.developerName = serviceInput.developerName;
                            serviceValueRequest.valueElementToReferenceId = null;
                            serviceValueRequest.contentType = serviceInput.contentType;

                            serviceActionRequest.serviceInputs.Add(serviceValueRequest);
                        }
                    }

                    if (describeServiceActionResponse.serviceOutputs != null &&
                        describeServiceActionResponse.serviceOutputs.Count > 0)
                    {
                        serviceActionRequest.serviceOutputs = new List<ServiceValueRequestAPI>();

                        foreach (DescribeValueAPI serviceOutput in describeServiceActionResponse.serviceOutputs)
                        {
                            serviceValueRequest = new ServiceValueRequestAPI();
                            serviceValueRequest.contentValue = serviceOutput.contentValue;
                            serviceValueRequest.developerName = serviceOutput.developerName;
                            serviceValueRequest.valueElementToReferenceId = null;
                            serviceValueRequest.contentType = serviceOutput.contentType;

                            serviceActionRequest.serviceOutputs.Add(serviceValueRequest);
                        }
                    }

                    serviceElementRequest.actions.Add(serviceActionRequest);
                }
            }

            if (describeServiceResponse.install != null)
            {
                serviceElementRequest.install = new ServiceInstallRequestAPI();
                serviceElementRequest.install.typeElements = describeServiceResponse.install.typeElements;
            }

            // Tell the service that we want to update if this name exists
            serviceElementRequest.updateByName = true;

            // Save the service element back to the system
            serviceElementResponse = DrawSingleton.GetInstance().SaveServiceElement(authenticatedWho, MANYWHO_BASE_URL, serviceElementRequest, "Program.CreateSalesforceService", "admin@manywho.com");

            return serviceElementResponse;
        }

        private static PageElementRequestAPI CreateLegalRequestPage(String developerName, ValueElementResponseAPI subjectValueElementResponse, ValueElementResponseAPI descriptionValueElementResponse)
        {
            PageElementRequestAPI pageElementRequest = null;
            PageComponentAPI pageComponent = null;
            PageContainerAPI pageContainer = null;

            pageElementRequest = new PageElementRequestAPI();
            pageElementRequest.developerName = developerName;
            pageElementRequest.elementType = ManyWhoConstants.UI_ELEMENT_TYPE_IMPLEMENTATION_PAGE_LAYOUT;
            pageElementRequest.label = "Legal Request";
            pageElementRequest.updateByName = true;
            pageElementRequest.pageContainers = new List<PageContainerAPI>();
            pageElementRequest.pageComponents = new List<PageComponentAPI>();

            pageContainer = new PageContainerAPI();
            pageContainer.containerType = ManyWhoConstants.CONTAINER_TYPE_VERTICAL_FLOW;
            pageContainer.label = "Legal Request";
            pageContainer.developerName = "container";

            pageElementRequest.pageContainers.Add(pageContainer);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_PRESENTATION;
            pageComponent.developerName = "intro";
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.content = "Please use this form to submit requests to the legal department. Make sure you provide as much information as possible to expedite the request.";
            pageComponent.order = 0;

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_INPUTBOX;
            pageComponent.developerName = "subject";
            pageComponent.hintValue = "Subject";
            pageComponent.isEditable = true;
            pageComponent.label = "Subject";
            pageComponent.maxSize = 255;
            pageComponent.order = 1;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.size = 75;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(subjectValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();

            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_TEXTBOX;
            pageComponent.developerName = "description";
            pageComponent.hintValue = "Description";
            pageComponent.isEditable = true;
            pageComponent.label = "Description";
            pageComponent.height = 10;
            pageComponent.width = 150;
            pageComponent.maxSize = 2000;
            pageComponent.order = 2;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(descriptionValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            return pageElementRequest;
        }

        private static PageElementRequestAPI CreateLegalTriagePage(String developerName, ValueElementResponseAPI subjectValueElementResponse, ValueElementResponseAPI descriptionValueElementResponse, TypeElementResponseAPI dropDownTypeElementResponse, ValueElementResponseAPI optionsTriageValueElementResponse, ValueElementResponseAPI triageValueElementResponse)
        {
            PageElementRequestAPI pageElementRequest = null;
            PageComponentAPI pageComponent = null;
            PageContainerAPI pageContainer = null;

            pageElementRequest = new PageElementRequestAPI();
            pageElementRequest.developerName = developerName;
            pageElementRequest.elementType = ManyWhoConstants.UI_ELEMENT_TYPE_IMPLEMENTATION_PAGE_LAYOUT;
            pageElementRequest.label = "Legal Request Triage";
            pageElementRequest.updateByName = true;
            pageElementRequest.pageContainers = new List<PageContainerAPI>();
            pageElementRequest.pageComponents = new List<PageComponentAPI>();

            pageContainer = new PageContainerAPI();
            pageContainer.containerType = ManyWhoConstants.CONTAINER_TYPE_VERTICAL_FLOW;
            pageContainer.label = "Legal Request Triage";
            pageContainer.developerName = "container";

            pageElementRequest.pageContainers.Add(pageContainer);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_PRESENTATION;
            pageComponent.developerName = "intro";
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.content = "Please review the submitted request and triage as appropriate.";
            pageComponent.order = 0;

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_COMBOBOX;
            pageComponent.developerName = "triage";
            pageComponent.isEditable = true;
            pageComponent.label = "Triage";
            pageComponent.order = 1;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(triageValueElementResponse.id, null, null);
            pageComponent.valueElementDataBindingReferenceId = new ValueElementIdAPI(optionsTriageValueElementResponse.id, null, null);
            pageComponent.columns = new List<PageComponentColumnAPI>();
            pageComponent.columns.Add(new PageComponentColumnAPI() { isBound = true, isDisplayValue = false, typeElementPropertyId = Utils.GetTypeElementEntryIdForDeveloperName(dropDownTypeElementResponse, "Value") });
            pageComponent.columns.Add(new PageComponentColumnAPI() { isBound = false, isDisplayValue = true, typeElementPropertyId = Utils.GetTypeElementEntryIdForDeveloperName(dropDownTypeElementResponse, "Label") });

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_INPUTBOX;
            pageComponent.developerName = "subject";
            pageComponent.hintValue = "Subject";
            pageComponent.isEditable = true;
            pageComponent.label = "Subject";
            pageComponent.maxSize = 255;
            pageComponent.order = 2;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.size = 75;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(subjectValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();

            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_TEXTBOX;
            pageComponent.developerName = "description";
            pageComponent.hintValue = "Description";
            pageComponent.isEditable = true;
            pageComponent.label = "Description";
            pageComponent.height = 10;
            pageComponent.width = 150;
            pageComponent.maxSize = 2000;
            pageComponent.order = 3;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(descriptionValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            return pageElementRequest;
        }

        private static PageElementRequestAPI CreateIncidentAndApprovalDetailsPage(String developerName, ValueElementResponseAPI subjectValueElementResponse, ValueElementResponseAPI descriptionValueElementResponse, ValueElementResponseAPI incidentDescriptionValueElementResponse, ValueElementResponseAPI storeManagerSignatureValueElementResponse, ValueElementResponseAPI customerSignatureValueElementResponse, ValueElementResponseAPI confirmDocumentsSignedAttachedValueElementResponse, Boolean editable)
        {
            PageElementRequestAPI pageElementRequest = null;
            PageComponentAPI pageComponent = null;
            PageContainerAPI pageContainer = null;

            pageElementRequest = new PageElementRequestAPI();
            pageElementRequest.developerName = developerName;
            pageElementRequest.elementType = ManyWhoConstants.UI_ELEMENT_TYPE_IMPLEMENTATION_PAGE_LAYOUT;
            pageElementRequest.label = "Incident Information Form";
            pageElementRequest.updateByName = true;
            pageElementRequest.pageContainers = new List<PageContainerAPI>();
            pageElementRequest.pageComponents = new List<PageComponentAPI>();

            pageContainer = new PageContainerAPI();
            pageContainer.containerType = ManyWhoConstants.CONTAINER_TYPE_VERTICAL_FLOW;
            pageContainer.label = "Incident Information Form";
            pageContainer.developerName = "container";

            pageElementRequest.pageContainers.Add(pageContainer);

            pageContainer = new PageContainerAPI();
            pageContainer.containerType = ManyWhoConstants.CONTAINER_TYPE_INLINE_FLOW;
            pageContainer.developerName = "signaturescontainer";
            pageContainer.order = 1;

            pageElementRequest.pageContainers.Add(pageContainer);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_PRESENTATION;
            pageComponent.developerName = "intro";
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.content = "The following information was submitted to the legal department for review.";
            pageComponent.order = 0;

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_INPUTBOX;
            pageComponent.developerName = "subject";
            pageComponent.hintValue = "Subject";
            pageComponent.isEditable = false;
            pageComponent.label = "Subject";
            pageComponent.maxSize = 255;
            pageComponent.order = 1;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.size = 75;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(subjectValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();

            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_TEXTBOX;
            pageComponent.developerName = "description";
            pageComponent.hintValue = "Description";
            pageComponent.isEditable = false;
            pageComponent.label = "Description";
            pageComponent.height = 10;
            pageComponent.width = 150;
            pageComponent.maxSize = 2000;
            pageComponent.order = 2;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(descriptionValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_PRESENTATION;
            pageComponent.developerName = "incidentintro";
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.content = "Please use the description below to write the details of the incident. The customer will need to verify that they are happy with your description. In addition to this written information, please attach any pictures to the feed that may be helpful to ensure we have a clear understanding of what happened.";
            pageComponent.order = 3;

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_TEXTBOX;
            pageComponent.developerName = "incident description";
            pageComponent.hintValue = "Incident Description";
            pageComponent.isEditable = editable;
            pageComponent.label = "Incident Description";
            pageComponent.height = 10;
            pageComponent.width = 150;
            pageComponent.maxSize = 2000;
            pageComponent.order = 4;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(incidentDescriptionValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_CHECKBOX;
            pageComponent.developerName = "confirm signatures";
            pageComponent.isEditable = editable;
            pageComponent.label = "Confirm Signatures Scanned/Attached";
            pageComponent.order = 5;
            pageComponent.pageContainerDeveloperName = "container";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(confirmDocumentsSignedAttachedValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_INPUTBOX;
            pageComponent.developerName = "store manager signature";
            pageComponent.hintValue = "Store Manager Signature";
            pageComponent.isEditable = editable;
            pageComponent.label = "Store Manager Signature";
            pageComponent.maxSize = 255;
            pageComponent.order = 1;
            pageComponent.pageContainerDeveloperName = "signaturescontainer";
            pageComponent.isRequired = true;
            pageComponent.size = 25;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(storeManagerSignatureValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_INPUTBOX;
            pageComponent.developerName = "customer signature";
            pageComponent.hintValue = "Customer Signature";
            pageComponent.isEditable = editable;
            pageComponent.label = "Customer Signature";
            pageComponent.maxSize = 255;
            pageComponent.order = 1;
            pageComponent.pageContainerDeveloperName = "signaturescontainer";
            pageComponent.isRequired = true;
            pageComponent.size = 25;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(customerSignatureValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            return pageElementRequest;
        }

        private static PageElementRequestAPI CreateIncident2WeekReviewDetailsPage(String developerName, ValueElementResponseAPI subjectValueElementResponse, ValueElementResponseAPI descriptionValueElementResponse, ValueElementResponseAPI incidentDescriptionValueElementResponse, ValueElementResponseAPI storeManagerSignatureValueElementResponse, ValueElementResponseAPI customerSignatureValueElementResponse, ValueElementResponseAPI confirmDocumentsSignedAttachedValueElementResponse, ValueElementResponseAPI weekReviewNotesValueElementResponse, Boolean editable)
        {
            PageElementRequestAPI pageElementRequest = null;
            PageComponentAPI pageComponent = null;
            PageContainerAPI pageContainer = null;

            pageElementRequest = CreateIncidentAndApprovalDetailsPage(developerName, subjectValueElementResponse, descriptionValueElementResponse, incidentDescriptionValueElementResponse, storeManagerSignatureValueElementResponse, customerSignatureValueElementResponse, confirmDocumentsSignedAttachedValueElementResponse, false);

            pageElementRequest.label = "Incident: 2 Week Review";

            pageContainer = new PageContainerAPI();
            pageContainer.containerType = ManyWhoConstants.CONTAINER_TYPE_VERTICAL_FLOW;
            pageContainer.label = "Incident Review";
            pageContainer.developerName = "reviewcontainer";
            pageContainer.order = 2;

            pageElementRequest.pageContainers.Add(pageContainer);

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_TEXTBOX;
            pageComponent.developerName = "2 week review notes";
            pageComponent.hintValue = "2 Week Review Notes";
            pageComponent.isEditable = editable;
            pageComponent.label = "2 Week Review Notes";
            pageComponent.height = 10;
            pageComponent.width = 150;
            pageComponent.maxSize = 2000;
            pageComponent.order = 4;
            pageComponent.pageContainerDeveloperName = "reviewcontainer";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(weekReviewNotesValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            return pageElementRequest;
        }

        private static PageElementRequestAPI CreateIncidentAnnualReviewDetailsPage(String developerName, ValueElementResponseAPI subjectValueElementResponse, ValueElementResponseAPI descriptionValueElementResponse, ValueElementResponseAPI incidentDescriptionValueElementResponse, ValueElementResponseAPI storeManagerSignatureValueElementResponse, ValueElementResponseAPI customerSignatureValueElementResponse, ValueElementResponseAPI confirmDocumentsSignedAttachedValueElementResponse, ValueElementResponseAPI weekReviewNotesValueElementResponse, ValueElementResponseAPI annualReviewNotesValueElementResponse)
        {
            PageElementRequestAPI pageElementRequest = null;
            PageComponentAPI pageComponent = null;

            pageElementRequest = CreateIncident2WeekReviewDetailsPage(developerName, subjectValueElementResponse, descriptionValueElementResponse, incidentDescriptionValueElementResponse, storeManagerSignatureValueElementResponse, customerSignatureValueElementResponse, confirmDocumentsSignedAttachedValueElementResponse, weekReviewNotesValueElementResponse, false);

            pageElementRequest.label = "Incident: Annual Review";

            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_TEXTBOX;
            pageComponent.developerName = "annual review notes";
            pageComponent.hintValue = "Annual Review Notes";
            pageComponent.isEditable = true;
            pageComponent.label = "Annual Review Notes";
            pageComponent.height = 10;
            pageComponent.width = 150;
            pageComponent.maxSize = 2000;
            pageComponent.order = 4;
            pageComponent.pageContainerDeveloperName = "reviewcontainer";
            pageComponent.isRequired = true;
            pageComponent.valueElementValueBindingReferenceId = new ValueElementIdAPI(annualReviewNotesValueElementResponse.id, null, null);

            pageElementRequest.pageComponents.Add(pageComponent);

            return pageElementRequest;
        }
    }
}
