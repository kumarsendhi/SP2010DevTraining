using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace TrainingEventReceiver.ER_RegistrationList_ItemDeleting
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ER_RegistrationList_ItemDeleting : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being deleted.
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            if (properties.ListTitle == "Registration")
            {
                string id = properties.ListItem["RegistrationId"].ToString();
                int hyphenindex = id.IndexOf("-");
                string classId = id.Substring(0, hyphenindex);

                SPWeb web = properties.Web;
                SPList clist = web.Lists["Classes"];
                SPListItem item = clist.GetItemById(Convert.ToInt32(classId));
                item["Registrations"] = Convert.ToInt32(item["Registrations"].ToString()) - 1;
                item.Update();
            }
                base.ItemDeleting(properties);
        }


    }
}