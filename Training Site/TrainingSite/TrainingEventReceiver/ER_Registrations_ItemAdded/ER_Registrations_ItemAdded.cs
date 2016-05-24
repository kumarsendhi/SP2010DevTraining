using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace TrainingEventReceiver.ER_Registrations_ItemAdded
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ER_Registrations_ItemAdded : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            if (properties.ListTitle == "Registration")
            {
                string classId = properties.ListItem["RegistrationID"].ToString();
                string id = properties.ListItem["ID"].ToString();
                properties.ListItem["ID"] = classId + "-" + id;
                properties.ListItem.Update();

                SPWeb web = properties.Web;
                SPList clist = web.Lists["Classes"];
                SPListItem item = clist.GetItemById(Convert.ToInt32(classId));
                item["Registrations"] = Convert.ToInt32(item["Registrations"].ToString()) + 1;
                item.Update();


            }
                base.ItemAdded(properties);
        }


    }
}