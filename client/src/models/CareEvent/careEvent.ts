import { CareEventType } from './careEventType';

export interface CareEvent {
  id: number;
  plantId: number;
  plantName: string;
  eventType: CareEventType;
  eventTypeName: string;
  eventDate: string;
  notes?: string;
  beforeImagePath?: string;
  afterImagePath?: string;
}