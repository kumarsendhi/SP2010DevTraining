using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace Training_Classes.Features.Feature_TrainingClass
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("2a57cb5a-92ad-4c4d-b139-b07b323a9a85")]
    public class Feature_TrainingClassEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb currentWeb = properties.Feature.Parent as SPWeb;

            SPList clist = currentWeb.Lists["Classes"];
            SPField title = clist.Fields["Title"];
            title.Required = false;
            title.ShowInEditForm = false;
            title.ShowInNewForm = false;

            title.Title = "Class ID";
            title.Update();

            SPField registrationFiled = clist.Fields["Registrations"];
            registrationFiled.DefaultValue = "0";
            registrationFiled.ShowInNewForm = false;
            registrationFiled.Update();

            SPFieldDateTime startDate = currentWeb.Fields["Start Date"] as SPFieldDateTime;
            startDate.DisplayFormat = SPDateTimeFieldFormatType.DateTime;
            SPFieldDateTime endDate = currentWeb.Fields["End Date"] as SPFieldDateTime;
            endDate.DisplayFormat = SPDateTimeFieldFormatType.DateTime;

            clist.Fields.Add(startDate);
            clist.Fields.Add(endDate);
            SPView defaultview = clist.DefaultView;
            defaultview.ViewFields.Add(startDate);
            defaultview.ViewFields.Add(endDate);
            defaultview.Update();
            clist.Update();


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
