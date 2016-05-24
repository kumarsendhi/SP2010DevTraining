using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace Training_Classes.Features.Feature_TrainingClasses
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("4b0e1301-ea09-415d-aa3e-78f8bfbfd683")]
    public class Feature_TrainingClassesEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            //Reference the newly created Classes list and perform the following:
            //1.  Set the display name of the "Title" column to "Class ID" and hide it from the New and Edit forms
            //2.  Make the "Class ID" column not required
            //3.  Set the default value of the Registrations column to 0 and do not display it on the new form
            //4.  Add the built-in "Start Date" and "End Date" columns

            //Reference the newly created Classes list
            SPWeb currentWeb = properties.Feature.Parent as SPWeb;
            SPList classesList = currentWeb.Lists["Classes"];

            //Title column updates
            SPField titleField = classesList.Fields["Title"];
            titleField.Required = false;
            titleField.ShowInNewForm = false;
            titleField.ShowInEditForm = false;
            titleField.Title = "Class ID";
            titleField.Update();

            //Registrations column updates
            SPField registrationsField = classesList.Fields["Registrations"];
            registrationsField.DefaultValue = "0";
            registrationsField.ShowInNewForm = false;
            registrationsField.Update();

            //Add the "Start Date" and "End Date" columns to the list, ensure they both display Date and Time, and add them to the default view of the list
            SPFieldDateTime startDate = currentWeb.Fields["Start Date"] as SPFieldDateTime;
            startDate.DisplayFormat = SPDateTimeFieldFormatType.DateTime;
            SPFieldDateTime endDate = currentWeb.Fields["End Date"] as SPFieldDateTime;
            endDate.DisplayFormat = SPDateTimeFieldFormatType.DateTime;
            classesList.Fields.Add(startDate);
            classesList.Fields.Add(endDate);
            SPView defaultView = classesList.DefaultView;
            defaultView.ViewFields.Add(startDate);
            defaultView.ViewFields.Add(endDate);
            defaultView.Update();
            classesList.Update();
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
