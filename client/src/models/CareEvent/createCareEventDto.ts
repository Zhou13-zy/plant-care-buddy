import { CareEventType } from './careEventType';

export interface CreateCareEventDto {
  plantId: number;
  eventType: CareEventType;
  eventDate: string;
  notes?: string;
  imagePath?: string;
}