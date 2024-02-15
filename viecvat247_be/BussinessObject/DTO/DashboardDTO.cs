namespace BussinessObject.DTO
{
    public class DashboardDTO
    {
        public int NumberJobAssigner { get; set; }
        public int NumberJobSeeker { get; set; }
        public int NumberJobCategory { get; set; }
        public int NumberSkill { get; set; }
        public int NumberSkillCategory { get; set; }
        public int NumberStaff { get; set; }
        public int NumberJobWaitingApprover { get; set; }
        public int NumberJobApprover { get; set; }
        public int NumberJobWaitingEdit { get; set; }
        public int NumberJobReject { get; set; }
        public int NumberJobCompete { get; set; }
        public long NumberEpointDeposit { get; set; }
        public long NumberEpointWithDraw { get; set; }
        public long NumberEpointCreatePost { get; set; }
        public int NumberApplyJobPerson { get; set; }
        public int NumberRejectApplyJobPerson { get; set; }
        public int NumberWaittingApplyJobPerson { get; set; }
        public int NumberCompleteJobPerson { get; set; }
        public int NumberJobOnline { get; set; }
        public int NumberJobOffline { get; set; }
        public List<ChartDTO> chartDTOs { get; set; }
    }
}
