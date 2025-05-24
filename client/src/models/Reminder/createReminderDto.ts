import { RecurrencePatternDto } from "./recurrencePatternDto";

export interface CreateReminderDto {
  plantId: string;
  type: string;
  title: string;
  description: string;
  dueDate: string;
  recurrence?: RecurrencePatternDto | null;
  intensity: string;
  strategyId?: string | null;
  strategyParameters?: string | null;
  isStrategyOverride: boolean;
}