import { CareEventType } from './careEventType';

export interface UpdateCareEventDto {
  eventType: CareEventType;
  eventDate: string;
  notes?: string;
  beforePhoto?: File;
  afterPhoto?: File;
}