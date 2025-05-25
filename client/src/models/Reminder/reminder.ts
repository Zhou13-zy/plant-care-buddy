import { RecurrencePatternDto } from "./recurrencePatternDto";

export interface Reminder {
  id: string;
  plantId: string;
  plantName: string;
  type: string;
  title: string;
  description: string;
  dueDate: string;
  recurrence?: RecurrencePatternDto | null;
  isCompleted: boolean;
  completedDate?: string | null;
  createdAt: string;
  updatedAt?: string | null;
}