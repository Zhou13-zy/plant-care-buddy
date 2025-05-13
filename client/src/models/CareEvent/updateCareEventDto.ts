import { CareEventType } from './careEventType';

export interface UpdateCareEventDto {
  eventType: CareEventType;
  eventDate: string;
  notes?: string;
  imagePath?: string;
}