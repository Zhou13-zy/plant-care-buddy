import { CareEventType } from '../models/CareEvent/careEventType';

/**
 * Gets the display name for a care event type
 */
export function getCareEventTypeName(type: CareEventType): string {
  return CareEventType[type];
}

/**
 * Gets a CSS class-friendly lowercase string for a care event type
 */
export function getCareEventTypeClass(type: CareEventType): string {
  return CareEventType[type].toLowerCase();
}

/**
 * Returns options for care event types to use in select dropdowns
 */
export function getCareEventTypeOptions() {
  return Object.entries(CareEventType)
    .filter(([key, value]) => typeof value === 'number')
    .map(([key, value]) => ({
      value: value as number,
      label: key
    }));
}