using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class CreatedCleaningPlanReminderModel
    {
        [Required]
        public bool Cleaning_Reminder { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Cleaning_Reminder_DateTime { get; set; }
    }
}
