import { CareEventType } from './careEventType';

export interface CreateCareEventDto {
  plantId: string;
  eventType: CareEventType;
  eventDate: string;
  notes?: string;
  beforePhoto?: File;
  afterPhoto?: File;
}