import { RecurrencePatternDto } from "./recurrencePatternDto";

export interface CreateReminderDto {
  plantId: string;
  type: string;
  title: string;
  description: string;
  dueDate: string;
  recurrence?: RecurrencePatternDto | null;
}