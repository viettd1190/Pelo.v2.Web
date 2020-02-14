using System;

namespace Pelo.v2.Web.Models.TaskModel
{
    public class TaskSearchModel : BaseSearchModel
    {
        public string Name { get; set; }

        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Code { get; set; }

        public int TaskStatusId { get; set; }

        public int TaskPriorityId { get; set; }

        public int TaskLoopId { get; set; }

        public int TaskTypeId { get; set; }

        public int UserCreatedId { get; set; }

        public int UserCareId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ColumnOrder{ get; set; }
    }
}
