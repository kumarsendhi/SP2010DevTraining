using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace TrainingTimerJob.Features.Feature_TrainingTimerJobs
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("9f9abf14-d97f-4764-b58f-2aed3acff037")]
    public class Feature_TrainingTimerJobsEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;

            foreach(SPJobDefinition job in webApp.JobDefinitions)
            {
                if(job.Name== "Process Intructor Schedules")
                {
                    job.Delete();
                }
                TJ_ProcessInstructorSchedules timerJob = new TJ_ProcessInstructorSchedules("Process Instructor Schedules", webApp);
                timerJob.Title = "Process Instructor Schedules";
                SPMinuteSchedule schedule = new SPMinuteSchedule();
                schedule.BeginSecond = 0;
                schedule.EndSecond = 59;
                schedule.Interval = 1;

                timerJob.Schedule = schedule;
                timerJob.Update();

            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;
            foreach (SPJobDefinition job in webApp.JobDefinitions)
            {
                if (job.Name == "Process Intructor Schedules")
                {
                    job.Delete();
                }
            }
            }


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
