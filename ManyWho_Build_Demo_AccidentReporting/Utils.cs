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
using ManyWho.Flow.SDK.Draw.Flow;
using ManyWho.Flow.SDK.Draw.Elements.UI;
using ManyWho.Flow.SDK.Draw.Elements.Map;
using ManyWho.Flow.SDK.Draw.Elements.Type;
using ManyWho.Flow.SDK.Draw.Elements.Group;
using ManyWho.Flow.SDK.Draw.Elements.Shared;
using ManyWho.Flow.SDK.Draw.Elements.Config;

namespace ManyWho_Build_Demo_AccidentReporting
{
    public class Utils
    {
        public static GroupElementRequestAPI CreateSwimlaneElement(String developerName, Int32 x, Int32 y, Int32 height, Int32 width, String serviceElementId, String authenticationId)
        {
            GroupAuthorizationGroupAPI groupAuthorizationGroup = null;
            GroupElementRequestAPI groupElementRequest = null;

            // Create the map element request for the input
            groupElementRequest = new GroupElementRequestAPI();
            groupElementRequest.developerName = developerName;
            groupElementRequest.elementType = ManyWhoConstants.GROUP_ELEMENT_TYPE_IMPLEMENTATION_SWIMLANE;
            groupElementRequest.x = x;
            groupElementRequest.y = y;
            groupElementRequest.height = height;
            groupElementRequest.width = width;
            groupElementRequest.updateByName = true;

            // Create the group authorization
            groupAuthorizationGroup = new GroupAuthorizationGroupAPI();
            groupAuthorizationGroup.attribute = "MEMBERS";
            groupAuthorizationGroup.authenticationId = authenticationId;

            // Set the authorization object
            groupElementRequest.authorization = new GroupAuthorizationAPI();
            groupElementRequest.authorization.streamBehaviour = ManyWhoConstants.GROUP_AUTHORIZATION_STREAM_BEHAVIOUR_USE_EXISTING;
            groupElementRequest.authorization.globalAuthenticationType = ManyWhoConstants.GROUP_AUTHORIZATION_GLOBAL_AUTHENTICATION_TYPE_SPECIFIED;
            groupElementRequest.authorization.serviceElementId = serviceElementId;
            groupElementRequest.authorization.groups = new List<GroupAuthorizationGroupAPI>();
            groupElementRequest.authorization.groups.Add(groupAuthorizationGroup);

            return groupElementRequest;
        }

        public static void CreateOutcome(String developerName, String label, MapElementRequestAPI mapElementRequest, MapElementResponseAPI nextMapElementResponse)
        {
            OutcomeRequestAPI outcomeRequest = null;

            // Create the new outcome connecting the map element
            outcomeRequest = new OutcomeRequestAPI();
            outcomeRequest.developerName = developerName;
            outcomeRequest.label = label;
            outcomeRequest.nextMapElementId = nextMapElementResponse.id;
            outcomeRequest.pageActionBinding = ManyWhoConstants.ACTION_BINDING_SAVE;

            // Map sure we have an outcomes list to user
            if (mapElementRequest.outcomes == null)
            {
                mapElementRequest.outcomes = new List<OutcomeAPI>();
            }

            // Add the outcome to the request
            mapElementRequest.outcomes.Add(outcomeRequest);
        }

        public static MapElementRequestAPI CreateInputElement(String developerName, String pageElementId, String groupElementId, String serviceElementId, SharedElementResponseAPI notificationSharedElementResponse, Int32 x, Int32 y)
        {
            MapElementRequestAPI mapElementRequest = null;
            MessageActionAPI messageAction = null;
            MessageInputAPI messageInput = null;

            // Create the map element request for the input
            mapElementRequest = new MapElementRequestAPI();
            mapElementRequest.developerName = developerName;
            mapElementRequest.elementType = ManyWhoConstants.MAP_ELEMENT_TYPE_IMPLEMENTATION_INPUT;
            mapElementRequest.groupElementId = groupElementId;
            mapElementRequest.pageElementId = pageElementId;
            mapElementRequest.x = x;
            mapElementRequest.y = y;
            mapElementRequest.updateByName = true;
            mapElementRequest.postUpdateToStream = true;

            // Only add the notification if we have the service element id
            if (serviceElementId != null &&
                serviceElementId.Trim().Length > 0)
            {
                mapElementRequest.messageActions = new List<MessageActionAPI>();

                // Create the notification message action
                messageAction = new MessageActionAPI();
                messageAction.developerName = "Notify";
                messageAction.uriPart = "notify";
                messageAction.serviceElementId = serviceElementId;
                messageAction.inputs = new List<MessageInputAPI>();

                // Create the post input
                messageInput = new MessageInputAPI();
                messageInput.developerName = "Post";
                messageInput.sharedElementContentValueToReference = new SharedElementIdAPI(notificationSharedElementResponse.id, null, null);

                // Add the input to the action
                messageAction.inputs.Add(messageInput);

                // Add the action to the request
                mapElementRequest.messageActions.Add(messageAction);
            }

            return mapElementRequest;
        }

        public static MapElementRequestAPI CreateMessageElement(String developerName, String groupElementId, Int32 x, Int32 y)
        {
            MapElementRequestAPI mapElementRequest = null;

            // Create the map element request for the input
            mapElementRequest = new MapElementRequestAPI();
            mapElementRequest.developerName = developerName;
            mapElementRequest.elementType = ManyWhoConstants.MAP_ELEMENT_TYPE_IMPLEMENTATION_MESSAGE;
            mapElementRequest.groupElementId = groupElementId;
            mapElementRequest.x = x;
            mapElementRequest.y = y;
            mapElementRequest.updateByName = true;
            mapElementRequest.postUpdateToStream = false;

            return mapElementRequest;
        }

        public static PageElementRequestAPI CreateGenericPageElement(String developerName, String content)
        {
            PageElementRequestAPI pageElementRequest = null;
            PageContainerAPI pageContainer = null;
            PageComponentAPI pageComponent = null;

            // Create the basic page request
            pageElementRequest = new PageElementRequestAPI();
            pageElementRequest.developerName = developerName;
            pageElementRequest.elementType = ManyWhoConstants.UI_ELEMENT_TYPE_IMPLEMENTATION_PAGE_LAYOUT;
            pageElementRequest.pageContainers = new List<PageContainerAPI>();
            pageElementRequest.pageComponents = new List<PageComponentAPI>();
            pageElementRequest.updateByName = true;

            // Create a root page container
            pageContainer = new PageContainerAPI();
            pageContainer.containerType = ManyWhoConstants.PAGE_CONTAINER_CONTAINER_TYPE_VERTICAL_FLOW;
            pageContainer.developerName = "Generic";

            pageElementRequest.pageContainers.Add(pageContainer);

            // Create a single component to house the content
            pageComponent = new PageComponentAPI();
            pageComponent.componentType = ManyWhoConstants.COMPONENT_TYPE_PRESENTATION;
            pageComponent.pageContainerDeveloperName = "Generic";
            pageComponent.developerName = "Content";
            pageComponent.content = content;

            pageElementRequest.pageComponents.Add(pageComponent);

            return pageElementRequest;
        }

        /// <summary>
        /// A utility method for quickly creating shared elements.
        /// </summary>
        public static SharedElementRequestAPI CreateSharedElement(String developerName, String contentType)
        {
            SharedElementRequestAPI sharedElementRequest = null;

            sharedElementRequest = new SharedElementRequestAPI();
            sharedElementRequest.access = ManyWhoConstants.ACCESS_INPUT_OUTPUT;
            sharedElementRequest.contentType = contentType;
            sharedElementRequest.developerName = developerName;
            sharedElementRequest.elementType = ManyWhoConstants.SHARED_ELEMENT_TYPE_IMPLEMENTATION_VARIABLE;
            sharedElementRequest.updateByName = true;

            return sharedElementRequest;
        }

        public static TypeElementRequestAPI CreateDropDownType(String developerName)
        {
            TypeElementRequestAPI typeElementRequest = null;

            typeElementRequest = new TypeElementRequestAPI();
            typeElementRequest.developerName = developerName;
            typeElementRequest.elementType = ManyWhoConstants.TYPE_ELEMENT_TYPE_IMPLEMENTATION_TYPE;
            typeElementRequest.updateByName = true;
            typeElementRequest.typeElementEntries = new List<TypeElementEntryAPI>();
            typeElementRequest.typeElementEntries.Add(new TypeElementEntryAPI() { contentType = ManyWhoConstants.CONTENT_TYPE_STRING, developerName = "Label" });
            typeElementRequest.typeElementEntries.Add(new TypeElementEntryAPI() { contentType = ManyWhoConstants.CONTENT_TYPE_STRING, developerName = "Value" });

            return typeElementRequest;
        }

        public static String GetTypeElementEntryIdForDeveloperName(TypeElementResponseAPI typeElementResponse, String developerName)
        {
            String typeElementEntryId = null;

            if (typeElementResponse != null &&
                typeElementResponse.typeElementEntries != null &&
                typeElementResponse.typeElementEntries.Count > 0)
            {
                // Go through each of the type element entries and match by developer name
                foreach (TypeElementEntryAPI typeElementEntry in typeElementResponse.typeElementEntries)
                {
                    // Check to see if this is the matching type element entry
                    if (typeElementEntry.developerName.Equals(developerName, StringComparison.InvariantCultureIgnoreCase) == true)
                    {
                        // We have our type element entry - break out of the loop
                        typeElementEntryId = typeElementEntry.id;
                        break;
                    }
                }
            }

            return typeElementEntryId;
        }
    }
}
