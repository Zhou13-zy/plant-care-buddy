export interface RecurrencePatternDto {
    type: string;
    interval: number;
    endDate?: string;
    occurrenceCount?: number;
    daysOfWeek?: string[];
    dayOfMonth?: number;
  }