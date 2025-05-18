import { CareEventType } from './careEventType';

export interface CreateCareEventDto {
  plantId: number;
  eventType: CareEventType;
  eventDate: string;
  notes?: string;
  beforePhoto?: File;
  afterPhoto?: File;
}