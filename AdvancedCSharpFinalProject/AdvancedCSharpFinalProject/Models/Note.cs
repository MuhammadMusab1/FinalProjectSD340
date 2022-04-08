namespace AdvancedCSharpFinalProject.Models
{
    public class Note : Notification
    {
        public Developer Developer { get; set; }
        public string DeveloperId { get; set; }
        public ProjectTask Task { get; set; }
        public int TaskId { get; set; }

        public Note() { }

        public Note(ApplicationUser targetUser, string message, Developer dev, ProjectTask task)
        {
            TargetUser = targetUser;
            TargetUserId = targetUser.Id;
            Message = message;
            IsUnread = true;

            Developer = dev;
            DeveloperId = dev.Id;
            Task = task;
            TaskId = task.Id;
        }

    }
}
