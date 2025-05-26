import { RecurrenceType } from "./recurrenceType";

export interface RecurrencePatternDto {
    type: RecurrenceType;
    interval: number;
    endDate?: string;
    occurrenceCount?: number;
    daysOfWeek?: string[];
    dayOfMonth?: number;
    monthOfYear?: number;
  }