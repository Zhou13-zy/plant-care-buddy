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
  intensity: string;
  isCompleted: boolean;
  completedDate?: string | null;
  strategyId?: string | null;
  strategyParameters?: string | null;
  isStrategyOverride: boolean;
  createdAt: string;
  updatedAt?: string | null;
}