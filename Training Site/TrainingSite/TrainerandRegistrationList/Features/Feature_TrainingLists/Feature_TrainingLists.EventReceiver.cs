using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace TrainerandRegistrationList.Features.Feature_TrainingLists
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("4549101f-614f-4243-a088-aebbb269904b")]
    public class Feature_TrainingListsEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb currentWeb = properties.Feature.Parent as SPWeb;

            SPList RList = currentWeb.Lists["Registration"];
            SPList TList = currentWeb.Lists["Trainers"];

            SPFieldCollection rfields = RList.Fields;
            SPFieldCollection tfields = TList.Fields;

            SPField fullNameField = tfields["Full Name"];
            fullNameField.Required = true;
            fullNameField.Update();

            SPField emailField = tfields["Email Address"];
            emailField.Required = true;
            emailField.Update();

            SPField titleField = rfields["Title"];
            titleField.Title = "Registration ID";
            titleField.Required = false;
            titleField.ShowInNewForm = false;
            titleField.Update();

            rfields.Add("First Name", SPFieldType.Text, true);
            rfields.Add("Last Name", SPFieldType.Text, true);
            rfields.Add("E-mail Address", SPFieldType.Text, true);
            rfields.Add("Phone Number", SPFieldType.Text, false);
            rfields.Add("ClassId", SPFieldType.Text, false);
            RList.Update();

            SPField classId = rfields["ClassId"];
            classId.ReadOnlyField = true;
            classId.Update();

           SPView tDefaultView = TList.DefaultView;
            tDefaultView.ViewFields.Delete("Attachments");
            tDefaultView.Update();

            SPView rDefaultView = RList.DefaultView;
            //rDefaultView.ViewFields.Delete("Attachments");
            rDefaultView.ViewFields.Add("First Name");
            rDefaultView.ViewFields.Add("Last Name");
            rDefaultView.ViewFields.Add("E-mail Address");
            rDefaultView.ViewFields.Add("Phone Number");
            rDefaultView.Update();

        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
