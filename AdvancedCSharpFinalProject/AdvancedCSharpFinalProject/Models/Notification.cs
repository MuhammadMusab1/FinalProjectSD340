using System;

namespace AdvancedCSharpFinalProject.Models
{
	public class Notification
	{
		public int Id { get; set; }
		public ApplicationUser TargetUser { get; set; }
		public string TargetUserId { get; set; }
		public string Message { get; set; }
		public bool IsUnread { get; set; }
		public Notification() { }
		public Notification(ApplicationUser targetUser, string message)
        {
			TargetUser = targetUser;
			TargetUserId = targetUser.Id;
			Message = message;
			IsUnread = true;
        }
	}

}
