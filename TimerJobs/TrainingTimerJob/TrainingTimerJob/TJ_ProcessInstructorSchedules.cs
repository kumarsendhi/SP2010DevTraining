using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace TrainingTimerJob
{
    public class TJ_ProcessInstructorSchedules : SPJobDefinition
    {
        // By defining the job name as a constant in the job definition class, you ensure that it is always available and remains unchanged.
        public const string JobName = "Process Intructor Schedules";

        // This is required for the serialization and de-serialization of your timer job. 
        public TJ_ProcessInstructorSchedules() : base() { }

        public TJ_ProcessInstructorSchedules(string JobName, SPService service, SPServer server, SPJobLockType lockType) : base(JobName,service,server, SPJobLockType.ContentDatabase) { }

       public TJ_ProcessInstructorSchedules(string JobName, SPWebApplication webApplication): base(JobName, webApplication, null, SPJobLockType.ContentDatabase) { }

        public override void Execute(Guid targetInstanceId)
        {
            SPWebApplication webApp = this.Parent as SPWebApplication;
            SPList taskList = webApp.Sites[0].RootWeb.Lists["Tasks"];
            SPListItem newTask = taskList.Items.Add();
            newTask["Title"] = DateTime.Now.ToString();
            newTask.Update();

            MailMessage mail = new MailMessage("administrator@corp.com", "administrator@corp.com", "Hello this is mail", "How are you?");
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("win-c3h44gvu09b");
            client.Send(mail);

        }



    }
}
