namespace BaseApp.Constants
{
    public class EnumTypes
    {

        public enum ShiftStatus
        {
            LATE,
            OUTOFOFFICE,
            LEAVE,
            NA
        }

        public enum ActivityType
        {
            CHECKIN,
            CHECKOUT,
            BREAKSTART,
            BREAKEND
        }

        public enum ApplicationType
        {
            LEAVE,
            HOLIDAY,
            OVERTIME,
            EARLYLEAVE,
            GOINGOUT,
            REMOTE
        }

        public enum ApplicationStatus
        {
            REQUESTED,
            APPROVED,
            REJECTED,
            CANCELLED
        }

        public enum RoleType
        {
            ROLE_ADMIN,
            ROLE_EMPLOYEE
        }

        public enum TaskType
        {
            CODE,
            TEST,
            CODE_CR,
            FIX_BUG,
            INFRA_OR_DEPLOY,
            RESEARCH_SETUP,
            BA,
            DESIGN,
            DESIGN_DB,
            DESIGN_SPEC,
            CREATE_TEST_CASE,
            MANAGEMENT,
            OT,
            TRANSLATE,
            TRANSFER,
            SYNC,
            MEETING,
            DAILY
        }

        public enum TaskStatus
        {
            IN_BACKLOG,
            OPEN,
            IN_PROGRESS,
            RESOLVED,
            REOPEN,
            DONE,
            CANCEL,
            CONFIRM,
            CLOSE
        }

    }
}
