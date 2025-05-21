import { CareEventType } from './careEventType';

export interface CareEvent {
  id: string;
  plantId: string;
  plantName: string;
  eventType: CareEventType;
  eventTypeName: string;
  eventDate: string;
  notes?: string;
  beforeImagePath?: string;
  afterImagePath?: string;
}