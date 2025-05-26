import { RecurrenceType } from "../models/Reminder/recurrenceType";
import { RecurrencePatternDto } from "../models/Reminder/recurrencePatternDto";

export const recurrenceTypeLabels: Record<number, string> = {
  [RecurrenceType.None]: "None",
  [RecurrenceType.Daily]: "Daily",
  [RecurrenceType.Weekly]: "Weekly",
  [RecurrenceType.Monthly]: "Monthly",
  [RecurrenceType.Yearly]: "Yearly",
  [RecurrenceType.Custom]: "Custom"
};

export function getRecurrenceDescription(recurrence: RecurrencePatternDto): string {
  if (!recurrence) return "";
  const typeLabel = recurrenceTypeLabels[recurrence.type as number] || "Custom";
  switch (recurrence.type) {
    case RecurrenceType.Daily:
      return `Every ${recurrence.interval} day${recurrence.interval > 1 ? "s" : ""}`;
    case RecurrenceType.Weekly:
      if (recurrence.daysOfWeek && recurrence.daysOfWeek.length > 0) {
        const days = recurrence.daysOfWeek.map(d => 
          ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"][+d]
        ).join(", ");
        return `Every ${recurrence.interval} week${recurrence.interval > 1 ? "s" : ""} on ${days}`;
      }
      return `Every ${recurrence.interval} week${recurrence.interval > 1 ? "s" : ""}`;
    case RecurrenceType.Monthly:
      if (recurrence.dayOfMonth)
        return `On the ${recurrence.dayOfMonth}${getOrdinal(recurrence.dayOfMonth)} of every ${recurrence.interval} month${recurrence.interval > 1 ? "s" : ""}`;
      return `Every ${recurrence.interval} month${recurrence.interval > 1 ? "s" : ""}`;
    case RecurrenceType.Yearly:
      if (recurrence.dayOfMonth && recurrence.monthOfYear)
        return `On the ${recurrence.dayOfMonth}${getOrdinal(recurrence.dayOfMonth)} of ${getMonthName(recurrence.monthOfYear)}`;
      return `Every year`;
    default:
      return `${typeLabel} (every ${recurrence.interval})`;
  }
}

export function getOrdinal(n: number) {
  if (n > 3 && n < 21) return "th";
  switch (n % 10) {
    case 1: return "st";
    case 2: return "nd";
    case 3: return "rd";
    default: return "th";
  }
}

function getMonthName(month: number) {
  return [
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
  ][month - 1];
}
