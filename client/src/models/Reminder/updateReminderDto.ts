import { RecurrencePatternDto } from "./recurrencePatternDto";

export interface UpdateReminderDto {
  title: string;
  description: string;
  dueDate: string;
  recurrence?: RecurrencePatternDto | null;
  intensity: string;
  strategyParameters?: string | null;
  isStrategyOverride: boolean;
}