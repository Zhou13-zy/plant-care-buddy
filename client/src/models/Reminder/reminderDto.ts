import { RecurrencePatternDto } from "./recurrencePatternDto";

export interface ReminderDto {
  id: string;
  plantId: string;
  type: string;
  title: string;
  description: string;
  dueDate: string;
  recurrence?: RecurrencePatternDto | null;
  intensity: string;
  isCompleted: boolean;
  completedDate?: string | null;
  strategyId?: string | null;
  strategyParameters?: string | null;
  isStrategyOverride: boolean;
  createdAt: string;
  updatedAt?: string | null;
}